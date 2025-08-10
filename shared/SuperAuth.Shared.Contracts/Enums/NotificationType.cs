namespace SuperAuth.Shared.Contracts.Enums;

/// <summary>
/// 알림 유형을 정의하는 열거형
/// </summary>
[Flags]
public enum NotificationType
{
    /// <summary>
    /// 알림 없음
    /// </summary>
    None = 0,

    /// <summary>
    /// 이메일 알림
    /// </summary>
    Email = 1 << 0,

    /// <summary>
    /// SMS 알림
    /// </summary>
    Sms = 1 << 1,

    /// <summary>
    /// 모바일 푸시 알림
    /// </summary>
    Push = 1 << 2,

    /// <summary>
    /// 웹 푸시 알림
    /// </summary>
    WebPush = 1 << 3,

    /// <summary>
    /// 인앱 알림 (대시보드 내)
    /// </summary>
    InApp = 1 << 4,

    /// <summary>
    /// 웹훅 알림
    /// </summary>
    Webhook = 1 << 5,

    /// <summary>
    /// Slack 알림
    /// </summary>
    Slack = 1 << 6,

    /// <summary>
    /// Microsoft Teams 알림
    /// </summary>
    Teams = 1 << 7,

    /// <summary>
    /// Discord 알림
    /// </summary>
    Discord = 1 << 8,

    /// <summary>
    /// 전화 알림 (음성 통화)
    /// </summary>
    Voice = 1 << 9,

    /// <summary>
    /// 브라우저 데스크톱 알림
    /// </summary>
    Desktop = 1 << 10,

    /// <summary>
    /// 시스템 로그 알림
    /// </summary>
    SystemLog = 1 << 11,

    /// <summary>
    /// SIEM 시스템 연동 알림
    /// </summary>
    Siem = 1 << 12,

    /// <summary>
    /// 모든 알림 유형
    /// </summary>
    All = Email | Sms | Push | WebPush | InApp | Webhook | Slack | Teams | Discord | Voice | Desktop | SystemLog | Siem
}