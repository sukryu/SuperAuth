namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 사용자 역할을 정의하는 열거형
/// </summary>
public enum UserRole
{
    /// <summary>
    /// 게스트 사용자 (임시 접근)
    /// </summary>
    Guest = 0,

    /// <summary>
    /// 일반 사용자 (기본 권한)
    /// </summary>
    User = 1,

    /// <summary>
    /// 모더레이터 (중간 관리 권한)
    /// </summary>
    Moderator = 2,

    /// <summary>
    /// 관리자 (고급 관리 권한)
    /// </summary>
    Administrator = 3,

    /// <summary>
    /// 시스템 관리자 (최고 권한)
    /// </summary>
    SystemAdministrator = 4,

    /// <summary>
    /// 서비스 계정 (API 전용)
    /// </summary>
    ServiceAccount = 5,

    /// <summary>
    /// 감사자 (읽기 전용 접근)
    /// </summary>
    Auditor = 6,

    /// <summary>
    /// 보안 분석가 (보안 데이터 접근)
    /// </summary>
    SecurityAnalyst = 7
}