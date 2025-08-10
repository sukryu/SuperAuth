namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 인증 방법을 정의하는 열거형
/// </summary>
[Flags]
public enum AuthenticationMethod
{
    /// <summary>
    /// 알 수 없는 인증 방법
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 비밀번호 기반 인증
    /// </summary>
    Password = 1 << 0,

    /// <summary>
    /// 이메일 기반 인증 (매직 링크)
    /// </summary>
    Email = 1 << 1,

    /// <summary>
    /// SMS 기반 인증
    /// </summary>
    Sms = 1 << 2,

    /// <summary>
    /// Google OAuth 인증
    /// </summary>
    GoogleOAuth = 1 << 3,

    /// <summary>
    /// Microsoft OAuth 인증
    /// </summary>
    MicrosoftOAuth = 1 << 4,

    /// <summary>
    /// GitHub OAuth 인증
    /// </summary>
    GitHubOAuth = 1 << 5,

    /// <summary>
    /// Facebook OAuth 인증
    /// </summary>
    FacebookOAuth = 1 << 6,

    /// <summary>
    /// Apple Sign-In 인증
    /// </summary>
    AppleSignIn = 1 << 7,

    /// <summary>
    /// TOTP (Time-based One-Time Password) - Google Authenticator 등
    /// </summary>
    Totp = 1 << 8,

    /// <summary>
    /// WebAuthn (FIDO2) - 생체인증, 보안키 등
    /// </summary>
    WebAuthn = 1 << 9,

    /// <summary>
    /// 푸시 알림 기반 인증
    /// </summary>
    PushNotification = 1 << 10,

    /// <summary>
    /// 백업 코드 인증
    /// </summary>
    BackupCode = 1 << 11,

    /// <summary>
    /// 생체 인증 (지문, 얼굴 등)
    /// </summary>
    Biometric = 1 << 12,

    /// <summary>
    /// 하드웨어 토큰 (YubiKey 등)
    /// </summary>
    HardwareToken = 1 << 13,

    /// <summary>
    /// SAML SSO 인증
    /// </summary>
    SamlSso = 1 << 14,

    /// <summary>
    /// LDAP/Active Directory 인증
    /// </summary>
    Ldap = 1 << 15,

    /// <summary>
    /// API 키 기반 인증
    /// </summary>
    ApiKey = 1 << 16,

    /// <summary>
    /// 인증서 기반 인증
    /// </summary>
    Certificate = 1 << 17
}