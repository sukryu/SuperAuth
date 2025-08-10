using System.Text.Json.Serialization;

namespace SuperAuth.Shared.Contracts.DTOs.Common;

/// <summary>
/// 오류 응답 DTO
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// 오류 코드
    /// </summary>
    [JsonPropertyName("code")]
    public string Code { get; init; } = string.Empty;

    /// <summary>
    /// 오류 메시지
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// 상세 오류 메시지
    /// </summary>
    [JsonPropertyName("details")]
    public string? Details { get; init; }

    /// <summary>
    /// 내부 오류 코드 (디버깅용)
    /// </summary>
    [JsonPropertyName("innerCode")]
    public string? InnerCode { get; init; }

    /// <summary>
    /// 오류 발생 시각 (UTC)
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 오류 추적 ID
    /// </summary>
    [JsonPropertyName("traceId")]
    public string? TraceId { get; init; }

    /// <summary>
    /// 검증 오류 목록
    /// </summary>
    [JsonPropertyName("validationErrors")]
    public IList<ValidationError>? ValidationErrors { get; init; }

    /// <summary>
    /// 추가 메타데이터
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }

    /// <summary>
    /// 도움말 URL
    /// </summary>
    [JsonPropertyName("helpUrl")]
    public string? HelpUrl { get; init; }

    /// <summary>
    /// 오류 심각도
    /// </summary>
    [JsonPropertyName("severity")]
    public ErrorSeverity Severity { get; init; } = ErrorSeverity.Error;

    /// <summary>
    /// 재시도 가능 여부
    /// </summary>
    [JsonPropertyName("retryable")]
    public bool Retryable { get; init; }

    /// <summary>
    /// 재시도 권장 시간 (초)
    /// </summary>
    [JsonPropertyName("retryAfterSeconds")]
    public int? RetryAfterSeconds { get; init; }

    /// <summary>
    /// 기본 오류 응답 생성
    /// </summary>
    /// <param name="code">오류 코드</param>
    /// <param name="message">오류 메시지</param>
    /// <param name="details">상세 정보</param>
    /// <returns>오류 응답</returns>
    public static ErrorResponse Create(string code, string message, string? details = null)
    {
        return new ErrorResponse
        {
            Code = code,
            Message = message,
            Details = details
        };
    }

    /// <summary>
    /// 검증 오류 응답 생성
    /// </summary>
    /// <param name="validationErrors">검증 오류 목록</param>
    /// <returns>검증 오류 응답</returns>
    public static ErrorResponse ValidationError(IEnumerable<ValidationError> validationErrors)
    {
        return new ErrorResponse
        {
            Code = "VALIDATION_ERROR",
            Message = "입력 데이터 검증에 실패했습니다.",
            ValidationErrors = validationErrors.ToList(),
            Severity = ErrorSeverity.Warning
        };
    }

    /// <summary>
    /// 인증 오류 응답 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <returns>인증 오류 응답</returns>
    public static ErrorResponse Unauthorized(string? message = null)
    {
        return new ErrorResponse
        {
            Code = "UNAUTHORIZED",
            Message = message ?? "인증이 필요합니다.",
            Severity = ErrorSeverity.Error,
            HelpUrl = "/docs/authentication"
        };
    }

    /// <summary>
    /// 권한 없음 오류 응답 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <returns>권한 없음 오류 응답</returns>
    public static ErrorResponse Forbidden(string? message = null)
    {
        return new ErrorResponse
        {
            Code = "FORBIDDEN",
            Message = message ?? "접근 권한이 없습니다.",
            Severity = ErrorSeverity.Error,
            HelpUrl = "/docs/authorization"
        };
    }

    /// <summary>
    /// 리소스 없음 오류 응답 생성
    /// </summary>
    /// <param name="resourceName">리소스 이름</param>
    /// <returns>리소스 없음 오류 응답</returns>
    public static ErrorResponse NotFound(string? resourceName = null)
    {
        var message = resourceName != null 
            ? $"{resourceName}을(를) 찾을 수 없습니다."
            : "요청한 리소스를 찾을 수 없습니다.";

        return new ErrorResponse
        {
            Code = "NOT_FOUND",
            Message = message,
            Severity = ErrorSeverity.Warning
        };
    }

    /// <summary>
    /// 충돌 오류 응답 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <returns>충돌 오류 응답</returns>
    public static ErrorResponse Conflict(string? message = null)
    {
        return new ErrorResponse
        {
            Code = "CONFLICT",
            Message = message ?? "요청이 현재 리소스 상태와 충돌합니다.",
            Severity = ErrorSeverity.Warning
        };
    }

    /// <summary>
    /// 비즈니스 규칙 위반 오류 응답 생성
    /// </summary>
    /// <param name="ruleName">위반된 규칙명</param>
    /// <param name="message">오류 메시지</param>
    /// <returns>비즈니스 규칙 위반 오류 응답</returns>
    public static ErrorResponse BusinessRuleViolation(string ruleName, string message)
    {
        return new ErrorResponse
        {
            Code = "BUSINESS_RULE_VIOLATION",
            Message = message,
            Details = $"비즈니스 규칙 '{ruleName}'이 위반되었습니다.",
            Severity = ErrorSeverity.Warning,
            Metadata = new Dictionary<string, object> { ["ruleName"] = ruleName }
        };
    }

    /// <summary>
    /// 속도 제한 오류 응답 생성
    /// </summary>
    /// <param name="retryAfterSeconds">재시도 권장 시간</param>
    /// <returns>속도 제한 오류 응답</returns>
    public static ErrorResponse RateLimitExceeded(int retryAfterSeconds = 60)
    {
        return new ErrorResponse
        {
            Code = "RATE_LIMIT_EXCEEDED",
            Message = "요청 한도를 초과했습니다. 잠시 후 다시 시도해주세요.",
            Severity = ErrorSeverity.Warning,
            Retryable = true,
            RetryAfterSeconds = retryAfterSeconds
        };
    }

    /// <summary>
    /// 서버 내부 오류 응답 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <param name="traceId">추적 ID</param>
    /// <returns>서버 내부 오류 응답</returns>
    public static ErrorResponse InternalServerError(string? message = null, string? traceId = null)
    {
        return new ErrorResponse
        {
            Code = "INTERNAL_SERVER_ERROR",
            Message = message ?? "서버에서 요청을 처리하는 중 오류가 발생했습니다.",
            Severity = ErrorSeverity.Critical,
            TraceId = traceId,
            HelpUrl = "/docs/support"
        };
    }

    /// <summary>
    /// 외부 서비스 오류 응답 생성
    /// </summary>
    /// <param name="serviceName">서비스 이름</param>
    /// <param name="message">오류 메시지</param>
    /// <returns>외부 서비스 오류 응답</returns>
    public static ErrorResponse ExternalServiceError(string serviceName, string? message = null)
    {
        return new ErrorResponse
        {
            Code = "EXTERNAL_SERVICE_ERROR",
            Message = message ?? $"{serviceName} 서비스에서 오류가 발생했습니다.",
            Severity = ErrorSeverity.Error,
            Retryable = true,
            RetryAfterSeconds = 30,
            Metadata = new Dictionary<string, object> { ["serviceName"] = serviceName }
        };
    }

    /// <summary>
    /// 타임아웃 오류 응답 생성
    /// </summary>
    /// <param name="operation">타임아웃된 작업</param>
    /// <returns>타임아웃 오류 응답</returns>
    public static ErrorResponse Timeout(string? operation = null)
    {
        var message = operation != null 
            ? $"{operation} 작업이 시간 초과되었습니다."
            : "요청 처리 시간이 초과되었습니다.";

        return new ErrorResponse
        {
            Code = "TIMEOUT",
            Message = message,
            Severity = ErrorSeverity.Error,
            Retryable = true,
            RetryAfterSeconds = 10
        };
    }
}

/// <summary>
/// 검증 오류 DTO
/// </summary>
public class ValidationError
{
    /// <summary>
    /// 필드 이름
    /// </summary>
    [JsonPropertyName("field")]
    public string Field { get; init; } = string.Empty;

    /// <summary>
    /// 오류 메시지
    /// </summary>
    [JsonPropertyName("message")]
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// 오류 코드
    /// </summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>
    /// 시도된 값
    /// </summary>
    [JsonPropertyName("attemptedValue")]
    public object? AttemptedValue { get; init; }

    /// <summary>
    /// 사용자 정의 상태 정보
    /// </summary>
    [JsonPropertyName("customState")]
    public Dictionary<string, object>? CustomState { get; init; }

    /// <summary>
    /// 검증 오류 생성
    /// </summary>
    /// <param name="field">필드 이름</param>
    /// <param name="message">오류 메시지</param>
    /// <param name="code">오류 코드</param>
    /// <param name="attemptedValue">시도된 값</param>
    /// <returns>검증 오류</returns>
    public static ValidationError Create(string field, string message, string? code = null, object? attemptedValue = null)
    {
        return new ValidationError
        {
            Field = field,
            Message = message,
            Code = code,
            AttemptedValue = attemptedValue
        };
    }

    /// <summary>
    /// 필수 필드 오류 생성
    /// </summary>
    /// <param name="field">필드 이름</param>
    /// <returns>필수 필드 오류</returns>
    public static ValidationError Required(string field)
    {
        return new ValidationError
        {
            Field = field,
            Message = $"{field}은(는) 필수 항목입니다.",
            Code = "REQUIRED"
        };
    }

    /// <summary>
    /// 형식 오류 생성
    /// </summary>
    /// <param name="field">필드 이름</param>
    /// <param name="expectedFormat">예상 형식</param>
    /// <param name="attemptedValue">시도된 값</param>
    /// <returns>형식 오류</returns>
    public static ValidationError InvalidFormat(string field, string expectedFormat, object? attemptedValue = null)
    {
        return new ValidationError
        {
            Field = field,
            Message = $"{field}의 형식이 올바르지 않습니다. 예상 형식: {expectedFormat}",
            Code = "INVALID_FORMAT",
            AttemptedValue = attemptedValue
        };
    }

    /// <summary>
    /// 범위 오류 생성
    /// </summary>
    /// <param name="field">필드 이름</param>
    /// <param name="min">최소값</param>
    /// <param name="max">최대값</param>
    /// <param name="attemptedValue">시도된 값</param>
    /// <returns>범위 오류</returns>
    public static ValidationError OutOfRange(string field, object min, object max, object? attemptedValue = null)
    {
        return new ValidationError
        {
            Field = field,
            Message = $"{field}은(는) {min}과(와) {max} 사이의 값이어야 합니다.",
            Code = "OUT_OF_RANGE",
            AttemptedValue = attemptedValue
        };
    }

    /// <summary>
    /// 중복 값 오류 생성
    /// </summary>
    /// <param name="field">필드 이름</param>
    /// <param name="attemptedValue">시도된 값</param>
    /// <returns>중복 값 오류</returns>
    public static ValidationError Duplicate(string field, object? attemptedValue = null)
    {
        return new ValidationError
        {
            Field = field,
            Message = $"{field} 값이 이미 사용 중입니다.",
            Code = "DUPLICATE",
            AttemptedValue = attemptedValue
        };
    }
}

/// <summary>
/// 오류 심각도
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ErrorSeverity
{
    /// <summary>
    /// 정보
    /// </summary>
    Info,

    /// <summary>
    /// 경고
    /// </summary>
    Warning,

    /// <summary>
    /// 오류
    /// </summary>
    Error,

    /// <summary>
    /// 심각
    /// </summary>
    Critical
}