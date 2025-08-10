using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace SuperAuth.Shared.Contracts.DTOs.Common;

/// <summary>
/// 기본 요청 DTO
/// </summary>
public abstract class BaseRequest
{
    /// <summary>
    /// 요청 ID (추적용)
    /// </summary>
    [JsonPropertyName("requestId")]
    public string? RequestId { get; init; }

    /// <summary>
    /// 요청 타임스탬프 (UTC)
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 클라이언트 정보
    /// </summary>
    [JsonPropertyName("clientInfo")]
    public ClientInfo? ClientInfo { get; init; }

    /// <summary>
    /// 요청 메타데이터
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }
}

/// <summary>
/// 기본 응답 DTO
/// </summary>
public abstract class BaseResponse
{
    /// <summary>
    /// 응답 타임스탬프 (UTC)
    /// </summary>
    [JsonPropertyName("timestamp")]
    public DateTimeOffset Timestamp { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 처리 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("processingTimeMs")]
    public long? ProcessingTimeMs { get; init; }

    /// <summary>
    /// 서버 정보
    /// </summary>
    [JsonPropertyName("serverInfo")]
    public ServerInfo? ServerInfo { get; init; }
}

/// <summary>
/// 페이징 요청 DTO
/// </summary>
public class PagedRequest : BaseRequest
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    /// <summary>
    /// 페이지 번호 (1부터 시작, 기본값: 1)
    /// </summary>
    [JsonPropertyName("pageNumber")]
    [Range(1, int.MaxValue, ErrorMessage = "페이지 번호는 1 이상이어야 합니다.")]
    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = Math.Max(1, value);
    }

    /// <summary>
    /// 페이지 크기 (기본값: 10, 최대: 100)
    /// </summary>
    [JsonPropertyName("pageSize")]
    [Range(1, 100, ErrorMessage = "페이지 크기는 1-100 사이여야 합니다.")]
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = Math.Clamp(value, 1, 100);
    }

    /// <summary>
    /// 정렬 조건 (예: "name asc,createdAt desc")
    /// </summary>
    [JsonPropertyName("sortBy")]
    public string? SortBy { get; init; }

    /// <summary>
    /// 검색 텍스트
    /// </summary>
    [JsonPropertyName("searchText")]
    [StringLength(100, ErrorMessage = "검색 텍스트는 100자를 초과할 수 없습니다.")]
    public string? SearchText { get; init; }

    /// <summary>
    /// 검색 필드 목록
    /// </summary>
    [JsonPropertyName("searchFields")]
    public IList<string>? SearchFields { get; init; }

    /// <summary>
    /// 필터 조건
    /// </summary>
    [JsonPropertyName("filters")]
    public Dictionary<string, object>? Filters { get; init; }

    /// <summary>
    /// 스킵할 아이템 수 계산
    /// </summary>
    [JsonIgnore]
    public int Skip => (PageNumber - 1) * PageSize;

    /// <summary>
    /// 가져올 아이템 수
    /// </summary>
    [JsonIgnore]
    public int Take => PageSize;
}

/// <summary>
/// ID 기반 요청 DTO
/// </summary>
public class IdRequest : BaseRequest
{
    /// <summary>
    /// 엔티티 ID
    /// </summary>
    [JsonPropertyName("id")]
    [Required(ErrorMessage = "ID는 필수입니다.")]
    public Guid Id { get; init; }
}

/// <summary>
/// ID 기반 요청 DTO (강타입)
/// </summary>
/// <typeparam name="TId">ID 타입</typeparam>
public class IdRequest<TId> : BaseRequest where TId : notnull
{
    /// <summary>
    /// 엔티티 ID
    /// </summary>
    [JsonPropertyName("id")]
    [Required(ErrorMessage = "ID는 필수입니다.")]
    public TId Id { get; init; } = default!;
}

/// <summary>
/// 여러 ID 기반 요청 DTO
/// </summary>
public class IdsRequest : BaseRequest
{
    /// <summary>
    /// 엔티티 ID 목록
    /// </summary>
    [JsonPropertyName("ids")]
    [Required(ErrorMessage = "ID 목록은 필수입니다.")]
    [MinLength(1, ErrorMessage = "최소 하나의 ID가 필요합니다.")]
    public IList<Guid> Ids { get; init; } = new List<Guid>();
}

/// <summary>
/// 클라이언트 정보
/// </summary>
public class ClientInfo
{
    /// <summary>
    /// IP 주소
    /// </summary>
    [JsonPropertyName("ipAddress")]
    public string? IpAddress { get; init; }

    /// <summary>
    /// 사용자 에이전트
    /// </summary>
    [JsonPropertyName("userAgent")]
    public string? UserAgent { get; init; }

    /// <summary>
    /// 디바이스 타입
    /// </summary>
    [JsonPropertyName("deviceType")]
    public string? DeviceType { get; init; }

    /// <summary>
    /// 운영체제
    /// </summary>
    [JsonPropertyName("operatingSystem")]
    public string? OperatingSystem { get; init; }

    /// <summary>
    /// 브라우저
    /// </summary>
    [JsonPropertyName("browser")]
    public string? Browser { get; init; }

    /// <summary>
    /// 위치 정보
    /// </summary>
    [JsonPropertyName("location")]
    public LocationInfo? Location { get; init; }

    /// <summary>
    /// 시간대
    /// </summary>
    [JsonPropertyName("timeZone")]
    public string? TimeZone { get; init; }

    /// <summary>
    /// 언어
    /// </summary>
    [JsonPropertyName("language")]
    public string? Language { get; init; }

    /// <summary>
    /// 애플리케이션 버전
    /// </summary>
    [JsonPropertyName("appVersion")]
    public string? AppVersion { get; init; }

    /// <summary>
    /// 디바이스 ID
    /// </summary>
    [JsonPropertyName("deviceId")]
    public string? DeviceId { get; init; }

    /// <summary>
    /// 세션 ID
    /// </summary>
    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }
}

/// <summary>
/// 위치 정보
/// </summary>
public class LocationInfo
{
    /// <summary>
    /// 국가 코드 (ISO 3166-1 alpha-2)
    /// </summary>
    [JsonPropertyName("countryCode")]
    public string? CountryCode { get; init; }

    /// <summary>
    /// 국가명
    /// </summary>
    [JsonPropertyName("countryName")]
    public string? CountryName { get; init; }

    /// <summary>
    /// 지역/주
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; init; }

    /// <summary>
    /// 도시
    /// </summary>
    [JsonPropertyName("city")]
    public string? City { get; init; }

    /// <summary>
    /// 위도
    /// </summary>
    [JsonPropertyName("latitude")]
    public double? Latitude { get; init; }

    /// <summary>
    /// 경도
    /// </summary>
    [JsonPropertyName("longitude")]
    public double? Longitude { get; init; }

    /// <summary>
    /// ISP (인터넷 서비스 제공업체)
    /// </summary>
    [JsonPropertyName("isp")]
    public string? Isp { get; init; }

    /// <summary>
    /// 조직
    /// </summary>
    [JsonPropertyName("organization")]
    public string? Organization { get; init; }

    /// <summary>
    /// 프록시 여부
    /// </summary>
    [JsonPropertyName("isProxy")]
    public bool? IsProxy { get; init; }

    /// <summary>
    /// VPN 여부
    /// </summary>
    [JsonPropertyName("isVpn")]
    public bool? IsVpn { get; init; }

    /// <summary>
    /// Tor 여부
    /// </summary>
    [JsonPropertyName("isTor")]
    public bool? IsTor { get; init; }
}

/// <summary>
/// 서버 정보
/// </summary>
public class ServerInfo
{
    /// <summary>
    /// 서버 이름
    /// </summary>
    [JsonPropertyName("serverName")]
    public string? ServerName { get; init; }

    /// <summary>
    /// 애플리케이션 버전
    /// </summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }

    /// <summary>
    /// 환경 (Development, Staging, Production)
    /// </summary>
    [JsonPropertyName("environment")]
    public string? Environment { get; init; }

    /// <summary>
    /// 지역
    /// </summary>
    [JsonPropertyName("region")]
    public string? Region { get; init; }

    /// <summary>
    /// 인스턴스 ID
    /// </summary>
    [JsonPropertyName("instanceId")]
    public string? InstanceId { get; init; }

    /// <summary>
    /// 로드 밸런서 정보
    /// </summary>
    [JsonPropertyName("loadBalancer")]
    public string? LoadBalancer { get; init; }
}

/// <summary>
/// 건강 상태 확인 응답
/// </summary>
public class HealthCheckResponse : BaseResponse
{
    /// <summary>
    /// 전체 상태
    /// </summary>
    [JsonPropertyName("status")]
    public HealthStatus Status { get; init; }

    /// <summary>
    /// 총 확인 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("totalDurationMs")]
    public long TotalDurationMs { get; init; }

    /// <summary>
    /// 개별 확인 결과
    /// </summary>
    [JsonPropertyName("checks")]
    public Dictionary<string, HealthCheckResult> Checks { get; init; } = new();

    /// <summary>
    /// 시스템 정보
    /// </summary>
    [JsonPropertyName("systemInfo")]
    public SystemInfo? SystemInfo { get; init; }
}

/// <summary>
/// 건강 상태 확인 결과
/// </summary>
public class HealthCheckResult
{
    /// <summary>
    /// 상태
    /// </summary>
    [JsonPropertyName("status")]
    public HealthStatus Status { get; init; }

    /// <summary>
    /// 설명
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>
    /// 확인 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("durationMs")]
    public long DurationMs { get; init; }

    /// <summary>
    /// 오류 메시지
    /// </summary>
    [JsonPropertyName("error")]
    public string? Error { get; init; }

    /// <summary>
    /// 추가 데이터
    /// </summary>
    [JsonPropertyName("data")]
    public Dictionary<string, object>? Data { get; init; }

    /// <summary>
    /// 마지막 확인 시간
    /// </summary>
    [JsonPropertyName("lastChecked")]
    public DateTimeOffset LastChecked { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// 태그 목록
    /// </summary>
    [JsonPropertyName("tags")]
    public IList<string>? Tags { get; init; }
}

/// <summary>
/// 건강 상태
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HealthStatus
{
    /// <summary>
    /// 정상
    /// </summary>
    Healthy,

    /// <summary>
    /// 성능 저하
    /// </summary>
    Degraded,

    /// <summary>
    /// 비정상
    /// </summary>
    Unhealthy
}

/// <summary>
/// 시스템 정보
/// </summary>
public class SystemInfo
{
    /// <summary>
    /// CPU 사용률 (%)
    /// </summary>
    [JsonPropertyName("cpuUsagePercent")]
    public double? CpuUsagePercent { get; init; }

    /// <summary>
    /// 메모리 사용량 (바이트)
    /// </summary>
    [JsonPropertyName("memoryUsageBytes")]
    public long? MemoryUsageBytes { get; init; }

    /// <summary>
    /// 총 메모리 (바이트)
    /// </summary>
    [JsonPropertyName("totalMemoryBytes")]
    public long? TotalMemoryBytes { get; init; }

    /// <summary>
    /// 메모리 사용률 (%)
    /// </summary>
    [JsonPropertyName("memoryUsagePercent")]
    public double? MemoryUsagePercent => 
        TotalMemoryBytes > 0 && MemoryUsageBytes.HasValue 
            ? (double)MemoryUsageBytes.Value / TotalMemoryBytes.Value * 100 
            : null;

    /// <summary>
    /// 디스크 사용량 (바이트)
    /// </summary>
    [JsonPropertyName("diskUsageBytes")]
    public long? DiskUsageBytes { get; init; }

    /// <summary>
    /// 총 디스크 용량 (바이트)
    /// </summary>
    [JsonPropertyName("totalDiskBytes")]
    public long? TotalDiskBytes { get; init; }

    /// <summary>
    /// 디스크 사용률 (%)
    /// </summary>
    [JsonPropertyName("diskUsagePercent")]
    public double? DiskUsagePercent => 
        TotalDiskBytes > 0 && DiskUsageBytes.HasValue 
            ? (double)DiskUsageBytes.Value / TotalDiskBytes.Value * 100 
            : null;

    /// <summary>
    /// 가동 시간
    /// </summary>
    [JsonPropertyName("uptime")]
    public TimeSpan? Uptime { get; init; }

    /// <summary>
    /// 활성 스레드 수
    /// </summary>
    [JsonPropertyName("activeThreads")]
    public int? ActiveThreads { get; init; }

    /// <summary>
    /// GC 수집 횟수
    /// </summary>
    [JsonPropertyName("gcCollections")]
    public Dictionary<int, long>? GcCollections { get; init; }

    /// <summary>
    /// 네트워크 연결 수
    /// </summary>
    [JsonPropertyName("networkConnections")]
    public int? NetworkConnections { get; init; }

    /// <summary>
    /// 로드 평균 (1분, 5분, 15분)
    /// </summary>
    [JsonPropertyName("loadAverage")]
    public double[]? LoadAverage { get; init; }
}

/// <summary>
/// 버전 정보 응답
/// </summary>
public class VersionResponse : BaseResponse
{
    /// <summary>
    /// 애플리케이션 버전
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; init; } = string.Empty;

    /// <summary>
    /// 빌드 번호
    /// </summary>
    [JsonPropertyName("buildNumber")]
    public string? BuildNumber { get; init; }

    /// <summary>
    /// 커밋 해시
    /// </summary>
    [JsonPropertyName("commitHash")]
    public string? CommitHash { get; init; }

    /// <summary>
    /// 브랜치명
    /// </summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; init; }

    /// <summary>
    /// 빌드 날짜
    /// </summary>
    [JsonPropertyName("buildDate")]
    public DateTimeOffset? BuildDate { get; init; }

    /// <summary>
    /// .NET 런타임 버전
    /// </summary>
    [JsonPropertyName("dotNetVersion")]
    public string? DotNetVersion { get; init; }

    /// <summary>
    /// 환경 정보
    /// </summary>
    [JsonPropertyName("environment")]
    public string? Environment { get; init; }

    /// <summary>
    /// 의존성 버전 목록
    /// </summary>
    [JsonPropertyName("dependencies")]
    public Dictionary<string, string>? Dependencies { get; init; }
}

/// <summary>
/// 메트릭 응답
/// </summary>
public class MetricsResponse : BaseResponse
{
    /// <summary>
    /// 애플리케이션 메트릭
    /// </summary>
    [JsonPropertyName("application")]
    public ApplicationMetrics? Application { get; init; }

    /// <summary>
    /// 시스템 메트릭
    /// </summary>
    [JsonPropertyName("system")]
    public SystemMetrics? System { get; init; }

    /// <summary>
    /// 비즈니스 메트릭
    /// </summary>
    [JsonPropertyName("business")]
    public Dictionary<string, object>? Business { get; init; }

    /// <summary>
    /// 수집 시간 범위
    /// </summary>
    [JsonPropertyName("timeRange")]
    public TimeRange? TimeRange { get; init; }
}

/// <summary>
/// 애플리케이션 메트릭
/// </summary>
public class ApplicationMetrics
{
    /// <summary>
    /// 총 요청 수
    /// </summary>
    [JsonPropertyName("totalRequests")]
    public long TotalRequests { get; init; }

    /// <summary>
    /// 성공한 요청 수
    /// </summary>
    [JsonPropertyName("successfulRequests")]
    public long SuccessfulRequests { get; init; }

    /// <summary>
    /// 실패한 요청 수
    /// </summary>
    [JsonPropertyName("failedRequests")]
    public long FailedRequests { get; init; }

    /// <summary>
    /// 평균 응답 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("averageResponseTimeMs")]
    public double AverageResponseTimeMs { get; init; }

    /// <summary>
    /// 95% 백분위 응답 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("p95ResponseTimeMs")]
    public double P95ResponseTimeMs { get; init; }

    /// <summary>
    /// 99% 백분위 응답 시간 (밀리초)
    /// </summary>
    [JsonPropertyName("p99ResponseTimeMs")]
    public double P99ResponseTimeMs { get; init; }

    /// <summary>
    /// 초당 요청 수 (RPS)
    /// </summary>
    [JsonPropertyName("requestsPerSecond")]
    public double RequestsPerSecond { get; init; }

    /// <summary>
    /// 오류율 (%)
    /// </summary>
    [JsonPropertyName("errorRate")]
    public double ErrorRate { get; init; }

    /// <summary>
    /// 활성 연결 수
    /// </summary>
    [JsonPropertyName("activeConnections")]
    public int ActiveConnections { get; init; }

    /// <summary>
    /// 캐시 히트율 (%)
    /// </summary>
    [JsonPropertyName("cacheHitRate")]
    public double? CacheHitRate { get; init; }

    /// <summary>
    /// 큐 길이
    /// </summary>
    [JsonPropertyName("queueLength")]
    public int? QueueLength { get; init; }
}

/// <summary>
/// 시스템 메트릭
/// </summary>
public class SystemMetrics
{
    /// <summary>
    /// CPU 사용률 (%)
    /// </summary>
    [JsonPropertyName("cpuUsage")]
    public double CpuUsage { get; init; }

    /// <summary>
    /// 메모리 사용률 (%)
    /// </summary>
    [JsonPropertyName("memoryUsage")]
    public double MemoryUsage { get; init; }

    /// <summary>
    /// 디스크 사용률 (%)
    /// </summary>
    [JsonPropertyName("diskUsage")]
    public double DiskUsage { get; init; }

    /// <summary>
    /// 네트워크 송신량 (bps)
    /// </summary>
    [JsonPropertyName("networkOutBps")]
    public long NetworkOutBps { get; init; }

    /// <summary>
    /// 네트워크 수신량 (bps)
    /// </summary>
    [JsonPropertyName("networkInBps")]
    public long NetworkInBps { get; init; }

    /// <summary>
    /// 디스크 읽기 속도 (bps)
    /// </summary>
    [JsonPropertyName("diskReadBps")]
    public long DiskReadBps { get; init; }

    /// <summary>
    /// 디스크 쓰기 속도 (bps)
    /// </summary>
    [JsonPropertyName("diskWriteBps")]
    public long DiskWriteBps { get; init; }

    /// <summary>
    /// 로드 평균
    /// </summary>
    [JsonPropertyName("loadAverage")]
    public double[]? LoadAverage { get; init; }
}

/// <summary>
/// 시간 범위
/// </summary>
public class TimeRange
{
    /// <summary>
    /// 시작 시간
    /// </summary>
    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; init; }

    /// <summary>
    /// 종료 시간
    /// </summary>
    [JsonPropertyName("end")]
    public DateTimeOffset End { get; init; }

    /// <summary>
    /// 기간
    /// </summary>
    [JsonPropertyName("duration")]
    public TimeSpan Duration => End - Start;

    /// <summary>
    /// 간격 (집계 단위)
    /// </summary>
    [JsonPropertyName("interval")]
    public TimeSpan? Interval { get; init; }
}

/// <summary>
/// 파일 업로드 응답
/// </summary>
public class FileUploadResponse : BaseResponse
{
    /// <summary>
    /// 파일 ID
    /// </summary>
    [JsonPropertyName("fileId")]
    public string FileId { get; init; } = string.Empty;

    /// <summary>
    /// 원본 파일명
    /// </summary>
    [JsonPropertyName("originalFileName")]
    public string OriginalFileName { get; init; } = string.Empty;

    /// <summary>
    /// 저장된 파일명
    /// </summary>
    [JsonPropertyName("storedFileName")]
    public string StoredFileName { get; init; } = string.Empty;

    /// <summary>
    /// 파일 크기 (바이트)
    /// </summary>
    [JsonPropertyName("fileSize")]
    public long FileSize { get; init; }

    /// <summary>
    /// MIME 타입
    /// </summary>
    [JsonPropertyName("mimeType")]
    public string MimeType { get; init; } = string.Empty;

    /// <summary>
    /// 파일 URL
    /// </summary>
    [JsonPropertyName("fileUrl")]
    public string? FileUrl { get; init; }

    /// <summary>
    /// 썸네일 URL (이미지인 경우)
    /// </summary>
    [JsonPropertyName("thumbnailUrl")]
    public string? ThumbnailUrl { get; init; }

    /// <summary>
    /// 파일 해시 (무결성 검증용)
    /// </summary>
    [JsonPropertyName("fileHash")]
    public string? FileHash { get; init; }

    /// <summary>
    /// 만료 시간 (임시 파일인 경우)
    /// </summary>
    [JsonPropertyName("expiresAt")]
    public DateTimeOffset? ExpiresAt { get; init; }
}

/// <summary>
/// 일괄 작업 응답
/// </summary>
/// <typeparam name="T">결과 타입</typeparam>
public class BatchResponse<T> : BaseResponse
{
    /// <summary>
    /// 총 아이템 수
    /// </summary>
    [JsonPropertyName("totalItems")]
    public int TotalItems { get; init; }

    /// <summary>
    /// 성공한 아이템 수
    /// </summary>
    [JsonPropertyName("successCount")]
    public int SuccessCount { get; init; }

    /// <summary>
    /// 실패한 아이템 수
    /// </summary>
    [JsonPropertyName("failureCount")]
    public int FailureCount { get; init; }

    /// <summary>
    /// 성공률 (%)
    /// </summary>
    [JsonPropertyName("successRate")]
    public double SuccessRate => TotalItems > 0 ? (double)SuccessCount / TotalItems * 100 : 0;

    /// <summary>
    /// 성공 결과 목록
    /// </summary>
    [JsonPropertyName("successes")]
    public IList<T> Successes { get; init; } = new List<T>();

    /// <summary>
    /// 실패 결과 목록
    /// </summary>
    [JsonPropertyName("failures")]
    public IList<BatchFailure> Failures { get; init; } = new List<BatchFailure>();

    /// <summary>
    /// 경고 목록
    /// </summary>
    [JsonPropertyName("warnings")]
    public IList<string>? Warnings { get; init; }
}

/// <summary>
/// 일괄 작업 실패 정보
/// </summary>
public class BatchFailure
{
    /// <summary>
    /// 실패한 아이템 인덱스
    /// </summary>
    [JsonPropertyName("index")]
    public int Index { get; init; }

    /// <summary>
    /// 아이템 식별자
    /// </summary>
    [JsonPropertyName("itemId")]
    public string? ItemId { get; init; }

    /// <summary>
    /// 오류 코드
    /// </summary>
    [JsonPropertyName("errorCode")]
    public string ErrorCode { get; init; } = string.Empty;

    /// <summary>
    /// 오류 메시지
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; init; } = string.Empty;

    /// <summary>
    /// 상세 정보
    /// </summary>
    [JsonPropertyName("details")]
    public Dictionary<string, object>? Details { get; init; }
}