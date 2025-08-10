namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 강타입 애플리케이션 ID를 나타내는 값 객체
/// </summary>
public sealed record ApplicationId
{
    /// <summary>
    /// 애플리케이션 ID 값
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// 클라이언트 ID (OAuth 클라이언트 ID)
    /// </summary>
    public string ClientId => Value.ToString("N");

    /// <summary>
    /// 애플리케이션 ID 생성
    /// </summary>
    /// <param name="value">GUID 값</param>
    /// <exception cref="ArgumentException">빈 GUID인 경우</exception>
    public ApplicationId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("애플리케이션 ID는 빈 GUID일 수 없습니다.", nameof(value));

        Value = value;
    }

    /// <summary>
    /// 문자열로부터 애플리케이션 ID 생성
    /// </summary>
    /// <param name="value">GUID 문자열</param>
    /// <exception cref="ArgumentException">유효하지 않은 GUID 형식</exception>
    public ApplicationId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("애플리케이션 ID 문자열은 필수입니다.", nameof(value));

        if (!Guid.TryParse(value, out var guid) || guid == Guid.Empty)
            throw new ArgumentException($"유효하지 않은 애플리케이션 ID 형식입니다: {value}", nameof(value));

        Value = guid;
    }

    /// <summary>
    /// 새로운 애플리케이션 ID 생성
    /// </summary>
    public static ApplicationId New() => new(Guid.NewGuid());

    /// <summary>
    /// 문자열에서 파싱 시도
    /// </summary>
    public static bool TryParse(string? value, out ApplicationId? applicationId)
    {
        applicationId = null;
        
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!Guid.TryParse(value, out var guid) || guid == Guid.Empty)
            return false;

        applicationId = new ApplicationId(guid);
        return true;
    }

    /// <summary>
    /// 클라이언트 ID에서 파싱 시도
    /// </summary>
    public static bool TryParseClientId(string? clientId, out ApplicationId? applicationId)
    {
        return TryParse(clientId, out applicationId);
    }

    /// <summary>
    /// 짧은 형식의 ID (처음 8자리)
    /// </summary>
    public string ToShortString() => Value.ToString("N")[..8];

    /// <summary>
    /// 사람이 읽기 쉬운 형식 (app_12345678)
    /// </summary>
    public string ToReadableString() => $"app_{ToShortString()}";

    /// <summary>
    /// 암시적 변환: ApplicationId → Guid
    /// </summary>
    public static implicit operator Guid(ApplicationId applicationId) => applicationId.Value;

    /// <summary>
    /// 명시적 변환: Guid → ApplicationId
    /// </summary>
    public static explicit operator ApplicationId(Guid value) => new(value);

    /// <summary>
    /// 명시적 변환: string → ApplicationId
    /// </summary>
    public static explicit operator ApplicationId(string value) => new(value);

    /// <summary>
    /// 암시적 변환: ApplicationId → string
    /// </summary>
    public static implicit operator string(ApplicationId applicationId) => applicationId.Value.ToString();

    public override string ToString() => Value.ToString();
}