using System.Linq.Expressions;

namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 기본 저장소 인터페이스
/// </summary>
/// <typeparam name="TEntity">엔티티 타입</typeparam>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// ID로 엔티티 조회
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 모든 엔티티 조회
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티 목록 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 첫 번째 엔티티 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티가 존재하는지 확인
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티 개수 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 엔티티 추가
    /// </summary>
    /// <param name="entity">엔티티</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 여러 엔티티 일괄 추가
    /// </summary>
    /// <param name="entities">엔티티 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 엔티티 수정
    /// </summary>
    /// <param name="entity">엔티티</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// 여러 엔티티 일괄 수정
    /// </summary>
    /// <param name="entities">엔티티 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 엔티티 삭제
    /// </summary>
    /// <param name="entity">엔티티</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// ID로 엔티티 삭제
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 여러 엔티티 일괄 삭제
    /// </summary>
    /// <param name="entities">엔티티 목록</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티 일괄 삭제
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task DeleteWhereAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);
}

/// <summary>
/// 강타입 ID를 가진 저장소 인터페이스
/// </summary>
/// <typeparam name="TEntity">엔티티 타입</typeparam>
/// <typeparam name="TId">ID 타입</typeparam>
public interface IRepository<TEntity, in TId> : IRepository<TEntity>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    /// <summary>
    /// 강타입 ID로 엔티티 조회
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 강타입 ID로 엔티티 삭제
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task DeleteAsync(TId id, CancellationToken cancellationToken = default);
}

/// <summary>
/// 읽기 전용 저장소 인터페이스
/// </summary>
/// <typeparam name="TEntity">엔티티 타입</typeparam>
public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// ID로 엔티티 조회
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 모든 엔티티 조회
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티 목록 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 첫 번째 엔티티 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티가 존재하는지 확인
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 엔티티 개수 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> predicate, 
        CancellationToken cancellationToken = default);
}

/// <summary>
/// 페이징 지원 저장소 인터페이스
/// </summary>
/// <typeparam name="TEntity">엔티티 타입</typeparam>
public interface IPagedRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    /// <summary>
    /// 페이징된 엔티티 목록 조회
    /// </summary>
    /// <param name="pageNumber">페이지 번호 (1부터 시작)</param>
    /// <param name="pageSize">페이지 크기</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<PagedResult<TEntity>> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 조건에 맞는 페이징된 엔티티 목록 조회
    /// </summary>
    /// <param name="predicate">조건식</param>
    /// <param name="pageNumber">페이지 번호 (1부터 시작)</param>
    /// <param name="pageSize">페이지 크기</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<PagedResult<TEntity>> GetPagedAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber, 
        int pageSize, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 정렬 및 페이징된 엔티티 목록 조회
    /// </summary>
    /// <param name="pageNumber">페이지 번호 (1부터 시작)</param>
    /// <param name="pageSize">페이지 크기</param>
    /// <param name="orderBy">정렬 조건</param>
    /// <param name="ascending">오름차순 여부</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<PagedResult<TEntity>> GetPagedAsync<TKey>(
        int pageNumber,
        int pageSize,
        Expression<Func<TEntity, TKey>> orderBy,
        bool ascending = true,
        CancellationToken cancellationToken = default);
}

/// <summary>
/// 소프트 삭제 지원 저장소 인터페이스
/// </summary>
/// <typeparam name="TEntity">엔티티 타입</typeparam>
public interface ISoftDeletableRepository<TEntity> : IRepository<TEntity> 
    where TEntity : class, IEntity, ISoftDeletableEntity
{
    /// <summary>
    /// 삭제되지 않은 엔티티만 조회
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> GetActiveAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 삭제된 엔티티만 조회
    /// </summary>
    /// <param name="cancellationToken">취소 토큰</param>
    Task<IEnumerable<TEntity>> GetDeletedAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 소프트 삭제 수행
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="deletedBy">삭제자 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task SoftDeleteAsync(Guid id, Guid? deletedBy = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// 소프트 삭제 복원
    /// </summary>
    /// <param name="id">엔티티 ID</param>
    /// <param name="cancellationToken">취소 토큰</param>
    Task RestoreAsync(Guid id, CancellationToken cancellationToken = default);
}

/// <summary>
/// 페이징 결과
/// </summary>
/// <typeparam name="T">아이템 타입</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// 현재 페이지 아이템 목록
    /// </summary>
    public IReadOnlyList<T> Items { get; init; } = new List<T>();

    /// <summary>
    /// 현재 페이지 번호 (1부터 시작)
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// 페이지 크기
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// 총 아이템 개수
    /// </summary>
    public int TotalCount { get; init; }

    /// <summary>
    /// 총 페이지 개수
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// 이전 페이지 존재 여부
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// 다음 페이지 존재 여부
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// 첫 번째 페이지 여부
    /// </summary>
    public bool IsFirstPage => PageNumber == 1;

    /// <summary>
    /// 마지막 페이지 여부
    /// </summary>
    public bool IsLastPage => PageNumber == TotalPages;

    /// <summary>
    /// 현재 페이지의 첫 번째 아이템 번호
    /// </summary>
    public int FirstItemOnPage => (PageNumber - 1) * PageSize + 1;

    /// <summary>
    /// 현재 페이지의 마지막 아이템 번호
    /// </summary>
    public int LastItemOnPage => Math.Min(PageNumber * PageSize, TotalCount);

    /// <summary>
    /// 페이징된 결과 생성
    /// </summary>
    /// <param name="items">아이템 목록</param>
    /// <param name="totalCount">총 개수</param>
    /// <param name="pageNumber">페이지 번호</param>
    /// <param name="pageSize">페이지 크기</param>
    public static PagedResult<T> Create(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
    {
        return new PagedResult<T>
        {
            Items = items.ToList(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    /// <summary>
    /// 빈 페이징 결과 생성
    /// </summary>
    /// <param name="pageNumber">페이지 번호</param>
    /// <param name="pageSize">페이지 크기</param>
    public static PagedResult<T> Empty(int pageNumber, int pageSize)
    {
        return new PagedResult<T>
        {
            Items = new List<T>(),
            TotalCount = 0,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}