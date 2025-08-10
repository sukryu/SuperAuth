namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 작업 단위 패턴 인터페이스
/// </summary>
public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// 트랜잭션 시작
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 변경사항 저장
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>영향받은 행 수</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 트랜잭션 커밋
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 트랜잭션 롤백
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 트랜잭션 활성 여부
    /// </summary>
    bool HasActiveTransaction { get; }

    /// <summary>
    /// 저장소 조회
    /// </summary>
    /// <typeparam name="TEntity">엔티티 타입</typeparam>
    /// <returns>저장소 인스턴스</returns>
    IRepository<TEntity> Repository<TEntity>() where TEntity : class, IEntity;

    /// <summary>
    /// 강타입 ID 저장소 조회
    /// </summary>
    /// <typeparam name="TEntity">엔티티 타입</typeparam>
    /// <typeparam name="TId">ID 타입</typeparam>
    /// <returns>저장소 인스턴스</returns>
    IRepository<TEntity, TId> Repository<TEntity, TId>() 
        where TEntity : class, IEntity<TId>
        where TId : notnull;
}

/// <summary>
/// 도메인 이벤트 지원 작업 단위 인터페이스
/// </summary>
public interface IDomainEventUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// 도메인 이벤트 발행과 함께 변경사항 저장
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>영향받은 행 수</returns>
    Task<int> SaveChangesAndPublishEventsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 수집된 도메인 이벤트 목록 조회
    /// </summary>
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    /// <summary>
    /// 도메인 이벤트 목록 삭제
    /// </summary>
    void ClearDomainEvents();
}

/// <summary>
/// 분산 트랜잭션 지원 작업 단위 인터페이스
/// </summary>
public interface IDistributedUnitOfWork : IUnitOfWork
{
    /// <summary>
    /// 분산 트랜잭션 시작
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task BeginDistributedTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 분산 트랜잭션 커밋
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task CommitDistributedTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 분산 트랜잭션 롤백
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RollbackDistributedTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 외부 리소스 등록 (예: 메시지 큐, 외부 API)
    /// </summary>
    /// <param name="resource">외부 리소스</param>
    void EnlistResource(ITransactionalResource resource);
}

/// <summary>
/// 트랜잭션 리소스 인터페이스
/// </summary>
public interface ITransactionalResource
{
    /// <summary>
    /// 리소스 이름
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 트랜잭션 준비
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task PrepareAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 트랜잭션 커밋
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 트랜잭션 롤백
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RollbackAsync(CancellationToken cancellationToken = default);
}