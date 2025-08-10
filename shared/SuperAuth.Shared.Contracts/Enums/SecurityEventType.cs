namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 보안 이벤트 유형을 정의하는 열거형
/// </summary>
public enum SecurityEventType
{
    /// <summary>
    /// 알 수 없는 이벤트
    /// </summary>
    Unknown = 0,

    // ===== 인증 관련 이벤트 =====
    /// <summary>
    /// 로그인 성공
    /// </summary>
    LoginSuccess = 100,

    /// <summary>
    /// 로그인 실패
    /// </summary>
    LoginFailure = 101,

    /// <summary>
    /// 로그아웃
    /// </summary>
    Logout = 102,

    /// <summary>
    /// 토큰 갱신
    /// </summary>
    TokenRefresh = 103,

    /// <summary>
    /// 토큰 만료
    /// </summary>
    TokenExpired = 104,

    /// <summary>
    /// 토큰 취소
    /// </summary>
    TokenRevoked = 105,

    /// <summary>
    /// MFA 성공
    /// </summary>
    MfaSuccess = 106,

    /// <summary>
    /// MFA 실패
    /// </summary>
    MfaFailure = 107,

    /// <summary>
    /// MFA 설정 변경
    /// </summary>
    MfaConfigurationChanged = 108,

    /// <summary>
    /// 비밀번호 변경
    /// </summary>
    PasswordChanged = 109,

    /// <summary>
    /// 비밀번호 재설정 요청
    /// </summary>
    PasswordResetRequested = 110,

    /// <summary>
    /// 비밀번호 재설정 완료
    /// </summary>
    PasswordResetCompleted = 111,

    // ===== 계정 관리 이벤트 =====
    /// <summary>
    /// 사용자 계정 생성
    /// </summary>
    UserAccountCreated = 200,

    /// <summary>
    /// 사용자 계정 수정
    /// </summary>
    UserAccountModified = 201,

    /// <summary>
    /// 사용자 계정 삭제
    /// </summary>
    UserAccountDeleted = 202,

    /// <summary>
    /// 사용자 계정 잠금
    /// </summary>
    UserAccountLocked = 203,

    /// <summary>
    /// 사용자 계정 잠금 해제
    /// </summary>
    UserAccountUnlocked = 204,

    /// <summary>
    /// 사용자 계정 비활성화
    /// </summary>
    UserAccountDisabled = 205,

    /// <summary>
    /// 사용자 계정 활성화
    /// </summary>
    UserAccountEnabled = 206,

    /// <summary>
    /// 이메일 주소 변경
    /// </summary>
    EmailAddressChanged = 207,

    /// <summary>
    /// 이메일 인증
    /// </summary>
    EmailVerified = 208,

    /// <summary>
    /// 전화번호 인증
    /// </summary>
    PhoneNumberVerified = 209,

    // ===== 권한 관리 이벤트 =====
    /// <summary>
    /// 역할 할당
    /// </summary>
    RoleAssigned = 300,

    /// <summary>
    /// 역할 제거
    /// </summary>
    RoleRemoved = 301,

    /// <summary>
    /// 권한 부여
    /// </summary>
    PermissionGranted = 302,

    /// <summary>
    /// 권한 취소
    /// </summary>
    PermissionRevoked = 303,

    /// <summary>
    /// 권한 승급
    /// </summary>
    PrivilegeEscalation = 304,

    // ===== 세션 관리 이벤트 =====
    /// <summary>
    /// 세션 생성
    /// </summary>
    SessionCreated = 400,

    /// <summary>
    /// 세션 만료
    /// </summary>
    SessionExpired = 401,

    /// <summary>
    /// 세션 종료
    /// </summary>
    SessionTerminated = 402,

    /// <summary>
    /// 동시 세션 제한 초과
    /// </summary>
    ConcurrentSessionLimitExceeded = 403,

    /// <summary>
    /// 의심스러운 세션 활동
    /// </summary>
    SuspiciousSessionActivity = 404,

    // ===== 위협 탐지 이벤트 =====
    /// <summary>
    /// 브루트 포스 공격 탐지
    /// </summary>
    BruteForceAttackDetected = 500,

    /// <summary>
    /// 자격 증명 스터핑 공격 탐지
    /// </summary>
    CredentialStuffingDetected = 501,

    /// <summary>
    /// 비정상적인 로그인 위치
    /// </summary>
    AnomalousLoginLocation = 502,

    /// <summary>
    /// 비정상적인 로그인 시간
    /// </summary>
    AnomalousLoginTime = 503,

    /// <summary>
    /// 의심스러운 디바이스
    /// </summary>
    SuspiciousDevice = 504,

    /// <summary>
    /// 의심스러운 IP 주소
    /// </summary>
    SuspiciousIpAddress = 505,

    /// <summary>
    /// 봇 활동 탐지
    /// </summary>
    BotActivityDetected = 506,

    /// <summary>
    /// 계정 탈취 시도
    /// </summary>
    AccountTakeoverAttempt = 507,

    /// <summary>
    /// 피싱 시도 탐지
    /// </summary>
    PhishingAttemptDetected = 508,

    /// <summary>
    /// 멀웨어 감염 의심
    /// </summary>
    MalwareInfectionSuspected = 509,

    // ===== 시스템 보안 이벤트 =====
    /// <summary>
    /// 보안 정책 위반
    /// </summary>
    SecurityPolicyViolation = 600,

    /// <summary>
    /// 데이터 유출 시도
    /// </summary>
    DataExfiltrationAttempt = 601,

    /// <summary>
    /// 무단 API 접근
    /// </summary>
    UnauthorizedApiAccess = 602,

    /// <summary>
    /// 비정상적인 데이터 접근
    /// </summary>
    AnomalousDataAccess = 603,

    /// <summary>
    /// 시스템 구성 변경
    /// </summary>
    SystemConfigurationChanged = 604,

    /// <summary>
    /// 보안 감사 실패
    /// </summary>
    SecurityAuditFailure = 605,

    /// <summary>
    /// 암호화 키 타협
    /// </summary>
    CryptographicKeyCompromised = 606,

    // ===== 컴플라이언스 이벤트 =====
    /// <summary>
    /// GDPR 개인정보 접근 요청
    /// </summary>
    GdprDataAccessRequest = 700,

    /// <summary>
    /// GDPR 개인정보 삭제 요청
    /// </summary>
    GdprDataDeletionRequest = 701,

    /// <summary>
    /// 감사 로그 접근
    /// </summary>
    AuditLogAccessed = 702,

    /// <summary>
    /// 컴플라이언스 보고서 생성
    /// </summary>
    ComplianceReportGenerated = 703,

    // ===== 관리자 이벤트 =====
    /// <summary>
    /// 관리자 로그인
    /// </summary>
    AdministratorLogin = 800,

    /// <summary>
    /// 관리자 작업 수행
    /// </summary>
    AdministratorActionPerformed = 801,

    /// <summary>
    /// 백업 생성
    /// </summary>
    BackupCreated = 802,

    /// <summary>
    /// 시스템 복원
    /// </summary>
    SystemRestored = 803,

    /// <summary>
    /// 긴급 접근 모드 활성화
    /// </summary>
    EmergencyAccessActivated = 804
}