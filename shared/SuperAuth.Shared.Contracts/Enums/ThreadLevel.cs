namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 위협 수준을 정의하는 열거형
/// </summary>
public enum ThreatLevel
{
    /// <summary>
    /// 알 수 없음 (분석 대기 중)
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 낮음 - 일반적인 활동, 추가 조치 불필요
    /// 점수 범위: 0-25
    /// </summary>
    Low = 1,

    /// <summary>
    /// 보통 - 의심스러운 활동, 모니터링 필요
    /// 점수 범위: 26-50
    /// </summary>
    Medium = 2,

    /// <summary>
    /// 높음 - 위험한 활동, 추가 검증 필요
    /// 점수 범위: 51-75
    /// </summary>
    High = 3,

    /// <summary>
    /// 매우 높음 - 악성 활동 가능성, 즉시 차단 고려
    /// 점수 범위: 76-90
    /// </summary>
    VeryHigh = 4,

    /// <summary>
    /// 심각 - 확실한 공격, 즉시 차단 및 경고
    /// 점수 범위: 91-100
    /// </summary>
    Critical = 5,

    /// <summary>
    /// 차단됨 - 이미 차단된 위협
    /// </summary>
    Blocked = 6
}