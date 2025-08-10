using System.Text.RegularExpressions;

namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 이메일 주소를 나타내는 값 객체
/// </summary>
public sealed record Email
{
    private static readonly Regex EmailRegex = new(
        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    /// <summary>
    /// 이메일 주소 값
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 도메인 부분 (@ 이후)
    /// </summary>
    public string Domain => Value.Split('@')[1];

    /// <summary>
    /// 로컬 부분 (@ 이전)
    /// </summary>
    public string LocalPart => Value.Split('@')[0];

    /// <summary>
    /// 이메일이 검증된 상태인지 여부
    /// </summary>
    public bool IsVerified { get; init; }

    /// <summary>
    /// 이메일 객체 생성
    /// </summary>
    /// <param name="value">이메일 주소</param>
    /// <param name="isVerified">검증 상태 (기본값: false)</param>
    /// <exception cref="ArgumentException">유효하지 않은 이메일 형식</exception>
    public Email(string value, bool isVerified = false)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("이메일 주소는 필수입니다.", nameof(value));

        var normalizedValue = value.Trim().ToLowerInvariant();
        
        if (!IsValidEmail(normalizedValue))
            throw new ArgumentException($"유효하지 않은 이메일 형식입니다: {value}", nameof(value));

        if (normalizedValue.Length > 254) // RFC 5321 제한
            throw new ArgumentException("이메일 주소가 너무 깁니다. (최대 254자)", nameof(value));

        Value = normalizedValue;
        IsVerified = isVerified;
    }

    /// <summary>
    /// 이메일 형식 검증
    /// </summary>
    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            // 기본 정규식 검증
            if (!EmailRegex.IsMatch(email))
                return false;

            // 추가 검증
            var parts = email.Split('@');
            if (parts.Length != 2)
                return false;

            var localPart = parts[0];
            var domain = parts[1];

            // 로컬 부분 검증 (최대 64자)
            if (localPart.Length == 0 || localPart.Length > 64)
                return false;

            // 도메인 부분 검증 (최대 253자)
            if (domain.Length == 0 || domain.Length > 253)
                return false;

            // 연속된 점 검증
            if (email.Contains(".."))
                return false;

            // 시작/끝 점 검증
            if (localPart.StartsWith('.') || localPart.EndsWith('.'))
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 이메일 마스킹 (개인정보 보호)
    /// 예: john.doe@example.com → j***e@example.com
    /// </summary>
    public string ToMaskedString()
    {
        if (LocalPart.Length <= 2)
            return $"***@{Domain}";

        var firstChar = LocalPart[0];
        var lastChar = LocalPart[^1];
        var middleLength = Math.Max(LocalPart.Length - 2, 1);
        var masked = new string('*', middleLength);

        return $"{firstChar}{masked}{lastChar}@{Domain}";
    }

    /// <summary>
    /// 검증된 이메일로 변경
    /// </summary>
    public Email MarkAsVerified() => this with { IsVerified = true };

    /// <summary>
    /// 미검증 이메일로 변경
    /// </summary>
    public Email MarkAsUnverified() => this with { IsVerified = false };

    /// <summary>
    /// 암시적 변환: Email → string
    /// </summary>
    public static implicit operator string(Email email) => email.Value;

    /// <summary>
    /// 명시적 변환: string → Email
    /// </summary>
    public static explicit operator Email(string value) => new(value);

    public override string ToString() => Value;
}