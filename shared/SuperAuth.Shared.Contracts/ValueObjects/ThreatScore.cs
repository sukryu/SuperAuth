using SuperAuth.Shared.Contracts.Enums;

namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 위협 점수를 나타내는 값 객체 (0-100 범위)
/// </summary>
public sealed record ThreatScore : IComparable<ThreatScore>
{
    /// <summary>
    /// 최소 위협 점수
    /// </summary>
    public const int MinValue = 0;

    /// <summary>
    /// 최대 위협 점수
    /// </summary>
    public const int MaxValue = 100;

    /// <summary>
    /// 위협 점수 값 (0-100)
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// 위협 수준
    /// </summary>
    public ThreatLevel Level => CalculateThreatLevel(Value);

    /// <summary>
    /// 위협 점수가 임계값을 초과하는지 여부
    /// </summary>
    public bool IsHigh => Value >= 51;

    /// <summary>
    /// 위험한 수준인지 여부 (76점 이상)
    /// </summary>
    public bool IsDangerous => Value >= 76;

    /// <summary>
    /// 심각한 수준인지 여부 (91점 이상)
    /// </summary>
    public bool IsCritical => Value >= 91;

    /// <summary>
    /// 안전한 수준인지 여부 (25점 이하)
    /// </summary>
    public bool IsSafe => Value <= 25;

    /// <summary>
    /// 위협 점수 생성
    /// </summary>
    /// <param name="value">점수 값 (0-100)</param>
    /// <exception cref="ArgumentOutOfRangeException">범위를 벗어난 값</exception>
    public ThreatScore(int value)
    {
        if (value < MinValue || value > MaxValue)
            throw new ArgumentOutOfRangeException(
                nameof(value), 
                value, 
                $"위협 점수는 {MinValue}에서 {MaxValue} 사이여야 합니다.");

        Value = value;
    }

    /// <summary>
    /// 위협 점수 생성 (double 값을 int로 변환)
    /// </summary>
    /// <param name="value">점수 값 (0.0-100.0)</param>
    public ThreatScore(double value) : this((int)Math.Round(Math.Clamp(value, MinValue, MaxValue)))
    {
    }

    /// <summary>
    /// 점수로부터 위협 수준 계산
    /// </summary>
    private static ThreatLevel CalculateThreatLevel(int score) => score switch
    {
        >= 91 => ThreatLevel.Critical,
        >= 76 => ThreatLevel.VeryHigh,
        >= 51 => ThreatLevel.High,
        >= 26 => ThreatLevel.Medium,
        >= 1 => ThreatLevel.Low,
        0 => ThreatLevel.Low,
        _ => ThreatLevel.Unknown
    };

    /// <summary>
    /// 백분율로 표시
    /// </summary>
    public double ToPercentage() => Value / 100.0;

    /// <summary>
    /// 정규화된 값 (0.0-1.0)
    /// </summary>
    public double ToNormalized() => Value / 100.0;

    /// <summary>
    /// 색상 코드 반환 (UI 표시용)
    /// </summary>
    public string ToColorCode() => Level switch
    {
        ThreatLevel.Low => "#28a745",      // 녹색
        ThreatLevel.Medium => "#ffc107",   // 노란색
        ThreatLevel.High => "#fd7e14",     // 주황색
        ThreatLevel.VeryHigh => "#dc3545", // 빨간색
        ThreatLevel.Critical => "#6f42c1", // 보라색
        _ => "#6c757d"                     // 회색
    };

    /// <summary>
    /// 위협 점수 증가
    /// </summary>
    /// <param name="increment">증가량</param>
    /// <returns>새로운 위협 점수 (최대값 제한)</returns>
    public ThreatScore Increase(int increment)
    {
        return new ThreatScore(Math.Min(Value + increment, MaxValue));
    }

    /// <summary>
    /// 위협 점수 감소
    /// </summary>
    /// <param name="decrement">감소량</param>
    /// <returns>새로운 위협 점수 (최소값 제한)</returns>
    public ThreatScore Decrease(int decrement)
    {
        return new ThreatScore(Math.Max(Value - decrement, MinValue));
    }

    /// <summary>
    /// 두 위협 점수의 평균 계산
    /// </summary>
    public static ThreatScore Average(ThreatScore score1, ThreatScore score2)
    {
        return new ThreatScore((score1.Value + score2.Value) / 2);
    }

    /// <summary>
    /// 여러 위협 점수의 평균 계산
    /// </summary>
    public static ThreatScore Average(params ThreatScore[] scores)
    {
        if (scores == null || scores.Length == 0)
            throw new ArgumentException("최소 하나의 점수가 필요합니다.", nameof(scores));

        var sum = scores.Sum(s => s.Value);
        return new ThreatScore(sum / scores.Length);
    }

    /// <summary>
    /// 여러 위협 점수의 최대값
    /// </summary>
    public static ThreatScore Max(params ThreatScore[] scores)
    {
        if (scores == null || scores.Length == 0)
            throw new ArgumentException("최소 하나의 점수가 필요합니다.", nameof(scores));

        return scores.Aggregate((s1, s2) => s1.Value > s2.Value ? s1 : s2);
    }

    /// <summary>
    /// 여러 위협 점수의 최소값
    /// </summary>
    public static ThreatScore Min(params ThreatScore[] scores)
    {
        if (scores == null || scores.Length == 0)
            throw new ArgumentException("최소 하나의 점수가 필요합니다.", nameof(scores));

        return scores.Aggregate((s1, s2) => s1.Value < s2.Value ? s1 : s2);
    }

    /// <summary>
    /// 가중 평균 계산
    /// </summary>
    /// <param name="scores">점수와 가중치 쌍</param>
    public static ThreatScore WeightedAverage(params (ThreatScore score, double weight)[] scores)
    {
        if (scores == null || scores.Length == 0)
            throw new ArgumentException("최소 하나의 점수가 필요합니다.", nameof(scores));

        var totalWeight = scores.Sum(s => s.weight);
        if (totalWeight == 0)
            throw new ArgumentException("가중치의 합이 0일 수 없습니다.", nameof(scores));

        var weightedSum = scores.Sum(s => s.score.Value * s.weight);
        return new ThreatScore(weightedSum / totalWeight);
    }

    /// <summary>
    /// 안전한 점수 생성 (0점)
    /// </summary>
    public static ThreatScore Safe => new(0);

    /// <summary>
    /// 중간 점수 생성 (50점)
    /// </summary>
    public static ThreatScore Medium => new(50);

    /// <summary>
    /// 위험한 점수 생성 (75점)
    /// </summary>
    public static ThreatScore Dangerous => new(75);

    /// <summary>
    /// 심각한 점수 생성 (95점)
    /// </summary>
    public static ThreatScore Critical => new(95);

    /// <summary>
    /// 최대 점수 생성 (100점)
    /// </summary>
    public static ThreatScore Maximum => new(100);

    /// <summary>
    /// IComparable 구현
    /// </summary>
    public int CompareTo(ThreatScore? other)
    {
        if (other is null) return 1;
        return Value.CompareTo(other.Value);
    }

    /// <summary>
    /// 암시적 변환: ThreatScore → int
    /// </summary>
    public static implicit operator int(ThreatScore score) => score.Value;

    /// <summary>
    /// 명시적 변환: int → ThreatScore
    /// </summary>
    public static explicit operator ThreatScore(int value) => new(value);

    /// <summary>
    /// 명시적 변환: double → ThreatScore
    /// </summary>
    public static explicit operator ThreatScore(double value) => new(value);

    /// <summary>
    /// 연산자 오버로딩: +
    /// </summary>
    public static ThreatScore operator +(ThreatScore left, ThreatScore right)
        => new(Math.Min(left.Value + right.Value, MaxValue));

    /// <summary>
    /// 연산자 오버로딩: -
    /// </summary>
    public static ThreatScore operator -(ThreatScore left, ThreatScore right)
        => new(Math.Max(left.Value - right.Value, MinValue));

    /// <summary>
    /// 연산자 오버로딩: >
    /// </summary>
    public static bool operator >(ThreatScore left, ThreatScore right)
        => left.Value > right.Value;

    /// <summary>
    /// 연산자 오버로딩: <
    /// </summary>
    public static bool operator <(ThreatScore left, ThreatScore right)
        => left.Value < right.Value;

    /// <summary>
    /// 연산자 오버로딩: >=
    /// </summary>
    public static bool operator >=(ThreatScore left, ThreatScore right)
        => left.Value >= right.Value;

    /// <summary>
    /// 연산자 오버로딩: <=
    /// </summary>
    public static bool operator <=(ThreatScore left, ThreatScore right)
        => left.Value <= right.Value;

    /// <summary>
    /// 위협 점수 설명 반환
    /// </summary>
    public string GetDescription() => Level switch
    {
        ThreatLevel.Low => "낮음 - 정상적인 활동으로 판단됩니다.",
        ThreatLevel.Medium => "보통 - 주의 깊은 모니터링이 필요합니다.",
        ThreatLevel.High => "높음 - 추가 보안 검증이 권장됩니다.",
        ThreatLevel.VeryHigh => "매우 높음 - 즉시 조치가 필요합니다.",
        ThreatLevel.Critical => "심각 - 차단 및 긴급 대응이 필요합니다.",
        _ => "알 수 없음 - 분석이 필요합니다."
    };

    /// <summary>
    /// JSON 직렬화용 문자열 표현
    /// </summary>
    public override string ToString() => $"{Value} ({Level})";

    /// <summary>
    /// 상세 문자열 표현
    /// </summary>
    public string ToDetailedString() => $"위협 점수: {Value}/100 ({Level}) - {GetDescription()}";
}