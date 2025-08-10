using System.Text.Json.Serialization;

namespace SuperAuth.Shared.Contracts.DTOs.Common;

/// <summary>
/// 표준 API 응답 래퍼
/// </summary>
public class ApiResponse
{
    /// <summary>
    /// 요청 성공 여부
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// 응답 메시지
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; init; }

    /// <summary>
    /// 오류 정보
    /// </summary>
    [JsonPropertyName("error")]
    public ErrorResponse? Error { get; init; }

    /// <summary>
    /// 메타데이터
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }

    /// <summary>
    /// 타임스탬프 (UTC)
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 요청 추적 ID
    /// </summary>
    [JsonPropertyName("traceId")]
    public string? TraceId { get; init; }

    /// <summary>
    /// API 버전
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }

    /// <summary>
    /// 성공 응답 생성
    /// </summary>
    /// <param name="message">성공 메시지</param>
    /// <param name="metadata">메타데이터</param>
    /// <returns>성공 응답</returns>
    public static ApiResponse Success(string? message = null, Dictionary<string, object>? metadata = null)
    {
        return new ApiResponse
        {
            Success = true,
            Message = message ?? "요청이 성공적으로 처리되었습니다.",
            Metadata = metadata
        };
    }

    /// <summary>
    /// 실패 응답 생성
    /// </summary>
    /// <param name="error">오류 정보</param>
    /// <param name="message">오류 메시지</param>
    /// <returns>실패 응답</returns>
    public static ApiResponse Failure(ErrorResponse error, string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message ?? "요청 처리 중 오류가 발생했습니다.",
            Error = error
        };
    }

    /// <summary>
    /// 실패 응답 생성 (간단한 메시지)
    /// </summary>
    /// <param name="errorMessage">오류 메시지</param>
    /// <param name="errorCode">오류 코드</param>
    /// <returns>실패 응답</returns>
    public static ApiResponse Failure(string errorMessage, string? errorCode = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = "요청 처리 중 오류가 발생했습니다.",
            Error = new ErrorResponse
            {
                Code = errorCode ?? "UNKNOWN_ERROR",
                Message = errorMessage
            }
        };
    }

    /// <summary>
    /// 검증 실패 응답 생성
    /// </summary>
    /// <param name="validationErrors">검증 오류 목록</param>
    /// <returns>검증 실패 응답</returns>
    public static ApiResponse ValidationFailure(IEnumerable<ValidationError> validationErrors)
    {
        return new ApiResponse
        {
            Success = false,
            Message = "입력 데이터 검증에 실패했습니다.",
            Error = new ErrorResponse
            {
                Code = "VALIDATION_ERROR",
                Message = "하나 이상의 필드에서 검증 오류가 발생했습니다.",
                ValidationErrors = validationErrors.ToList()
            }
        };
    }

    /// <summary>
    /// 인증 실패 응답 생성
    /// </summary>
    /// <param name="message">인증 실패 메시지</param>
    /// <returns>인증 실패 응답</returns>
    public static ApiResponse Unauthorized(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message ?? "인증이 필요합니다.",
            Error = new ErrorResponse
            {
                Code = "UNAUTHORIZED",
                Message = message ?? "유효한 인증 토큰이 필요합니다."
            }
        };
    }

    /// <summary>
    /// 권한 없음 응답 생성
    /// </summary>
    /// <param name="message">권한 없음 메시지</param>
    /// <returns>권한 없음 응답</returns>
    public static ApiResponse Forbidden(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message ?? "접근 권한이 없습니다.",
            Error = new ErrorResponse
            {
                Code = "FORBIDDEN",
                Message = message ?? "이 리소스에 접근할 권한이 없습니다."
            }
        };
    }

    /// <summary>
    /// 리소스 없음 응답 생성
    /// </summary>
    /// <param name="resourceName">리소스 이름</param>
    /// <returns>리소스 없음 응답</returns>
    public static ApiResponse NotFound(string? resourceName = null)
    {
        var message = resourceName != null 
            ? $"{resourceName}을(를) 찾을 수 없습니다."
            : "요청한 리소스를 찾을 수 없습니다.";

        return new ApiResponse
        {
            Success = false,
            Message = message,
            Error = new ErrorResponse
            {
                Code = "NOT_FOUND",
                Message = message
            }
        };
    }

    /// <summary>
    /// 서버 오류 응답 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <returns>서버 오류 응답</returns>
    public static ApiResponse InternalServerError(string? message = null)
    {
        return new ApiResponse
        {
            Success = false,
            Message = message ?? "서버 내부 오류가 발생했습니다.",
            Error = new ErrorResponse
            {
                Code = "INTERNAL_SERVER_ERROR",
                Message = message ?? "서버에서 요청을 처리하는 중 오류가 발생했습니다."
            }
        };
    }
}

/// <summary>
/// 데이터를 포함한 API 응답
/// </summary>
/// <typeparam name="T">데이터 타입</typeparam>
public class ApiResponse<T> : ApiResponse
{
    /// <summary>
    /// 응답 데이터
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; init; }

    /// <summary>
    /// 성공 응답 생성 (데이터 포함)
    /// </summary>
    /// <param name="data">응답 데이터</param>
    /// <param name="message">성공 메시지</param>
    /// <param name="metadata">메타데이터</param>
    /// <returns>성공 응답</returns>
    public static ApiResponse<T> Success(T data, string? message = null, Dictionary<string, object>? metadata = null)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message ?? "요청이 성공적으로 처리되었습니다.",
            Metadata = metadata
        };
    }

    /// <summary>
    /// 실패 응답 생성 (데이터 타입 지정)
    /// </summary>
    /// <param name="error">오류 정보</param>
    /// <param name="message">오류 메시지</param>
    /// <returns>실패 응답</returns>
    public static new ApiResponse<T> Failure(ErrorResponse error, string? message = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default,
            Message = message ?? "요청 처리 중 오류가 발생했습니다.",
            Error = error
        };
    }

    /// <summary>
    /// 실패 응답 생성 (간단한 메시지, 데이터 타입 지정)
    /// </summary>
    /// <param name="errorMessage">오류 메시지</param>
    /// <param name="errorCode">오류 코드</param>
    /// <returns>실패 응답</returns>
    public static new ApiResponse<T> Failure(string errorMessage, string? errorCode = null)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Data = default,
            Message = "요청 처리 중 오류가 발생했습니다.",
            Error = new ErrorResponse
            {
                Code = errorCode ?? "UNKNOWN_ERROR",
                Message = errorMessage
            }
        };
    }

    /// <summary>
    /// ApiResponse를 ApiResponse<T>로 변환
    /// </summary>
    /// <param name="response">기본 ApiResponse</param>
    /// <param name="data">추가할 데이터</param>
    /// <returns>데이터가 포함된 ApiResponse</returns>
    public static ApiResponse<T> FromApiResponse(ApiResponse response, T? data = default)
    {
        return new ApiResponse<T>
        {
            Success = response.Success,
            Data = data,
            Message = response.Message,
            Error = response.Error,
            Metadata = response.Metadata,
            Timestamp = response.Timestamp,
            TraceId = response.TraceId,
            Version = response.Version
        };
    }
}