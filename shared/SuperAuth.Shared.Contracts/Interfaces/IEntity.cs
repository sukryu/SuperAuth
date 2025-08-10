namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 기본 엔티티 인터페이스
/// </summary>
public interface IEntity
{
    /// <summary>
    /// 엔티티 ID
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// 생성 일시 (UTC)
    /// </summary>
    DateTimeOffset CreatedAt { get; }

    /// <summary>
    /// 마지막 수정 일시 (UTC)
    /// </summary>
    DateTimeOffset UpdatedAt { get; }
}

/// <summary>
/// 강타입 ID를 가진 엔티티 인터페이스
/// </summary>
/// <typeparam name="TId">ID 타입</typeparam>
public interface IEntity<out TId> : IEntity
    where TId : notnull
{
    /// <summary>
    /// 강타입 엔티티 ID
    /// </summary>
    new TId Id { get; }
}

/// <summary>
/// 생성자 정보를 포함하는 엔티티 인터페이스
/// </summary>
public interface ICreatableEntity : IEntity
{
    /// <summary>
    /// 생성자 ID
    /// </summary>
    Guid? CreatedBy { get; }
}

/// <summary>
/// 수정자 정보를 포함하는 엔티티 인터페이스
/// </summary>
public interface IModifiableEntity : IEntity
{
    /// <summary>
    /// 마지막 수정자 ID
    /// </summary>
    Guid? UpdatedBy { get; }
}

/// <summary>
/// 완전한 감사 정보를 포함하는 엔티티 인터페이스
/// </summary>
public interface IAuditableEntity : ICreatableEntity, IModifiableEntity
{
}

/// <summary>
/// 소프트 삭제 가능한 엔티티 인터페이스
/// </summary>
public interface ISoftDeletableEntity : IEntity
{
    /// <summary>
    /// 삭제 여부
    /// </summary>
    bool IsDeleted { get; }

    /// <summary>
    /// 삭제 일시 (UTC)
    /// </summary>
    DateTimeOffset? DeletedAt { get; }

    /// <summary>
    /// 삭제자 ID
    /// </summary>
    Guid? DeletedBy { get; }

    /// <summary>
    /// 소프트 삭제 수행
    /// </summary>
    /// <param name="deletedBy">삭제자 ID</param>
    void SoftDelete(Guid? deletedBy = null);

    /// <summary>
    /// 소프트 삭제 복원
    /// </summary>
    void Restore();
}

/// <summary>
/// 버전 관리가 가능한 엔티티 인터페이스
/// </summary>
public interface IVersionableEntity : IEntity
{
    /// <summary>
    /// 엔티티 버전 (동시성 제어용)
    /// </summary>
    long Version { get; }

    /// <summary>
    /// 버전 증가
    /// </summary>
    void IncrementVersion();
}

/// <summary>
/// 테넌트별 분리가 가능한 엔티티 인터페이스
/// </summary>
public interface ITenantEntity : IEntity
{
    /// <summary>
    /// 테넌트 ID
    /// </summary>
    Guid TenantId { get; }
}

/// <summary>
/// 활성화/비활성화가 가능한 엔티티 인터페이스
/// </summary>
public interface IActivatableEntity : IEntity
{
    /// <summary>
    /// 활성화 여부
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// 활성화
    /// </summary>
    void Activate();

    /// <summary>
    /// 비활성화
    /// </summary>
    void Deactivate();
}

/// <summary>
/// 만료 가능한 엔티티 인터페이스
/// </summary>
public interface IExpirableEntity : IEntity
{
    /// <summary>
    /// 만료 일시 (UTC)
    /// </summary>
    DateTimeOffset? ExpiresAt { get; }

    /// <summary>
    /// 만료 여부 확인
    /// </summary>
    bool IsExpired { get; }

    /// <summary>
    /// 만료 시간 설정
    /// </summary>
    /// <param name="expiresAt">만료 일시</param>
    void SetExpiration(DateTimeOffset? expiresAt);
}

/// <summary>
/// 모든 기능을 포함한 완전한 엔티티 인터페이스
/// </summary>
public interface IFullAuditableEntity : 
    IAuditableEntity, 
    ISoftDeletableEntity, 
    IVersionableEntity, 
    IActivatableEntity
{
}