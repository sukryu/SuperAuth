namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// DDD 애그리게이트 루트 인터페이스
/// </summary>
public interface IAggregateRoot : IEntity
{
    /// <summary>
    /// 도메인 이벤트 목록
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    /// <summary>
    /// 도메인 이벤트 추가
    /// </summary>
    /// <param name="domainEvent">도메인 이벤트</param>
    void AddDomainEvent(IDomainEvent domainEvent);

    /// <summary>
    /// 도메인 이벤트 제거
    /// </summary>
    /// <param name="domainEvent">도메인 이벤트</param>
    void RemoveDomainEvent(IDomainEvent domainEvent);

    /// <summary>
    /// 모든 도메인 이벤트 삭제
    /// </summary>
    void ClearDomainEvents();
}

/// <summary>
/// 강타입 ID를 가진 애그리게이트 루트 인터페이스
/// </summary>
/// <typeparam name="TId">ID 타입</typeparam>
public interface IAggregateRoot<out TId> : IAggregateRoot, IEntity<TId>
    where TId : notnull
{
}

/// <summary>
/// 스냅샷 지원 애그리게이트 루트 인터페이스
/// </summary>
public interface ISnapshotableAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// 스냅샷 생성
    /// </summary>
    /// <returns>애그리게이트 상태 스냅샷</returns>
    object CreateSnapshot();

    /// <summary>
    /// 스냅샷에서 복원
    /// </summary>
    /// <param name="snapshot">애그리게이트 상태 스냅샷</param>
    void RestoreFromSnapshot(object snapshot);

    /// <summary>
    /// 스냅샷 버전
    /// </summary>
    int SnapshotVersion { get; }
}

/// <summary>
/// 이벤트 소싱 지원 애그리게이트 루트 인터페이스
/// </summary>
public interface IEventSourcedAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// 애그리게이트 버전 (이벤트 개수)
    /// </summary>
    long AggregateVersion { get; }

    /// <summary>
    /// 이벤트 목록에서 애그리게이트 복원
    /// </summary>
    /// <param name="events">도메인 이벤트 목록</param>
    void LoadFromHistory(IEnumerable<IDomainEvent> events);

    /// <summary>
    /// 커밋되지 않은 이벤트 목록
    /// </summary>
    IReadOnlyList<IDomainEvent> UncommittedEvents { get; }

    /// <summary>
    /// 이벤트 커밋 처리
    /// </summary>
    void MarkEventsAsCommitted();
}

/// <summary>
/// 비즈니스 규칙 검증 지원 애그리게이트 루트 인터페이스
/// </summary>
public interface IValidatableAggregateRoot : IAggregateRoot
{
    /// <summary>
    /// 비즈니스 규칙 검증
    /// </summary>
    /// <returns>검증 결과</returns>
    ValidationResult Validate();

    /// <summary>
    /// 유효한 상태인지 확인
    /// </summary>
    bool IsValid { get; }
}

/// <summary>
/// 검증 결과
/// </summary>
public class ValidationResult
{
    /// <summary>
    /// 검증 성공 여부
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// 검증 오류 목록
    /// </summary>
    public IReadOnlyList<ValidationError> Errors { get; init; } = new List<ValidationError>();

    /// <summary>
    /// 성공 결과 생성
    /// </summary>
    public static ValidationResult Success() => new() { IsValid = true };

    /// <summary>
    /// 실패 결과 생성
    /// </summary>
    /// <param name="errors">오류 목록</param>
    public static ValidationResult Failure(params ValidationError[] errors) =>
        new() { IsValid = false, Errors = errors.ToList() };

    /// <summary>
    /// 실패 결과 생성 (문자열 메시지)
    /// </summary>
    /// <param name="errorMessages">오류 메시지 목록</param>
    public static ValidationResult Failure(params string[] errorMessages) =>
        new() 
        { 
            IsValid = false, 
            Errors = errorMessages.Select(msg => new ValidationError(msg)).ToList() 
        };
}

/// <summary>
/// 검증 오류
/// </summary>
public class ValidationError
{
    /// <summary>
    /// 오류 메시지
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// 속성 이름
    /// </summary>
    public string? PropertyName { get; }

    /// <summary>
    /// 오류 코드
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// 검증 오류 생성
    /// </summary>
    /// <param name="message">오류 메시지</param>
    /// <param name="propertyName">속성 이름</param>
    /// <param name="errorCode">오류 코드</param>
    public ValidationError(string message, string? propertyName = null, string? errorCode = null)
    {
        Message = message ?? throw new ArgumentNullException(nameof(message));
        PropertyName = propertyName;
        ErrorCode = errorCode;
    }

    public override string ToString() => 
        string.IsNullOrEmpty(PropertyName) ? Message : $"{PropertyName}: {Message}";
}