namespace SuperAuth.Shared.Contracts.Interfaces;

/// <summary>
/// 시간 추상화 인터페이스 (테스트 가능성을 위한)
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// 현재 UTC 시간
    /// </summary>
    DateTimeOffset UtcNow { get; }

    /// <summary>
    /// 현재 로컬 시간
    /// </summary>
    DateTimeOffset Now { get; }

    /// <summary>
    /// 오늘 날짜 (UTC)
    /// </summary>
    DateOnly UtcToday { get; }

    /// <summary>
    /// 오늘 날짜 (로컬)
    /// </summary>
    DateOnly Today { get; }

    /// <summary>
    /// Unix 타임스탬프 (초)
    /// </summary>
    long UnixTimestamp { get; }

    /// <summary>
    /// Unix 타임스탬프 (밀리초)
    /// </summary>
    long UnixTimestampMilliseconds { get; }

    /// <summary>
    /// 지정된 시간대로 변환
    /// </summary>
    /// <param name="dateTime">변환할 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>변환된 시간</returns>
    DateTimeOffset ConvertToTimeZone(DateTimeOffset dateTime, string timeZoneId);

    /// <summary>
    /// Unix 타임스탬프를 DateTimeOffset으로 변환
    /// </summary>
    /// <param name="unixTimestamp">Unix 타임스탬프 (초)</param>
    /// <returns>DateTimeOffset</returns>
    DateTimeOffset FromUnixTimestamp(long unixTimestamp);

    /// <summary>
    /// Unix 타임스탬프를 DateTimeOffset으로 변환 (밀리초)
    /// </summary>
    /// <param name="unixTimestampMilliseconds">Unix 타임스탬프 (밀리초)</param>
    /// <returns>DateTimeOffset</returns>
    DateTimeOffset FromUnixTimestampMilliseconds(long unixTimestampMilliseconds);

    /// <summary>
    /// DateTimeOffset을 Unix 타임스탬프로 변환
    /// </summary>
    /// <param name="dateTime">변환할 시간</param>
    /// <returns>Unix 타임스탬프 (초)</returns>
    long ToUnixTimestamp(DateTimeOffset dateTime);

    /// <summary>
    /// DateTimeOffset을 Unix 타임스탬프로 변환 (밀리초)
    /// </summary>
    /// <param name="dateTime">변환할 시간</param>
    /// <returns>Unix 타임스탬프 (밀리초)</returns>
    long ToUnixTimestampMilliseconds(DateTimeOffset dateTime);

    /// <summary>
    /// 시간 범위 확인
    /// </summary>
    /// <param name="dateTime">확인할 시간</param>
    /// <param name="startTime">시작 시간</param>
    /// <param name="endTime">종료 시간</param>
    /// <returns>범위 내 포함 여부</returns>
    bool IsInRange(DateTimeOffset dateTime, DateTimeOffset startTime, DateTimeOffset endTime);

    /// <summary>
    /// 영업일 여부 확인 (월-금, 공휴일 제외)
    /// </summary>
    /// <param name="date">확인할 날짜</param>
    /// <param name="timeZoneId">시간대 ID (기본: UTC)</param>
    /// <returns>영업일 여부</returns>
    bool IsBusinessDay(DateOnly date, string? timeZoneId = null);

    /// <summary>
    /// 주말 여부 확인
    /// </summary>
    /// <param name="date">확인할 날짜</param>
    /// <returns>주말 여부</returns>
    bool IsWeekend(DateOnly date);

    /// <summary>
    /// 나이 계산
    /// </summary>
    /// <param name="birthDate">생년월일</param>
    /// <param name="referenceDate">기준 날짜 (기본: 오늘)</param>
    /// <returns>나이</returns>
    int CalculateAge(DateOnly birthDate, DateOnly? referenceDate = null);

    /// <summary>
    /// 두 날짜 간의 영업일 수 계산
    /// </summary>
    /// <param name="startDate">시작 날짜</param>
    /// <param name="endDate">종료 날짜</param>
    /// <param name="timeZoneId">시간대 ID (기본: UTC)</param>
    /// <returns>영업일 수</returns>
    int CalculateBusinessDays(DateOnly startDate, DateOnly endDate, string? timeZoneId = null);
}

/// <summary>
/// 시간 추상화 인터페이스의 확장 (고급 기능)
/// </summary>
public interface IAdvancedDateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// 지원되는 시간대 목록 조회
    /// </summary>
    IReadOnlyList<TimeZoneInfo> GetAvailableTimeZones();

    /// <summary>
    /// 시간대 정보 조회
    /// </summary>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>시간대 정보</returns>
    TimeZoneInfo? GetTimeZoneInfo(string timeZoneId);

    /// <summary>
    /// 서머타임 여부 확인
    /// </summary>
    /// <param name="dateTime">확인할 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>서머타임 여부</returns>
    bool IsDaylightSavingTime(DateTimeOffset dateTime, string timeZoneId);

    /// <summary>
    /// 월의 마지막 날짜 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="month">월</param>
    /// <returns>마지막 날짜</returns>
    DateOnly GetLastDayOfMonth(int year, int month);

    /// <summary>
    /// 월의 첫 번째 특정 요일 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="month">월</param>
    /// <param name="dayOfWeek">요일</param>
    /// <returns>첫 번째 해당 요일</returns>
    DateOnly GetFirstDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek);

    /// <summary>
    /// 월의 마지막 특정 요일 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="month">월</param>
    /// <param name="dayOfWeek">요일</param>
    /// <returns>마지막 해당 요일</returns>
    DateOnly GetLastDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek);

    /// <summary>
    /// 월의 N번째 특정 요일 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="month">월</param>
    /// <param name="dayOfWeek">요일</param>
    /// <param name="occurrence">N번째 (1-5)</param>
    /// <returns>N번째 해당 요일 (없으면 null)</returns>
    DateOnly? GetNthDayOfWeekInMonth(int year, int month, DayOfWeek dayOfWeek, int occurrence);

    /// <summary>
    /// 분기의 시작 날짜 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="quarter">분기 (1-4)</param>
    /// <returns>분기 시작 날짜</returns>
    DateOnly GetQuarterStart(int year, int quarter);

    /// <summary>
    /// 분기의 종료 날짜 조회
    /// </summary>
    /// <param name="year">년도</param>
    /// <param name="quarter">분기 (1-4)</param>
    /// <returns>분기 종료 날짜</returns>
    DateOnly GetQuarterEnd(int year, int quarter);

    /// <summary>
    /// 현재 분기 조회
    /// </summary>
    /// <param name="date">기준 날짜</param>
    /// <returns>분기 번호 (1-4)</returns>
    int GetQuarter(DateOnly date);

    /// <summary>
    /// 윤년 여부 확인
    /// </summary>
    /// <param name="year">년도</param>
    /// <returns>윤년 여부</returns>
    bool IsLeapYear(int year);

    /// <summary>
    /// ISO 주차 계산
    /// </summary>
    /// <param name="date">날짜</param>
    /// <returns>ISO 주차 번호</returns>
    int GetIsoWeekOfYear(DateOnly date);

    /// <summary>
    /// 날짜 형식 검증
    /// </summary>
    /// <param name="dateString">날짜 문자열</param>
    /// <param name="format">형식 패턴</param>
    /// <returns>유효한 날짜 여부</returns>
    bool IsValidDateFormat(string dateString, string format);

    /// <summary>
    /// 상대적 시간 표현 (예: "2시간 전", "3일 후")
    /// </summary>
    /// <param name="dateTime">기준 시간</param>
    /// <param name="referenceTime">비교 기준 시간 (기본: 현재)</param>
    /// <param name="culture">문화권 (기본: 현재)</param>
    /// <returns>상대적 시간 표현</returns>
    string GetRelativeTime(DateTimeOffset dateTime, DateTimeOffset? referenceTime = null, string? culture = null);
}

/// <summary>
/// 테스트용 시간 제공자 인터페이스
/// </summary>
public interface ITestDateTimeProvider : IAdvancedDateTimeProvider
{
    /// <summary>
    /// 현재 시간 설정 (테스트용)
    /// </summary>
    /// <param name="dateTime">설정할 시간</param>
    void SetCurrentTime(DateTimeOffset dateTime);

    /// <summary>
    /// 시간 이동 (테스트용)
    /// </summary>
    /// <param name="timeSpan">이동할 시간</param>
    void AdvanceTime(TimeSpan timeSpan);

    /// <summary>
    /// 시간 재설정 (실제 현재 시간으로)
    /// </summary>
    void Reset();

    /// <summary>
    /// 고정 시간 모드 여부
    /// </summary>
    bool IsFixed { get; }
}

/// <summary>
/// 캐시 가능한 시간 제공자 인터페이스
/// </summary>
public interface ICachedDateTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// 캐시 만료 시간 설정
    /// </summary>
    TimeSpan CacheExpiration { get; set; }

    /// <summary>
    /// 캐시된 시간 무효화
    /// </summary>
    void InvalidateCache();

    /// <summary>
    /// 캐시 적중률 조회
    /// </summary>
    double CacheHitRatio { get; }
}

/// <summary>
/// 시간대별 비즈니스 시간 관리 인터페이스
/// </summary>
public interface IBusinessTimeProvider : IDateTimeProvider
{
    /// <summary>
    /// 비즈니스 시간 설정
    /// </summary>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <param name="startTime">업무 시작 시간</param>
    /// <param name="endTime">업무 종료 시간</param>
    /// <param name="workingDays">업무일 (기본: 월-금)</param>
    void SetBusinessHours(
        string timeZoneId, 
        TimeOnly startTime, 
        TimeOnly endTime, 
        DayOfWeek[]? workingDays = null);

    /// <summary>
    /// 공휴일 추가
    /// </summary>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <param name="holiday">공휴일</param>
    /// <param name="name">공휴일 이름</param>
    void AddHoliday(string timeZoneId, DateOnly holiday, string name);

    /// <summary>
    /// 공휴일 제거
    /// </summary>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <param name="holiday">공휴일</param>
    void RemoveHoliday(string timeZoneId, DateOnly holiday);

    /// <summary>
    /// 비즈니스 시간 내 여부 확인
    /// </summary>
    /// <param name="dateTime">확인할 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>비즈니스 시간 내 여부</returns>
    bool IsBusinessHours(DateTimeOffset dateTime, string timeZoneId);

    /// <summary>
    /// 다음 비즈니스 시간 조회
    /// </summary>
    /// <param name="dateTime">기준 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>다음 비즈니스 시간</returns>
    DateTimeOffset GetNextBusinessTime(DateTimeOffset dateTime, string timeZoneId);

    /// <summary>
    /// 이전 비즈니스 시간 조회
    /// </summary>
    /// <param name="dateTime">기준 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>이전 비즈니스 시간</returns>
    DateTimeOffset GetPreviousBusinessTime(DateTimeOffset dateTime, string timeZoneId);

    /// <summary>
    /// 비즈니스 시간 계산 (점심시간 등 제외)
    /// </summary>
    /// <param name="startTime">시작 시간</param>
    /// <param name="endTime">종료 시간</param>
    /// <param name="timeZoneId">시간대 ID</param>
    /// <returns>실제 비즈니스 시간</returns>
    TimeSpan CalculateBusinessTime(DateTimeOffset startTime, DateTimeOffset endTime, string timeZoneId);
}