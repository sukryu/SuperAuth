using System.Security.Cryptography;
using System.Text;

namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 강타입 세션 ID를 나타내는 값 객체
/// </summary>
public sealed record SessionId
{
    /// <summary>
    /// 세션 ID 값
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 세션 ID 길이 (기본 32자리)
    /// </summary>
    public const int DefaultLength = 32;

    /// <summary>
    /// 최소 세션 ID 길이
    /// </summary>
    public const int MinLength = 16;

    /// <summary>
    /// 최대 세션 ID 길이
    /// </summary>
    public const int MaxLength = 128;

    /// <summary>
    /// 세션 ID가 생성된 시간 (선택적)
    /// </summary>
    public DateTimeOffset? CreatedAt { get; init; }

    /// <summary>
    /// 세션 ID 생성
    /// </summary>
    /// <param name="value">세션 ID 문자열</param>
    /// <param name="createdAt">생성 시간</param>
    /// <exception cref="ArgumentException">유효하지 않은 세션 ID</exception>
    public SessionId(string value, DateTimeOffset? createdAt = null)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("세션 ID는 필수입니다.", nameof(value));

        if (value.Length < MinLength || value.Length > MaxLength)
            throw new ArgumentException(
                $"세션 ID 길이는 {MinLength}자에서 {MaxLength}자 사이여야 합니다.", 
                nameof(value));

        if (!IsValidSessionId(value))
            throw new ArgumentException($"유효하지 않은 세션 ID 형식입니다: {value}", nameof(value));

        Value = value;
        CreatedAt = createdAt ?? DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// 새로운 세션 ID 생성 (암호학적으로 안전한 난수 사용)
    /// </summary>
    /// <param name="length">세션 ID 길이 (기본 32자리)</param>
    /// <returns>새로운 세션 ID</returns>
    public static SessionId New(int length = DefaultLength)
    {
        if (length < MinLength || length > MaxLength)
            throw new ArgumentOutOfRangeException(
                nameof(length), 
                length, 
                $"세션 ID 길이는 {MinLength}자에서 {MaxLength}자 사이여야 합니다.");

        var sessionId = GenerateSecureRandomString(length);
        return new SessionId(sessionId, DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// GUID 기반 세션 ID 생성
    /// </summary>
    public static SessionId NewFromGuid()
    {
        var guid = Guid.NewGuid().ToString("N"); // 32자리 hex 문자열
        return new SessionId(guid, DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// 타임스탬프를 포함한 세션 ID 생성
    /// </summary>
    /// <param name="length">랜덤 부분의 길이</param>
    public static SessionId NewWithTimestamp(int length = 24)
    {
        if (length < 16 || length > 120)
            throw new ArgumentOutOfRangeException(nameof(length));

        var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString("x"); // hex timestamp
        var randomPart = GenerateSecureRandomString(length);
        var sessionId = $"{timestamp}_{randomPart}";
        
        return new SessionId(sessionId, DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// 암호학적으로 안전한 랜덤 문자열 생성
    /// </summary>
    private static string GenerateSecureRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var result = new StringBuilder(length);
        
        using var rng = RandomNumberGenerator.Create();
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[bytes[i] % chars.Length]);
        }
        
        return result.ToString();
    }

    /// <summary>
    /// 세션 ID 형식 검증
    /// </summary>
    public static bool IsValidSessionId(string sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId))
            return false;

        if (sessionId.Length < MinLength || sessionId.Length > MaxLength)
            return false;

        // 영문자, 숫자, 하이픈, 언더스코어만 허용
        return sessionId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
    }

    /// <summary>
    /// 문자열에서 파싱 시도
    /// </summary>
    public static bool TryParse(string? value, out SessionId? sessionId)
    {
        sessionId = null;
        
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            sessionId = new SessionId(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 세션 ID의 해시값 계산 (로깅 등에 사용)
    /// </summary>
    public string ToHashString()
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Value));
        return Convert.ToHexString(hashBytes)[..16]; // 처음 16자리만 사용
    }

    /// <summary>
    /// 마스킹된 세션 ID (로깅용)
    /// 예: abc123...xyz789 (처음 6자리 + ... + 마지막 6자리)
    /// </summary>
    public string ToMaskedString()
    {
        if (Value.Length <= 12)
            return new string('*', Value.Length);

        var prefix = Value[..6];
        var suffix = Value[^6..];
        return $"{prefix}...{suffix}";
    }

    /// <summary>
    /// 세션 ID가 만료되었는지 확인
    /// </summary>
    /// <param name="sessionTimeout">세션 타임아웃 (기본 30분)</param>
    public bool IsExpired(TimeSpan? sessionTimeout = null)
    {
        if (CreatedAt == null)
            return false;

        var timeout = sessionTimeout ?? TimeSpan.FromMinutes(30);
        return DateTimeOffset.UtcNow - CreatedAt.Value > timeout;
    }

    /// <summary>
    /// 세션 수명 계산
    /// </summary>
    public TimeSpan? GetAge()
    {
        return CreatedAt == null ? null : DateTimeOffset.UtcNow - CreatedAt.Value;
    }

    /// <summary>
    /// 암시적 변환: SessionId → string
    /// </summary>
    public static implicit operator string(SessionId sessionId) => sessionId.Value;

    /// <summary>
    /// 명시적 변환: string → SessionId
    /// </summary>
    public static explicit operator SessionId(string value) => new(value);

    public override string ToString() => Value;
}