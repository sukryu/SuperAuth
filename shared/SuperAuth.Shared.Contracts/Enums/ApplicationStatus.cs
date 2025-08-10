namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 애플리케이션 상태를 정의하는 열거형
/// </summary>
public enum ApplicationStatus
{
    /// <summary>
    /// 초안 - 생성 중이지만 아직 활성화되지 않음
    /// </summary>
    Draft = 0,

    /// <summary>
    /// 활성 - 정상적으로 작동 중
    /// </summary>
    Active = 1,

    /// <summary>
    /// 비활성 - 일시적으로 비활성화됨
    /// </summary>
    Inactive = 2,

    /// <summary>
    /// 일시 중단 - 보안상 이유로 일시 중단됨
    /// </summary>
    Suspended = 3,

    /// <summary>
    /// 유지보수 모드 - 업데이트 또는 유지보수 중
    /// </summary>
    Maintenance = 4,

    /// <summary>
    /// 승인 대기 - 관리자 승인 대기 중
    /// </summary>
    PendingApproval = 5,

    /// <summary>
    /// 거부됨 - 승인이 거부됨
    /// </summary>
    Rejected = 6,

    /// <summary>
    /// 만료됨 - 유효 기간이 만료됨
    /// </summary>
    Expired = 7,

    /// <summary>
    /// 삭제됨 - 소프트 삭제됨
    /// </summary>
    Deleted = 8,

    /// <summary>
    /// 보관됨 - 아카이브됨 (읽기 전용)
    /// </summary>
    Archived = 9,

    /// <summary>
    /// 마이그레이션 중 - 다른 시스템으로 마이그레이션 중
    /// </summary>
    Migrating = 10,

    /// <summary>
    /// 테스트 모드 - 테스트/개발 환경에서만 사용
    /// </summary>
    Testing = 11
}