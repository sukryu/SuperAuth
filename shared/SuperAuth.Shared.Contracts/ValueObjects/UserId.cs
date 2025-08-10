namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 강타입 사용자 ID를 나타내는 값 객체
/// </summary>
public sealed record UserId
{
    /// <summary>
    /// 사용자 ID 값
    /// </summary>
    public Guid Value { get; }

    /// <summary>
    /// 사용자 ID 생성
    /// </summary>
    /// <param name="value">GUID 값</param>
    /// <exception cref="ArgumentException">빈 GUID인 경우</exception>
    public UserId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("사용자 ID는 빈 GUID일 수 없습니다.", nameof(value));

        Value = value;
    }

    /// <summary>
    /// 문자열로부터 사용자 ID 생성
    /// </summary>
    /// <param name="value">GUID 문자열</param>
    /// <exception cref="ArgumentException">유효하지 않은 GUID 형식</exception>
    public UserId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("사용자 ID 문자열은 필수입니다.", nameof(value));

        if (!Guid.TryParse(value, out var guid) || guid == Guid.Empty)
            throw new ArgumentException($"유효하지 않은 사용자 ID 형식입니다: {value}", nameof(value));

        Value = guid;
    }

    /// <summary>
    /// 새로운 사용자 ID 생성
    /// </summary>
    public static UserId New() => new(Guid.NewGuid());

    /// <summary>
    /// 문자열에서 파싱 시도
    /// </summary>
    public static bool TryParse(string? value, out UserId? userId)
    {
        userId = null;
        
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!Guid.TryParse(value, out var guid) || guid == Guid.Empty)
            return false;

        userId = new UserId(guid);
        return true;
    }

    /// <summary>
    /// 짧은 형식의 ID (처음 8자리)
    /// </summary>
    public string ToShortString() => Value.ToString("N")[..8];

    /// <summary>
    /// Base64 인코딩된 형식
    /// </summary>
    public string ToBase64String() => Convert.ToBase64String(Value.ToByteArray());

    /// <summary>
    /// URL 안전한 Base64 형식
    /// </summary>
    public string ToUrlSafeBase64String()
    {
        var base64 = ToBase64String();
        return base64.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    /// <summary>
    /// 암시적 변환: UserId → Guid
    /// </summary>
    public static implicit operator Guid(UserId userId) => userId.Value;

    /// <summary>
    /// 명시적 변환: Guid → UserId
    /// </summary>
    public static explicit operator UserId(Guid value) => new(value);

    /// <summary>
    /// 명시적 변환: string → UserId
    /// </summary>
    public static explicit operator UserId(string value) => new(value);

    /// <summary>
    /// 암시적 변환: UserId → string
    /// </summary>
    public static implicit operator string(UserId userId) => userId.Value.ToString();

    public override string ToString() => Value.ToString();
}