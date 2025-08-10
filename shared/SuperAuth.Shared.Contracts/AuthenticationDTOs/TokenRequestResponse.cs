using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SuperAuth.Shared.Contracts.DTOs.Common;
using SuperAuth.Shared.Contracts.Enums;

namespace SuperAuth.Shared.Contracts.DTOs.Authentication;

/// <summary>
/// 토큰 갱신 요청 DTO
/// </summary>
public class RefreshTokenRequest : BaseRequest
{
    /// <summary>
    /// 리프레시 토큰
    /// </summary>
    [JsonPropertyName("refreshToken")]
    [Required(ErrorMessage = "리프레시 토큰은 필수입니다.")]
    public string RefreshToken { get; init; } = string.Empty;

    /// <summary>
    /// 기존 액세스 토큰 (선택적)
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; init; }
}

/// <summary>
/// 토큰 응답 DTO
/// </summary>
public class TokenResponse : BaseResponse
{
    /// <summary>
    /// 액세스 토큰
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string AccessToken { get; init; } = string.Empty;

    /// <summary>
    /// 리프레시 토큰
    /// </summary>
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; init; }

    /// <summary>
    /// 토큰 타입 (Bearer)
    /// </summary>
    [JsonPropertyName("tokenType")]
    public string TokenType { get; init; } = "Bearer";

    /// <summary>
    /// 만료 시간 (초)
    /// </summary>
    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; init; }

    /// <summary>
    /// 만료 시간 (UTC)
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset ExpiresAt { get; init; }

    /// <summary>
    /// 권한 범위
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; init; }

    /// <summary>
    /// 권한 범위 목록
    /// </summary>
    [JsonPropertyName("scopes")]
    public IList<string>? Scopes { get; init; }

    /// <summary>
    /// JWT ID (고유 식별자)
    /// </summary>
    [JsonPropertyName("jti")]
    public string? Jti { get; init; }

    /// <summary>
    /// 발급자
    /// </summary>
    [JsonPropertyName("issuer")]
    public string? Issuer { get; init; }

    /// <summary>
    /// 대상
    /// </summary>
    [JsonPropertyName("audience")]
    public string? Audience { get; init; }

    /// <summary>
    /// 토큰 발급 시간
    /// </summary>
    [JsonPropertyName("issuedAt")]
    public DateTimeOffset IssuedAt { get; init; } = DateTimeOffset.UtcNow;
}

/// <summary>
/// 토큰 검증 요청 DTO
/// </summary>
public class ValidateTokenRequest : BaseRequest
{
    /// <summary>
    /// 검증할 토큰
    /// </summary>
    [JsonPropertyName("token")]
    [Required(ErrorMessage = "토큰은 필수입니다.")]
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// 토큰 타입
    /// </summary>
    [JsonPropertyName("tokenType")]
    public string TokenType { get; init; } = "Bearer";
}

/// <summary>
/// 토큰 검증 응답 DTO
/// </summary>
public class ValidateTokenResponse : BaseResponse
{
    /// <summary>
    /// 토큰 유효성
    /// </summary>
    [JsonPropertyName("isValid")]
    public bool IsValid { get; init; }

    /// <summary>
    /// 만료 여부
    /// </summary>
    [JsonPropertyName("isExpired")]
    public bool IsExpired { get; init; }

    /// <summary>
    /// 사용자 ID
    /// </summary>
    [JsonPropertyName("userId")]
    public Guid? UserId { get; init; }

    /// <summary>
    /// 클라이언트 ID
    /// </summary>
    [JsonPropertyName("clientId")]
    public string? ClientId { get; init; }

    /// <summary>
    /// 권한 범위 목록
    /// </summary>
    [JsonPropertyName("scopes")]
    public IList<string>? Scopes { get; init; }

    /// <summary>
    /// 만료 시간
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset? ExpiresAt { get; init; }

    /// <summary>
    /// 발급 시간
    /// </summary>
    [JsonPropertyName("issuedAt")]
    public DateTimeOffset? IssuedAt { get; init; }

    /// <summary>
    /// 토큰 클레임
    /// </summary>
    [JsonPropertyName("claims")]
    public Dictionary<string, object>? Claims { get; init; }

    /// <summary>
    /// 오류 메시지
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }
}

/// <summary>
/// 토큰 폐기 요청 DTO
/// </summary>
public class RevokeTokenRequest : BaseRequest
{
    /// <summary>
    /// 폐기할 토큰
    /// </summary>
    [JsonPropertyName("token")]
    [Required(ErrorMessage = "토큰은 필수입니다.")]
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// 토큰 타입 힌트 (access_token, refresh_token)
    /// </summary>
    [JsonPropertyName("tokenTypeHint")]
    public string? TokenTypeHint { get; init; }

    /// <summary>
    /// 모든 토큰 폐기 여부
    /// </summary>
    [JsonPropertyName("revokeAll")]
    public bool RevokeAll { get; init; }

    /// <summary>
    /// 다른 디바이스 토큰도 폐기할지 여부
    /// </summary>
    [JsonPropertyName("revokeOtherDevices")]
    public bool RevokeOtherDevices { get; init; }
}

/// <summary>
/// 토큰 정보 요청 DTO (RFC 7662 - Token Introspection)
/// </summary>
public class IntrospectTokenRequest : BaseRequest
{
    /// <summary>
    /// 검사할 토큰
    /// </summary>
    [JsonPropertyName("token")]
    [Required(ErrorMessage = "토큰은 필수입니다.")]
    public string Token { get; init; } = string.Empty;

    /// <summary>
    /// 토큰 타입 힌트
    /// </summary>
    [JsonPropertyName("tokenTypeHint")]
    public string? TokenTypeHint { get; init; }
}

/// <summary>
/// 토큰 정보 응답 DTO (RFC 7662 - Token Introspection)
/// </summary>
public class IntrospectTokenResponse : BaseResponse
{
    /// <summary>
    /// 토큰 활성 상태
    /// </summary>
    [JsonPropertyName("active")]
    public bool Active { get; init; }

    /// <summary>
    /// 클라이언트 ID
    /// </summary>
    [JsonPropertyName("client_id")]
    public string? ClientId { get; init; }

    /// <summary>
    /// 사용자명
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <summary>
    /// 토큰 타입
    /// </summary>
    [JsonPropertyName("token_type")]
    public string? TokenType { get; init; }

    /// <summary>
    /// 만료 시간 (Unix timestamp)
    /// </summary>
    [JsonPropertyName("exp")]
    public long? Exp { get; init; }

    /// <summary>
    /// 발급 시간 (Unix timestamp)
    /// </summary>
    [JsonPropertyName("iat")]
    public long? Iat { get; init; }

    /// <summary>
    /// 사용 가능 시간 (Unix timestamp)
    /// </summary>
    [JsonPropertyName("nbf")]
    public long? Nbf { get; init; }

    /// <summary>
    /// 주체
    /// </summary>
    [JsonPropertyName("sub")]
    public string? Sub { get; init; }

    /// <summary>
    /// 대상
    /// </summary>
    [JsonPropertyName("aud")]
    public string? Aud { get; init; }

    /// <summary>
    /// 발급자
    /// </summary>
    [JsonPropertyName("iss")]
    public string? Iss { get; init; }

    /// <summary>
    /// JWT ID
    /// </summary>
    [JsonPropertyName("jti")]
    public string? Jti { get; init; }

    /// <summary>
    /// 권한 범위
    /// </summary>
    [JsonPropertyName("scope")]
    public string? Scope { get; init; }

    /// <summary>
    /// 추가 클레임
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalClaims { get; init; }
}

/// <summary>
/// JWT 토큰 클레임 정보
/// </summary>
public class JwtTokenClaims
{
    /// <summary>
    /// 주체 (사용자 ID)
    /// </summary>
    [JsonPropertyName("sub")]
    public string Sub { get; init; } = string.Empty;

    /// <summary>
    /// 이름
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// 이메일
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; init; }

    /// <summary>
    /// 이메일 인증 여부
    /// </summary>
    [JsonPropertyName("email_verified")]
    public bool? EmailVerified { get; init; }

    /// <summary>
    /// 역할 목록
    /// </summary>
    [JsonPropertyName("roles")]
    public IList<string>? Roles { get; init; }

    /// <summary>
    /// 권한 목록
    /// </summary>
    [JsonPropertyName("permissions")]
    public IList<string>? Permissions { get; init; }

    /// <summary>
    /// 테넌트 ID
    /// </summary>
    [JsonPropertyName("tenant_id")]
    public string? TenantId { get; init; }

    /// <summary>
    /// 세션 ID
    /// </summary>
    [JsonPropertyName("session_id")]
    public string? SessionId { get; init; }

    /// <summary>
    /// 디바이스 ID
    /// </summary>
    [JsonPropertyName("device_id")]
    public string? DeviceId { get; init; }

    /// <summary>
    /// IP 주소
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string? IpAddress { get; init; }

    /// <summary>
    /// 인증 방법
    /// </summary>
    [JsonPropertyName("auth_method")]
    public string? AuthMethod { get; init; }

    /// <summary>
    /// 인증 시간 (Unix timestamp)
    /// </summary>
    [JsonPropertyName("auth_time")]
    public long? AuthTime { get; init; }

    /// <summary>
    /// 2단계 인증 사용 여부
    /// </summary>
    [JsonPropertyName("mfa_used")]
    public bool? MfaUsed { get; init; }

    /// <summary>
    /// 위험 점수
    /// </summary>
    [JsonPropertyName("risk_score")]
    public int? RiskScore { get; init; }

    /// <summary>
    /// 추가 클레임
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, object>? AdditionalClaims { get; init; }
}