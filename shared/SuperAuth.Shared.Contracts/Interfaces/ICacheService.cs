namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 캐시 서비스 인터페이스
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// 캐시에서 값 조회
    /// </summary>
    /// <typeparam name="T">값 타입</typeparam>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>캐시된 값 (없으면 default)</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시에 값 저장
    /// </summary>
    /// <typeparam name="T">값 타입</typeparam>
    /// <param name="key">캐시 키</param>
    /// <param name="value">저장할 값</param>
    /// <param name="expiration">만료 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시에서 값 제거
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시 키 존재 여부 확인
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>존재 여부</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 패턴에 맞는 키 목록 조회
    /// </summary>
    /// <param name="pattern">패턴 (예: "user:*")</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>키 목록</returns>
    Task<IEnumerable<string>> GetKeysAsync(string pattern, CancellationToken cancellationToken = default);

    /// <summary>
    /// 패턴에 맞는 키들 일괄 삭제
    /// </summary>
    /// <param name="pattern">패턴 (예: "user:*")</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>삭제된 키 개수</returns>
    Task<long> RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시 만료 시간 설정
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="expiration">만료 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SetExpirationAsync(string key, TimeSpan expiration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시 만료 시간 조회
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>남은 만료 시간 (만료되지 않으면 null)</returns>
    Task<TimeSpan?> GetExpirationAsync(string key, CancellationToken cancellationToken = default);
}

/// <summary>
/// 분산 캐시 서비스 인터페이스
/// </summary>
public interface IDistributedCacheService : ICacheService
{
    /// <summary>
    /// 분산 락 획득
    /// </summary>
    /// <param name="key">락 키</param>
    /// <param name="expiration">락 만료 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>락 인스턴스 (실패시 null)</returns>
    Task<IDistributedLock?> AcquireLockAsync(string key, TimeSpan expiration, CancellationToken cancellationToken = default);

    /// <summary>
    /// 원자적 증가 연산
    /// </summary>
    /// <param name="key">키</param>
    /// <param name="increment">증가값 (기본: 1)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>증가 후 값</returns>
    Task<long> IncrementAsync(string key, long increment = 1, CancellationToken cancellationToken = default);

    /// <summary>
    /// 원자적 감소 연산
    /// </summary>
    /// <param name="key">키</param>
    /// <param name="decrement">감소값 (기본: 1)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>감소 후 값</returns>
    Task<long> DecrementAsync(string key, long decrement = 1, CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건부 설정 (키가 없을 때만 설정)
    /// </summary>
    /// <typeparam name="T">값 타입</typeparam>
    /// <param name="key">캐시 키</param>
    /// <param name="value">저장할 값</param>
    /// <param name="expiration">만료 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>설정 성공 여부</returns>
    Task<bool> SetIfNotExistsAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 해시 필드 설정
    /// </summary>
    /// <param name="hashKey">해시 키</param>
    /// <param name="field">필드명</param>
    /// <param name="value">값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SetHashFieldAsync(string hashKey, string field, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 해시 필드 조회
    /// </summary>
    /// <param name="hashKey">해시 키</param>
    /// <param name="field">필드명</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>필드 값</returns>
    Task<string?> GetHashFieldAsync(string hashKey, string field, CancellationToken cancellationToken = default);

    /// <summary>
    /// 해시 전체 조회
    /// </summary>
    /// <param name="hashKey">해시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>해시 딕셔너리</returns>
    Task<Dictionary<string, string>> GetHashAsync(string hashKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// 리스트에 아이템 추가 (왼쪽)
    /// </summary>
    /// <param name="listKey">리스트 키</param>
    /// <param name="value">값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>리스트 길이</returns>
    Task<long> ListLeftPushAsync(string listKey, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 리스트에서 아이템 제거 (왼쪽)
    /// </summary>
    /// <param name="listKey">리스트 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>제거된 값</returns>
    Task<string?> ListLeftPopAsync(string listKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// 세트에 아이템 추가
    /// </summary>
    /// <param name="setKey">세트 키</param>
    /// <param name="value">값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>추가 성공 여부</returns>
    Task<bool> SetAddAsync(string setKey, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 세트에서 아이템 제거
    /// </summary>
    /// <param name="setKey">세트 키</param>
    /// <param name="value">값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>제거 성공 여부</returns>
    Task<bool> SetRemoveAsync(string setKey, string value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 세트 멤버 여부 확인
    /// </summary>
    /// <param name="setKey">세트 키</param>
    /// <param name="value">값</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>멤버 여부</returns>
    Task<bool> SetContainsAsync(string setKey, string value, CancellationToken cancellationToken = default);
}

/// <summary>
/// 분산 락 인터페이스
/// </summary>
public interface IDistributedLock : IAsyncDisposable
{
    /// <summary>
    /// 락 키
    /// </summary>
    string Key { get; }

    /// <summary>
    /// 락 토큰 (고유 식별자)
    /// </summary>
    string Token { get; }

    /// <summary>
    /// 락 획득 시간
    /// </summary>
    DateTimeOffset AcquiredAt { get; }

    /// <summary>
    /// 락 만료 시간
    /// </summary>
    DateTimeOffset ExpiresAt { get; }

    /// <summary>
    /// 락이 유효한지 확인
    /// </summary>
    bool IsValid { get; }

    /// <summary>
    /// 락 연장
    /// </summary>
    /// <param name="additionalTime">추가 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>연장 성공 여부</returns>
    Task<bool> ExtendAsync(TimeSpan additionalTime, CancellationToken cancellationToken = default);

    /// <summary>
    /// 락 해제
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task ReleaseAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// 캐시 이벤트 인터페이스
/// </summary>
public interface ICacheEventService
{
    /// <summary>
    /// 캐시 설정 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheSet;

    /// <summary>
    /// 캐시 조회 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheGet;

    /// <summary>
    /// 캐시 제거 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheRemove;

    /// <summary>
    /// 캐시 만료 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheExpired;

    /// <summary>
    /// 캐시 히트 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheHit;

    /// <summary>
    /// 캐시 미스 이벤트
    /// </summary>
    event EventHandler<CacheEventArgs>? CacheMiss;
}

/// <summary>
/// 캐시 이벤트 인자
/// </summary>
public class CacheEventArgs : EventArgs
{
    /// <summary>
    /// 캐시 키
    /// </summary>
    public string Key { get; init; } = string.Empty;

    /// <summary>
    /// 이벤트 발생 시간
    /// </summary>
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 추가 메타데이터
    /// </summary>
    public Dictionary<string, object> Metadata { get; init; } = new();
}

/// <summary>
/// 캐시 통계 인터페이스
/// </summary>
public interface ICacheStatistics
{
    /// <summary>
    /// 총 요청 수
    /// </summary>
    long TotalRequests { get; }

    /// <summary>
    /// 캐시 히트 수
    /// </summary>
    long CacheHits { get; }

    /// <summary>
    /// 캐시 미스 수
    /// </summary>
    long CacheMisses { get; }

    /// <summary>
    /// 캐시 히트율
    /// </summary>
    double HitRatio { get; }

    /// <summary>
    /// 평균 응답 시간
    /// </summary>
    TimeSpan AverageResponseTime { get; }

    /// <summary>
    /// 통계 초기화
    /// </summary>
    void Reset();

    /// <summary>
    /// 통계 스냅샷 생성
    /// </summary>
    /// <returns>통계 스냅샷</returns>
    CacheStatisticsSnapshot CreateSnapshot();
}

/// <summary>
/// 캐시 통계 스냅샷
/// </summary>
public record CacheStatisticsSnapshot(
    long TotalRequests,
    long CacheHits,
    long CacheMisses,
    double HitRatio,
    TimeSpan AverageResponseTime,
    DateTimeOffset Timestamp,
    Dictionary<string, object> AdditionalMetrics
);

/// <summary>
/// 캐시 설정 옵션
/// </summary>
public class CacheOptions
{
    /// <summary>
    /// 기본 만료 시간
    /// </summary>
    public TimeSpan? DefaultExpiration { get; set; }

    /// <summary>
    /// 최대 메모리 사용량 (바이트)
    /// </summary>
    public long? MaxMemoryUsage { get; set; }

    /// <summary>
    /// 압축 사용 여부
    /// </summary>
    public bool UseCompression { get; set; } = false;

    /// <summary>
    /// 직렬화 방식
    /// </summary>
    public CacheSerializationMode SerializationMode { get; set; } = CacheSerializationMode.Json;

    /// <summary>
    /// 키 접두사
    /// </summary>
    public string? KeyPrefix { get; set; }

    /// <summary>
    /// 키 분리자
    /// </summary>
    public string KeySeparator { get; set; } = ":";

    /// <summary>
    /// 슬라이딩 만료 사용 여부
    /// </summary>
    public bool UseSlidingExpiration { get; set; } = false;

    /// <summary>
    /// 자동 갱신 여부
    /// </summary>
    public bool AutoRefresh { get; set; } = false;

    /// <summary>
    /// 백그라운드 갱신 임계치 (만료 전 몇 초)
    /// </summary>
    public TimeSpan? BackgroundRefreshThreshold { get; set; }

    /// <summary>
    /// 예외 발생 시 기본값 반환 여부
    /// </summary>
    public bool ReturnDefaultOnError { get; set; } = true;

    /// <summary>
    /// 타임아웃 설정
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(5);
}

/// <summary>
/// 캐시 직렬화 방식
/// </summary>
public enum CacheSerializationMode
{
    /// <summary>
    /// JSON 직렬화
    /// </summary>
    Json,

    /// <summary>
    /// MessagePack 직렬화
    /// </summary>
    MessagePack,

    /// <summary>
    /// 바이너리 직렬화
    /// </summary>
    Binary,

    /// <summary>
    /// 문자열 (압축 없음)
    /// </summary>
    String
}

/// <summary>
/// 캐시 타입
/// </summary>
public enum CacheType
{
    /// <summary>
    /// 메모리 캐시
    /// </summary>
    Memory,

    /// <summary>
    /// Redis 분산 캐시
    /// </summary>
    Redis,

    /// <summary>
    /// 하이브리드 (L1: Memory, L2: Redis)
    /// </summary>
    Hybrid,

    /// <summary>
    /// SQL Server 캐시
    /// </summary>
    SqlServer,

    /// <summary>
    /// 파일 시스템 캐시
    /// </summary>
    FileSystem
}

/// <summary>
/// 캐시 무효화 전략 인터페이스
/// </summary>
public interface ICacheInvalidationStrategy
{
    /// <summary>
    /// 태그 기반 무효화
    /// </summary>
    /// <param name="tags">태그 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task InvalidateByTagsAsync(IEnumerable<string> tags, CancellationToken cancellationToken = default);

    /// <summary>
    /// 의존성 기반 무효화
    /// </summary>
    /// <param name="dependency">의존성 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task InvalidateByDependencyAsync(string dependency, CancellationToken cancellationToken = default);

    /// <summary>
    /// 시간 기반 무효화
    /// </summary>
    /// <param name="olderThan">기준 시간</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task InvalidateOlderThanAsync(DateTimeOffset olderThan, CancellationToken cancellationToken = default);

    /// <summary>
    /// 크기 기반 무효화 (LRU)
    /// </summary>
    /// <param name="maxEntries">최대 엔트리 수</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task InvalidateByLruAsync(int maxEntries, CancellationToken cancellationToken = default);
}

/// <summary>
/// 다층 캐시 서비스 인터페이스
/// </summary>
public interface IMultiLevelCacheService : ICacheService
{
    /// <summary>
    /// L1 캐시 (메모리)
    /// </summary>
    ICacheService L1Cache { get; }

    /// <summary>
    /// L2 캐시 (분산)
    /// </summary>
    IDistributedCacheService L2Cache { get; }

    /// <summary>
    /// L1 캐시 히트율
    /// </summary>
    double L1HitRatio { get; }

    /// <summary>
    /// L2 캐시 히트율
    /// </summary>
    double L2HitRatio { get; }

    /// <summary>
    /// L1 캐시 승격 (L2에서 L1으로)
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task PromoteToL1Async(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// L1 캐시 무효화
    /// </summary>
    /// <param name="key">캐시 키</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task InvalidateL1Async(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시 계층 동기화
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SynchronizeCacheLevelsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// 캐시 압축 서비스 인터페이스
/// </summary>
public interface ICacheCompressionService
{
    /// <summary>
    /// 데이터 압축
    /// </summary>
    /// <param name="data">압축할 데이터</param>
    /// <returns>압축된 데이터</returns>
    byte[] Compress(byte[] data);

    /// <summary>
    /// 데이터 압축 해제
    /// </summary>
    /// <param name="compressedData">압축된 데이터</param>
    /// <returns>원본 데이터</returns>
    byte[] Decompress(byte[] compressedData);

    /// <summary>
    /// 압축률 계산
    /// </summary>
    /// <param name="originalSize">원본 크기</param>
    /// <param name="compressedSize">압축 크기</param>
    /// <returns>압축률 (0.0 ~ 1.0)</returns>
    double CalculateCompressionRatio(long originalSize, long compressedSize);

    /// <summary>
    /// 압축 알고리즘
    /// </summary>
    CompressionAlgorithm Algorithm { get; }
}

/// <summary>
/// 압축 알고리즘
/// </summary>
public enum CompressionAlgorithm
{
    /// <summary>
    /// GZip 압축
    /// </summary>
    GZip,

    /// <summary>
    /// Deflate 압축
    /// </summary>
    Deflate,

    /// <summary>
    /// Brotli 압축
    /// </summary>
    Brotli,

    /// <summary>
    /// LZ4 압축 (고속)
    /// </summary>
    LZ4,

    /// <summary>
    /// Snappy 압축
    /// </summary>
    Snappy
}

/// <summary>
/// 캐시 워밍 서비스 인터페이스
/// </summary>
public interface ICacheWarmupService
{
    /// <summary>
    /// 캐시 워밍 실행
    /// </summary>
    /// <param name="keys">워밍할 키 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task WarmupAsync(IEnumerable<string> keys, CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건부 캐시 워밍
    /// </summary>
    /// <param name="condition">워밍 조건</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task WarmupAsync(Func<string, bool> condition, CancellationToken cancellationToken = default);

    /// <summary>
    /// 백그라운드 캐시 워밍
    /// </summary>
    /// <param name="keys">워밍할 키 목록</param>
    void WarmupInBackground(IEnumerable<string> keys);

    /// <summary>
    /// 워밍 진행률 조회
    /// </summary>
    WarmupProgress GetProgress();

    /// <summary>
    /// 워밍 취소
    /// </summary>
    void CancelWarmup();
}

/// <summary>
/// 캐시 워밍 진행률
/// </summary>
public record WarmupProgress(
    int TotalKeys,
    int ProcessedKeys,
    int SuccessfulKeys,
    int FailedKeys,
    double ProgressPercentage,
    TimeSpan ElapsedTime,
    TimeSpan? EstimatedRemainingTime,
    bool IsCompleted,
    string? CurrentKey
);

/// <summary>
/// 캐시 백업 서비스 인터페이스
/// </summary>
public interface ICacheBackupService
{
    /// <summary>
    /// 캐시 백업 생성
    /// </summary>
    /// <param name="backupPath">백업 경로</param>
    /// <param name="pattern">키 패턴 (선택적)</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task CreateBackupAsync(string backupPath, string? pattern = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 캐시 복원
    /// </summary>
    /// <param name="backupPath">백업 경로</param>
    /// <param name="overwriteExisting">기존 키 덮어쓰기 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RestoreBackupAsync(string backupPath, bool overwriteExisting = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// 백업 검증
    /// </summary>
    /// <param name="backupPath">백업 경로</param>
    /// <param name="cancellationToken">취소 토큰</param>
    /// <returns>검증 결과</returns>
    Task<BackupValidationResult> ValidateBackupAsync(string backupPath, CancellationToken cancellationToken = default);

    /// <summary>
    /// 백업 목록 조회
    /// </summary>
    /// <param name="backupDirectory">백업 디렉토리</param>
    /// <returns>백업 정보 목록</returns>
    Task<IEnumerable<BackupInfo>> GetBackupsAsync(string backupDirectory);
}

/// <summary>
/// 백업 검증 결과
/// </summary>
public record BackupValidationResult(
    bool IsValid,
    long EntryCount,
    long TotalSize,
    DateTimeOffset CreatedAt,
    string Version,
    IReadOnlyList<string> Errors
);

/// <summary>
/// 백업 정보
/// </summary>
public record BackupInfo(
    string FilePath,
    string FileName,
    long FileSize,
    DateTimeOffset CreatedAt,
    long EntryCount,
    string Version
);