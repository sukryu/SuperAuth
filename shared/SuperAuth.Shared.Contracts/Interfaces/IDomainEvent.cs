namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 도메인 이벤트 인터페이스
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// 이벤트 ID
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// 이벤트 발생 시간 (UTC)
    /// </summary>
    DateTimeOffset OccurredAt { get; }

    /// <summary>
    /// 이벤트 타입명
    /// </summary>
    string EventType { get; }

    /// <summary>
    /// 이벤트 버전
    /// </summary>
    int EventVersion { get; }

    /// <summary>
    /// 애그리게이트 ID
    /// </summary>
    Guid AggregateId { get; }

    /// <summary>
    /// 애그리게이트 타입
    /// </summary>
    string AggregateType { get; }

    /// <summary>
    /// 애그리게이트 버전 (이벤트 순서)
    /// </summary>
    long AggregateVersion { get; }

    /// <summary>
    /// 이벤트를 발생시킨 사용자 ID
    /// </summary>
    Guid? UserId { get; }

    /// <summary>
    /// 이벤트 메타데이터
    /// </summary>
    IReadOnlyDictionary<string, object> Metadata { get; }
}

/// <summary>
/// 강타입 애그리게이트 ID를 가진 도메인 이벤트 인터페이스
/// </summary>
/// <typeparam name="TAggregateId">애그리게이트 ID 타입</typeparam>
public interface IDomainEvent<out TAggregateId> : IDomainEvent
    where TAggregateId : notnull
{
    /// <summary>
    /// 강타입 애그리게이트 ID
    /// </summary>
    new TAggregateId AggregateId { get; }
}

/// <summary>
/// 도메인 이벤트 핸들러 인터페이스
/// </summary>
/// <typeparam name="TDomainEvent">처리할 도메인 이벤트 타입</typeparam>
public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// 도메인 이벤트 처리
    /// </summary>
    /// <param name="domainEvent">도메인 이벤트</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// 도메인 이벤트 퍼블리셔 인터페이스
/// </summary>
public interface IDomainEventPublisher
{
    /// <summary>
    /// 도메인 이벤트 발행
    /// </summary>
    /// <param name="domainEvent">도메인 이벤트</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// 여러 도메인 이벤트 일괄 발행
    /// </summary>
    /// <param name="domainEvents">도메인 이벤트 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task PublishAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
}

/// <summary>
/// 도메인 이벤트 저장소 인터페이스
/// </summary>
public interface IDomainEventStore
{
    /// <summary>
    /// 이벤트 저장
    /// </summary>
    /// <param name="domainEvent">도메인 이벤트</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SaveAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// 이벤트 목록 저장
    /// </summary>
    /// <param name="domainEvents">도메인 이벤트 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SaveAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);

    /// <summary>
    /// 애그리게이트의 이벤트 목록 조회
    /// </summary>
    /// <param name="aggregateId">애그리게이트 ID</param>
    /// <param name="fromVersion">시작 버전</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<IDomainEvent>> GetEventsAsync(
        Guid aggregateId, 
        long? fromVersion = null, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 이벤트 스트림 조회 (페이징)
    /// </summary>
    /// <param name="streamName">스트림 이름</param>
    /// <param name="fromEventNumber">시작 이벤트 번호</param>
    /// <param name="maxCount">최대 개수</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<IDomainEvent>> GetEventStreamAsync(
        string streamName,
        long fromEventNumber = 0,
        int maxCount = 100,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// 통합 이벤트 인터페이스 (마이크로서비스 간 통신용)
/// </summary>
public interface IIntegrationEvent
{
    /// <summary>
    /// 이벤트 ID
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// 이벤트 발생 시간 (UTC)
    /// </summary>
    DateTimeOffset OccurredAt { get; }

    /// <summary>
    /// 이벤트 타입명
    /// </summary>
    string EventType { get; }

    /// <summary>
    /// 이벤트 버전
    /// </summary>
    int EventVersion { get; }

    /// <summary>
    /// 발행자 서비스명
    /// </summary>
    string PublisherService { get; }

    /// <summary>
    /// 상관관계 ID (추적용)
    /// </summary>
    string? CorrelationId { get; }

    /// <summary>
    /// 이벤트 메타데이터
    /// </summary>
    IReadOnlyDictionary<string, object> Metadata { get; }
}

/// <summary>
/// 통합 이벤트 핸들러 인터페이스
/// </summary>
/// <typeparam name="TIntegrationEvent">처리할 통합 이벤트 타입</typeparam>
public interface IIntegrationEventHandler<in TIntegrationEvent>
    where TIntegrationEvent : IIntegrationEvent
{
    /// <summary>
    /// 통합 이벤트 처리
    /// </summary>
    /// <param name="integrationEvent">통합 이벤트</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task HandleAsync(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);
}

/// <summary>
/// 통합 이벤트 버스 인터페이스
/// </summary>
public interface IIntegrationEventBus
{
    /// <summary>
    /// 통합 이벤트 발행
    /// </summary>
    /// <param name="integrationEvent">통합 이벤트</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task PublishAsync(IIntegrationEvent integrationEvent, CancellationToken cancellationToken = default);

    /// <summary>
    /// 통합 이벤트 구독
    /// </summary>
    /// <typeparam name="TIntegrationEvent">구독할 이벤트 타입</typeparam>
    /// <typeparam name="THandler">핸들러 타입</typeparam>
    void Subscribe<TIntegrationEvent, THandler>()
        where TIntegrationEvent : IIntegrationEvent
        where THandler : IIntegrationEventHandler<TIntegrationEvent>;

    /// <summary>
    /// 통합 이벤트 구독 해제
    /// </summary>
    /// <typeparam name="TIntegrationEvent">구독 해제할 이벤트 타입</typeparam>
    /// <typeparam name="THandler">핸들러 타입</typeparam>
    void Unsubscribe<TIntegrationEvent, THandler>()
        where TIntegrationEvent : IIntegrationEvent
        where THandler : IIntegrationEventHandler<TIntegrationEvent>;
}