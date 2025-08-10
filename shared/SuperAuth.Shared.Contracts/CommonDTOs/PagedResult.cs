using System.Text.Json.Serialization;

namespace SuperAuth.Shared.Contracts.DTOs.Common;

/// <summary>
/// 페이징된 결과 DTO
/// </summary>
/// <typeparam name="T">아이템 타입</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// 현재 페이지 아이템 목록
    /// </summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<T> Items { get; init; } = new List<T>();

    /// <summary>
    /// 페이징 정보
    /// </summary>
    [JsonPropertyName("pagination")]
    public PaginationInfo Pagination { get; init; } = new();

    /// <summary>
    /// 정렬 정보
    /// </summary>
    [JsonPropertyName("sorting")]
    public SortingInfo? Sorting { get; init; }

    /// <summary>
    /// 필터링 정보
    /// </summary>
    [JsonPropertyName("filtering")]
    public FilteringInfo? Filtering { get; init; }

    /// <summary>
    /// 메타데이터
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, object>? Metadata { get; init; }

    /// <summary>
    /// 페이징된 결과 생성
    /// </summary>
    /// <param name="items">아이템 목록</param>
    /// <param name="totalCount">총 개수</param>
    /// <param name="pageNumber">페이지 번호</param>
    /// <param name="pageSize">페이지 크기</param>
    /// <param name="sorting">정렬 정보</param>
    /// <param name="filtering">필터링 정보</param>
    /// <param name="metadata">메타데이터</param>
    /// <returns>페이징된 결과</returns>
    public static PagedResult<T> Create(
        IEnumerable<T> items, 
        int totalCount, 
        int pageNumber, 
        int pageSize,
        SortingInfo? sorting = null,
        FilteringInfo? filtering = null,
        Dictionary<string, object>? metadata = null)
    {
        return new PagedResult<T>
        {
            Items = items.ToList(),
            Pagination = new PaginationInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            },
            Sorting = sorting,
            Filtering = filtering,
            Metadata = metadata
        };
    }

    /// <summary>
    /// 빈 페이징 결과 생성
    /// </summary>
    /// <param name="pageNumber">페이지 번호</param>
    /// <param name="pageSize">페이지 크기</param>
    /// <returns>빈 페이징 결과</returns>
    public static PagedResult<T> Empty(int pageNumber = 1, int pageSize = 10)
    {
        return new PagedResult<T>
        {
            Items = new List<T>(),
            Pagination = new PaginationInfo
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = 0
            }
        };
    }

    /// <summary>
    /// 다른 타입으로 변환
    /// </summary>
    /// <typeparam name="TResult">변환할 타입</typeparam>
    /// <param name="converter">변환 함수</param>
    /// <returns>변환된 페이징 결과</returns>
    public PagedResult<TResult> Map<TResult>(Func<T, TResult> converter)
    {
        return new PagedResult<TResult>
        {
            Items = Items.Select(converter).ToList(),
            Pagination = Pagination,
            Sorting = Sorting,
            Filtering = Filtering,
            Metadata = Metadata
        };
    }
}

/// <summary>
/// 페이징 정보
/// </summary>
public class PaginationInfo
{
    /// <summary>
    /// 현재 페이지 번호 (1부터 시작)
    /// </summary>
    [JsonPropertyName("pageNumber")]
    public int PageNumber { get; init; }

    /// <summary>
    /// 페이지 크기
    /// </summary>
    [JsonPropertyName("pageSize")]
    public int PageSize { get; init; }

    /// <summary>
    /// 총 아이템 개수
    /// </summary>
    [JsonPropertyName("totalCount")]
    public int TotalCount { get; init; }

    /// <summary>
    /// 총 페이지 개수
    /// </summary>
    [JsonPropertyName("totalPages")]
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// 이전 페이지 존재 여부
    /// </summary>
    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// 다음 페이지 존재 여부
    /// </summary>
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// 첫 번째 페이지 여부
    /// </summary>
    [JsonPropertyName("isFirstPage")]
    public bool IsFirstPage => PageNumber == 1;

    /// <summary>
    /// 마지막 페이지 여부
    /// </summary>
    [JsonPropertyName("isLastPage")]
    public bool IsLastPage => PageNumber == TotalPages;

    /// <summary>
    /// 현재 페이지의 첫 번째 아이템 번호
    /// </summary>
    [JsonPropertyName("firstItemOnPage")]
    public int FirstItemOnPage => TotalCount == 0 ? 0 : (PageNumber - 1) * PageSize + 1;

    /// <summary>
    /// 현재 페이지의 마지막 아이템 번호
    /// </summary>
    [JsonPropertyName("lastItemOnPage")]
    public int LastItemOnPage => Math.Min(PageNumber * PageSize, TotalCount);

    /// <summary>
    /// 현재 페이지의 아이템 개수
    /// </summary>
    [JsonPropertyName("itemsOnPage")]
    public int ItemsOnPage => LastItemOnPage - FirstItemOnPage + 1;

    /// <summary>
    /// 이전 페이지 번호 (없으면 null)
    /// </summary>
    [JsonPropertyName("previousPage")]
    public int? PreviousPage => HasPreviousPage ? PageNumber - 1 : null;

    /// <summary>
    /// 다음 페이지 번호 (없으면 null)
    /// </summary>
    [JsonPropertyName("nextPage")]
    public int? NextPage => HasNextPage ? PageNumber + 1 : null;
}

/// <summary>
/// 정렬 정보
/// </summary>
public class SortingInfo
{
    /// <summary>
    /// 정렬 필드 목록
    /// </summary>
    [JsonPropertyName("fields")]
    public IReadOnlyList<SortField> Fields { get; init; } = new List<SortField>();

    /// <summary>
    /// 기본 정렬 여부
    /// </summary>
    [JsonPropertyName("isDefault")]
    public bool IsDefault { get; init; }

    /// <summary>
    /// 정렬 정보 생성
    /// </summary>
    /// <param name="field">정렬 필드</param>
    /// <param name="direction">정렬 방향</param>
    /// <returns>정렬 정보</returns>
    public static SortingInfo Create(string field, SortDirection direction = SortDirection.Ascending)
    {
        return new SortingInfo
        {
            Fields = new List<SortField>
            {
                new() { Field = field, Direction = direction }
            }
        };
    }

    /// <summary>
    /// 다중 정렬 정보 생성
    /// </summary>
    /// <param name="fields">정렬 필드 목록</param>
    /// <returns>정렬 정보</returns>
    public static SortingInfo Create(params SortField[] fields)
    {
        return new SortingInfo
        {
            Fields = fields.ToList()
        };
    }

    /// <summary>
    /// 정렬 문자열로 생성 (예: "name asc,createdAt desc")
    /// </summary>
    /// <param name="sortString">정렬 문자열</param>
    /// <returns>정렬 정보</returns>
    public static SortingInfo Parse(string sortString)
    {
        if (string.IsNullOrWhiteSpace(sortString))
            return new SortingInfo();

        var fields = sortString.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(ParseSortField)
            .ToList();

        return new SortingInfo { Fields = fields };
    }

    private static SortField ParseSortField(string fieldString)
    {
        var parts = fieldString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var field = parts[0];
        var direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.OrdinalIgnoreCase)
            ? SortDirection.Descending
            : SortDirection.Ascending;

        return new SortField { Field = field, Direction = direction };
    }
}

/// <summary>
/// 정렬 필드
/// </summary>
public class SortField
{
    /// <summary>
    /// 필드명
    /// </summary>
    [JsonPropertyName("field")]
    public string Field { get; init; } = string.Empty;

    /// <summary>
    /// 정렬 방향
    /// </summary>
    [JsonPropertyName("direction")]
    public SortDirection Direction { get; init; } = SortDirection.Ascending;

    /// <summary>
    /// 정렬 우선순위
    /// </summary>
    [JsonPropertyName("priority")]
    public int Priority { get; init; }

    /// <summary>
    /// 오름차순 정렬 필드 생성
    /// </summary>
    /// <param name="field">필드명</param>
    /// <param name="priority">우선순위</param>
    /// <returns>정렬 필드</returns>
    public static SortField Ascending(string field, int priority = 0)
    {
        return new SortField
        {
            Field = field,
            Direction = SortDirection.Ascending,
            Priority = priority
        };
    }

    /// <summary>
    /// 내림차순 정렬 필드 생성
    /// </summary>
    /// <param name="field">필드명</param>
    /// <param name="priority">우선순위</param>
    /// <returns>정렬 필드</returns>
    public static SortField Descending(string field, int priority = 0)
    {
        return new SortField
        {
            Field = field,
            Direction = SortDirection.Descending,
            Priority = priority
        };
    }

    /// <summary>
    /// 정렬 문자열로 변환
    /// </summary>
    /// <returns>정렬 문자열</returns>
    public override string ToString()
    {
        return $"{Field} {(Direction == SortDirection.Ascending ? "asc" : "desc")}";
    }
}

/// <summary>
/// 정렬 방향
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SortDirection
{
    /// <summary>
    /// 오름차순
    /// </summary>
    Ascending,

    /// <summary>
    /// 내림차순
    /// </summary>
    Descending
}

/// <summary>
/// 필터링 정보
/// </summary>
public class FilteringInfo
{
    /// <summary>
    /// 필터 목록
    /// </summary>
    [JsonPropertyName("filters")]
    public IReadOnlyList<FilterField> Filters { get; init; } = new List<FilterField>();

    /// <summary>
    /// 검색 텍스트
    /// </summary>
    [JsonPropertyName("searchText")]
    public string? SearchText { get; init; }

    /// <summary>
    /// 검색 필드 목록
    /// </summary>
    [JsonPropertyName("searchFields")]
    public IReadOnlyList<string>? SearchFields { get; init; }

    /// <summary>
    /// 필터링 정보 생성
    /// </summary>
    /// <param name="field">필터 필드</param>
    /// <param name="operator">연산자</param>
    /// <param name="value">값</param>
    /// <returns>필터링 정보</returns>
    public static FilteringInfo Create(string field, FilterOperator @operator, object? value)
    {
        return new FilteringInfo
        {
            Filters = new List<FilterField>
            {
                new() { Field = field, Operator = @operator, Value = value }
            }
        };
    }

    /// <summary>
    /// 검색 필터링 정보 생성
    /// </summary>
    /// <param name="searchText">검색 텍스트</param>
    /// <param name="searchFields">검색 필드 목록</param>
    /// <returns>필터링 정보</returns>
    public static FilteringInfo Search(string searchText, params string[] searchFields)
    {
        return new FilteringInfo
        {
            SearchText = searchText,
            SearchFields = searchFields.ToList()
        };
    }
}

/// <summary>
/// 필터 필드
/// </summary>
public class FilterField
{
    /// <summary>
    /// 필드명
    /// </summary>
    [JsonPropertyName("field")]
    public string Field { get; init; } = string.Empty;

    /// <summary>
    /// 연산자
    /// </summary>
    [JsonPropertyName("operator")]
    public FilterOperator Operator { get; init; }

    /// <summary>
    /// 필터 값
    /// </summary>
    [JsonPropertyName("value")]
    public object? Value { get; init; }

    /// <summary>
    /// 대소문자 구분 여부
    /// </summary>
    [JsonPropertyName("caseSensitive")]
    public bool CaseSensitive { get; init; }

    /// <summary>
    /// 같음 필터 생성
    /// </summary>
    /// <param name="field">필드명</param>
    /// <param name="value">값</param>
    /// <returns>필터 필드</returns>
    public static FilterField Equal(string field, object? value)
    {
        return new FilterField
        {
            Field = field,
            Operator = FilterOperator.Equal,
            Value = value
        };
    }

    /// <summary>
    /// 포함 필터 생성
    /// </summary>
    /// <param name="field">필드명</param>
    /// <param name="value">값</param>
    /// <returns>필터 필드</returns>
    public static FilterField Contains(string field, string value)
    {
        return new FilterField
        {
            Field = field,
            Operator = FilterOperator.Contains,
            Value = value
        };
    }

    /// <summary>
    /// 범위 필터 생성
    /// </summary>
    /// <param name="field">필드명</param>
    /// <param name="min">최소값</param>
    /// <param name="max">최대값</param>
    /// <returns>필터 필드</returns>
    public static FilterField Range(string field, object min, object max)
    {
        return new FilterField
        {
            Field = field,
            Operator = FilterOperator.Between,
            Value = new { Min = min, Max = max }
        };
    }
}

/// <summary>
/// 필터 연산자
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FilterOperator
{
    /// <summary>
    /// 같음
    /// </summary>
    Equal,

    /// <summary>
    /// 다름
    /// </summary>
    NotEqual,

    /// <summary>
    /// 포함
    /// </summary>
    Contains,

    /// <summary>
    /// 포함하지 않음
    /// </summary>
    NotContains,

    /// <summary>
    /// 시작함
    /// </summary>
    StartsWith,

    /// <summary>
    /// 끝남
    /// </summary>
    EndsWith,

    /// <summary>
    /// 보다 큼
    /// </summary>
    GreaterThan,

    /// <summary>
    /// 보다 크거나 같음
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// 보다 작음
    /// </summary>
    LessThan,

    /// <summary>
    /// 보다 작거나 같음
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// 사이 (범위)
    /// </summary>
    Between,

    /// <summary>
    /// 포함됨 (배열)
    /// </summary>
    In,

    /// <summary>
    /// 포함되지 않음 (배열)
    /// </summary>
    NotIn,

    /// <summary>
    /// null임
    /// </summary>
    IsNull,

    /// <summary>
    /// null이 아님
    /// </summary>
    IsNotNull,

    /// <summary>
    /// 비어있음
    /// </summary>
    IsEmpty,

    /// <summary>
    /// 비어있지 않음
    /// </summary>
    IsNotEmpty
}