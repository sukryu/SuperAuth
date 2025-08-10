using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using SuperAuth.Shared.Contracts.DTOs.Common;
using SuperAuth.Shared.Contracts.Enums;

namespace SuperAuth.Shared.Contracts.DTOs.Authentication;

/// <summary>
/// 로그인 요청 DTO
/// </summary>
public class LoginRequest : BaseRequest
{
    /// <summary>
    /// 이메일 주소 또는 사용자명
    /// </summary>
    [JsonPropertyName("identifier")]
    [Required(ErrorMessage = "이메일 또는 사용자명은 필수입니다.")]
    [StringLength(255, ErrorMessage = "식별자는 255자를 초과할 수 없습니다.")]
    public string Identifier { get; init; } = string.Empty;

    /// <summary>
    /// 비밀번호
    /// </summary>
    [JsonPropertyName("password")]
    [Required(ErrorMessage = "비밀번호는 필수입니다.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "비밀번호는 1-100자 사이여야 합니다.")]
    public string Password { get; init; } = string.Empty;

    /// <summary>
    /// 로그인 유지 여부
    /// </summary>
    [JsonPropertyName("rememberMe")]
    public bool RememberMe { get; init; }

    /// <summary>
    /// 2단계 인증 코드 (선택적)
    /// </summary>
    [JsonPropertyName("mfaCode")]
    [StringLength(10, ErrorMessage = "2단계 인증 코드는 10자를 초과할 수 없습니다.")]
    public string? MfaCode { get; init; }

    /// <summary>
    /// 2단계 인증 방법
    /// </summary>
    [JsonPropertyName("mfaMethod")]
    public AuthenticationMethod? MfaMethod { get; init; }

    /// <summary>
    /// 백업 코드 (2단계 인증 대신 사용)
    /// </summary>
    [JsonPropertyName("backupCode")]
    [StringLength(20, ErrorMessage = "백업 코드는 20자를 초과할 수 없습니다.")]
    public string? BackupCode { get; init; }

    /// <summary>
    /// reCAPTCHA 토큰 (선택적)
    /// </summary>
    [JsonPropertyName("recaptchaToken")]
    public string? RecaptchaToken { get; init; }

    /// <summary>
    /// 디바이스 신뢰 여부
    /// </summary>
    [JsonPropertyName("trustDevice")]
    public bool TrustDevice { get; init; }

    /// <summary>
    /// 리디렉션 URL (로그인 후 이동할 URL)
    /// </summary>
    [JsonPropertyName("redirectUrl")]
    [Url(ErrorMessage = "올바른 URL 형식이 아닙니다.")]
    public string? RedirectUrl { get; init; }
}

/// <summary>
/// 로그인 응답 DTO
/// </summary>
public class LoginResponse : BaseResponse
{
    /// <summary>
    /// 로그인 성공 여부
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// 액세스 토큰
    /// </summary>
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; init; }

    /// <summary>
    /// 리프레시 토큰
    /// </summary>
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; init; }

    /// <summary>
    /// 토큰 만료 시간 (UTC)
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset? ExpiresAt { get; init; }

    /// <summary>
    /// 토큰 타입 (Bearer)
    /// </summary>
    [JsonPropertyName("tokenType")]
    public string TokenType { get; init; } = "Bearer";

    /// <summary>
    /// 권한 범위 목록
    /// </summary>
    [JsonPropertyName("scopes")]
    public IList<string>? Scopes { get; init; }

    /// <summary>
    /// 사용자 정보
    /// </summary>
    [JsonPropertyName("user")]
    public UserInfo? User { get; init; }

    /// <summary>
    /// 2단계 인증 필요 여부
    /// </summary>
    [JsonPropertyName("mfaRequired")]
    public bool MfaRequired { get; init; }

    /// <summary>
    /// 사용 가능한 2단계 인증 방법 목록
    /// </summary>
    [JsonPropertyName("availableMfaMethods")]
    public IList<AuthenticationMethod>? AvailableMfaMethods { get; init; }

    /// <summary>
    /// 2단계 인증 토큰 (MFA 완료를 위한 임시 토큰)
    /// </summary>
    [JsonPropertyName("mfaToken")]
    public string? MfaToken { get; init; }

    /// <summary>
    /// 비밀번호 변경 필요 여부
    /// </summary>
    [JsonPropertyName("passwordChangeRequired")]
    public bool PasswordChangeRequired { get; init; }

    /// <summary>
    /// 이용약관 동의 필요 여부
    /// </summary>
    [JsonPropertyName("termsAcceptanceRequired")]
    public bool TermsAcceptanceRequired { get; init; }

    /// <summary>
    /// 계정 잠금 여부
    /// </summary>
    [JsonPropertyName("accountLocked")]
    public bool AccountLocked { get; init; }

    /// <summary>
    /// 계정 잠금 해제 시간 (UTC)
    /// </summary>
    [JsonPropertyName("lockoutEndsAt")]
    public DateTimeOffset? LockoutEndsAt { get; init; }

    /// <summary>
    /// 남은 로그인 시도 횟수
    /// </summary>
    [JsonPropertyName("remainingLoginAttempts")]
    public int? RemainingLoginAttempts { get; init; }

    /// <summary>
    /// 세션 ID
    /// </summary>
    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    /// <summary>
    /// 디바이스 등록 여부
    /// </summary>
    [JsonPropertyName("deviceRegistered")]
    public bool DeviceRegistered { get; init; }

    /// <summary>
    /// 보안 정보
    /// </summary>
    [JsonPropertyName("securityInfo")]
    public SecurityInfo? SecurityInfo { get; init; }

    /// <summary>
    /// 성공 로그인 응답 생성
    /// </summary>
    public static LoginResponse Success(
        string accessToken,
        string refreshToken,
        DateTimeOffset expiresAt,
        UserInfo user,
        string? sessionId = null)
    {
        return new LoginResponse
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt,
            User = user,
            SessionId = sessionId
        };
    }

    /// <summary>
    /// MFA 필요 응답 생성
    /// </summary>
    public static LoginResponse RequireMfa(
        IList<AuthenticationMethod> availableMethods,
        string mfaToken)
    {
        return new LoginResponse
        {
            Success = false,
            MfaRequired = true,
            AvailableMfaMethods = availableMethods,
            MfaToken = mfaToken
        };
    }

    /// <summary>
    /// 계정 잠금 응답 생성
    /// </summary>
    public static LoginResponse AccountLocked(DateTimeOffset? lockoutEndsAt = null)
    {
        return new LoginResponse
        {
            Success = false,
            AccountLocked = true,
            LockoutEndsAt = lockoutEndsAt
        };
    }
}

/// <summary>
/// 사용자 정보 DTO
/// </summary>
public class UserInfo
{
    /// <summary>
    /// 사용자 ID
    /// </summary>
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    /// <summary>
    /// 이메일 주소
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// 사용자명
    /// </summary>
    [JsonPropertyName("username")]
    public string? Username { get; init; }

    /// <summary>
    /// 표시 이름
    /// </summary>
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; init; }

    /// <summary>
    /// 이름
    /// </summary>
    [JsonPropertyName("firstName")]
    public string? FirstName { get; init; }

    /// <summary>
    /// 성
    /// </summary>
    [JsonPropertyName("lastName")]
    public string? LastName { get; init; }

    /// <summary>
    /// 전체 이름
    /// </summary>
    [JsonPropertyName("fullName")]
    public string? FullName => $"{FirstName} {LastName}".Trim();

    /// <summary>
    /// 프로필 이미지 URL
    /// </summary>
    [JsonPropertyName("profileImageUrl")]
    public string? ProfileImageUrl { get; init; }

    /// <summary>
    /// 사용자 역할 목록
    /// </summary>
    [JsonPropertyName("roles")]
    public IList<string> Roles { get; init; } = new List<string>();

    /// <summary>
    /// 권한 목록
    /// </summary>
    [JsonPropertyName("permissions")]
    public IList<string> Permissions { get; init; } = new List<string>();

    /// <summary>
    /// 이메일 인증 여부
    /// </summary>
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; init; }

    /// <summary>
    /// 전화번호 인증 여부
    /// </summary>
    [JsonPropertyName("phoneVerified")]
    public bool PhoneVerified { get; init; }

    /// <summary>
    /// 2단계 인증 활성화 여부
    /// </summary>
    [JsonPropertyName("mfaEnabled")]
    public bool MfaEnabled { get; init; }

    /// <summary>
    /// 마지막 로그인 시간
    /// </summary>
    [JsonPropertyName("lastLoginAt")]
    public DateTimeOffset? LastLoginAt { get; init; }

    /// <summary>
    /// 시간대
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; init; }

    /// <summary>
    /// 언어 설정
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; init; }

    /// <summary>
    /// 테넌트 ID (멀티테넌트 환경)
    /// </summary>
    [JsonPropertyName("tenantId")]
    public Guid? TenantId { get; init; }
}

/// <summary>
/// 보안 정보 DTO
/// </summary>
public class SecurityInfo
{
    /// <summary>
    /// 위험 점수 (0-100)
    /// </summary>
    [JsonPropertyName("riskScore")]
    public int RiskScore { get; init; }

    /// <summary>
    /// 위험 레벨
    /// </summary>
    [JsonPropertyName("riskLevel")]
    public ThreatLevel RiskLevel { get; init; }

    /// <summary>
    /// 새로운 디바이스 여부
    /// </summary>
    [JsonPropertyName("isNewDevice")]
    public bool IsNewDevice { get; init; }

    /// <summary>
    /// 새로운 위치 여부
    /// </summary>
    [JsonPropertyName("isNewLocation")]
    public bool IsNewLocation { get; init; }

    /// <summary>
    /// 의심스러운 활동 감지 여부
    /// </summary>
    [JsonPropertyName("suspiciousActivity")]
    public bool SuspiciousActivity { get; init; }

    /// <summary>
    /// 보안 권장사항
    /// </summary>
    [JsonPropertyName("recommendations")]
    public IList<string>? Recommendations { get; init; }

    /// <summary>
    /// 추가 인증 권장 여부
    /// </summary>
    [JsonPropertyName("additionalAuthRecommended")]
    public bool AdditionalAuthRecommended { get; init; }
}