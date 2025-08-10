using System.Text.RegularExpressions;

namespace SuperAuth.Shared.Contracts.ValueObjects;

/// <summary>
/// 전화번호를 나타내는 값 객체
/// </summary>
public sealed record PhoneNumber
{
    private static readonly Regex PhoneRegex = new(
        @"^\+[1-9]\d{1,14}$", // E.164 국제 표준 형식
        RegexOptions.Compiled);

    private static readonly Regex DigitsOnlyRegex = new(
        @"\D", // 숫자가 아닌 모든 문자
        RegexOptions.Compiled);

    /// <summary>
    /// E.164 형식의 전화번호 (+82101234567)
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// 국가 코드 (+82)
    /// </summary>
    public string CountryCode { get; }

    /// <summary>
    /// 국가 코드를 제외한 번호 (101234567)
    /// </summary>
    public string NationalNumber { get; }

    /// <summary>
    /// 전화번호가 검증된 상태인지 여부
    /// </summary>
    public bool IsVerified { get; init; }

    /// <summary>
    /// 전화번호 유형
    /// </summary>
    public PhoneNumberType Type { get; init; }

    /// <summary>
    /// 전화번호 객체 생성
    /// </summary>
    /// <param name="value">전화번호 (다양한 형식 허용)</param>
    /// <param name="isVerified">검증 상태 (기본값: false)</param>
    /// <param name="type">전화번호 유형 (기본값: Mobile)</param>
    /// <exception cref="ArgumentException">유효하지 않은 전화번호 형식</exception>
    public PhoneNumber(string value, bool isVerified = false, PhoneNumberType type = PhoneNumberType.Mobile)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("전화번호는 필수입니다.", nameof(value));

        var normalizedValue = NormalizePhoneNumber(value);
        
        if (!IsValidPhoneNumber(normalizedValue))
            throw new ArgumentException($"유효하지 않은 전화번호 형식입니다: {value}", nameof(value));

        Value = normalizedValue;
        IsVerified = isVerified;
        Type = type;

        // 국가 코드와 국내 번호 분리
        var (countryCode, nationalNumber) = ExtractCountryCodeAndNationalNumber(normalizedValue);
        CountryCode = countryCode;
        NationalNumber = nationalNumber;
    }

    /// <summary>
    /// 전화번호 정규화 (E.164 형식으로 변환)
    /// </summary>
    private static string NormalizePhoneNumber(string phoneNumber)
    {
        // 공백, 하이픈, 괄호 등 제거
        var digitsOnly = DigitsOnlyRegex.Replace(phoneNumber, "");

        // 이미 +로 시작하는 경우
        if (phoneNumber.StartsWith('+'))
            return '+' + digitsOnly;

        // 한국 번호 처리
        if (digitsOnly.StartsWith("82"))
            return '+' + digitsOnly;
        
        if (digitsOnly.StartsWith("010") || digitsOnly.StartsWith("011") || 
            digitsOnly.StartsWith("016") || digitsOnly.StartsWith("017") ||
            digitsOnly.StartsWith("018") || digitsOnly.StartsWith("019"))
            return "+82" + digitsOnly[1..]; // 0 제거 후 +82 추가

        // 미국 번호 처리 (1로 시작하거나 11자리)
        if (digitsOnly.StartsWith("1") && digitsOnly.Length == 11)
            return '+' + digitsOnly;
        
        if (digitsOnly.Length == 10) // 미국 국내 번호
            return "+1" + digitsOnly;

        // 기타 경우 +를 붙여서 반환
        return '+' + digitsOnly;
    }

    /// <summary>
    /// 전화번호 형식 검증
    /// </summary>
    public static bool IsValidPhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return false;

        try
        {
            var normalized = NormalizePhoneNumber(phoneNumber);
            return PhoneRegex.IsMatch(normalized) && normalized.Length <= 16; // E.164 최대 길이
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 국가 코드와 국내 번호 분리
    /// </summary>
    private static (string countryCode, string nationalNumber) ExtractCountryCodeAndNationalNumber(string e164Number)
    {
        // +를 제거한 후 처리
        var digits = e164Number[1..];

        // 주요 국가 코드들 (길이순으로 정렬)
        var countryCodes = new[]
        {
            "1",    // 미국, 캐나다
            "7",    // 러시아, 카자흐스탄
            "20",   // 이집트
            "27",   // 남아프리카
            "30",   // 그리스
            "31",   // 네덜란드
            "32",   // 벨기에
            "33",   // 프랑스
            "34",   // 스페인
            "36",   // 헝가리
            "39",   // 이탈리아
            "40",   // 루마니아
            "41",   // 스위스
            "43",   // 오스트리아
            "44",   // 영국
            "45",   // 덴마크
            "46",   // 스웨덴
            "47",   // 노르웨이
            "48",   // 폴란드
            "49",   // 독일
            "51",   // 페루
            "52",   // 멕시코
            "53",   // 쿠바
            "54",   // 아르헨티나
            "55",   // 브라질
            "56",   // 칠레
            "57",   // 콜롬비아
            "58",   // 베네수엘라
            "60",   // 말레이시아
            "61",   // 호주
            "62",   // 인도네시아
            "63",   // 필리핀
            "64",   // 뉴질랜드
            "65",   // 싱가포르
            "66",   // 태국
            "81",   // 일본
            "82",   // 한국
            "84",   // 베트남
            "86",   // 중국
            "90",   // 터키
            "91",   // 인도
            "92",   // 파키스탄
            "93",   // 아프가니스탄
            "94",   // 스리랑카
            "95",   // 미얀마
            "98",   // 이란
            "212",  // 모로코
            "213",  // 알제리
            "216",  // 튀니지
            "218",  // 리비아
            "220",  // 감비아
            "221",  // 세네갈
            "222",  // 모리타니
            "223",  // 말리
            "224",  // 기니
            "225",  // 코트디부아르
            "226",  // 부르키나파소
            "227",  // 니제르
            "228",  // 토고
            "229",  // 베냉
            "230",  // 모리셔스
            "231",  // 라이베리아
            "232",  // 시에라리온
            "233",  // 가나
            "234",  // 나이지리아
            "235",  // 차드
            "236",  // 중앙아프리카공화국
            "237",  // 카메룬
            "238",  // 카보베르데
            "239",  // 상투메프린시페
            "240",  // 적도기니
            "241",  // 가봉
            "242",  // 콩고공화국
            "243",  // 콩고민주공화국
            "244",  // 앙골라
            "245",  // 기니비사우
            "246",  // 영국령 인도양 지역
            "248",  // 세이셸
            "249",  // 수단
            "250",  // 르완다
            "251",  // 에티오피아
            "252",  // 소말리아
            "253",  // 지부티
            "254",  // 케냐
            "255",  // 탄자니아
            "256",  // 우간다
            "257",  // 부룬디
            "258",  // 모잠비크
            "260",  // 잠비아
            "261",  // 마다가스카르
            "262",  // 레위니옹, 마요트
            "263",  // 짐바브웨
            "264",  // 나미비아
            "265",  // 말라위
            "266",  // 레소토
            "267",  // 보츠와나
            "268",  // 에스와티니
            "269",  // 코모로
            "290",  // 세인트헬레나
            "291",  // 에리트레아
            "297",  // 아루바
            "298",  // 페로 제도
            "299",  // 그린란드
            "350",  // 지브롤터
            "351",  // 포르투갈
            "352",  // 룩셈부르크
            "353",  // 아일랜드
            "354",  // 아이슬란드
            "355",  // 알바니아
            "356",  // 몰타
            "357",  // 키프로스
            "358",  // 핀란드
            "359",  // 불가리아
            "370",  // 리투아니아
            "371",  // 라트비아
            "372",  // 에스토니아
            "373",  // 몰도바
            "374",  // 아르메니아
            "375",  // 벨라루스
            "376",  // 안도라
            "377",  // 모나코
            "378",  // 산마리노
            "380",  // 우크라이나
            "381",  // 세르비아
            "382",  // 몬테네그로
            "383",  // 코소보
            "385",  // 크로아티아
            "386",  // 슬로베니아
            "387",  // 보스니아 헤르체고비나
            "389",  // 북마케도니아
            "420",  // 체코
            "421",  // 슬로바키아
            "423",  // 리히텐슈타인
            "500",  // 포클랜드 제도
            "501",  // 벨리즈
            "502",  // 과테말라
            "503",  // 엘살바도르
            "504",  // 온두라스
            "505",  // 니카라과
            "506",  // 코스타리카
            "507",  // 파나마
            "508",  // 생피에르 미클롱
            "509",  // 아이티
            "590",  // 과들루프
            "591",  // 볼리비아
            "592",  // 가이아나
            "593",  // 에콰도르
            "594",  // 프랑스령 기아나
            "595",  // 파라과이
            "596",  // 마르티니크
            "597",  // 수리남
            "598",  // 우루과이
            "599",  // 네덜란드령 안틸레스
            "670",  // 동티모르
            "672",  // 노퍽 섬
            "673",  // 브루나이
            "674",  // 나우루
            "675",  // 파푸아뉴기니
            "676",  // 통가
            "677",  // 솔로몬 제도
            "678",  // 바누아투
            "679",  // 피지
            "680",  // 팔라우
            "681",  // 왈리스 푸투나
            "682",  // 쿡 제도
            "683",  // 니우에
            "684",  // 아메리칸사모아
            "685",  // 사모아
            "686",  // 키리바시
            "687",  // 뉴칼레도니아
            "688",  // 투발루
            "689",  // 프랑스령 폴리네시아
            "690",  // 토켈라우
            "691",  // 미크로네시아
            "692",  // 마셜 제도
            "850",  // 북한
            "852",  // 홍콩
            "853",  // 마카오
            "855",  // 캄보디아
            "856",  // 라오스
            "880",  // 방글라데시
            "886",  // 대만
            "960",  // 몰디브
            "961",  // 레바논
            "962",  // 요단
            "963",  // 시리아
            "964",  // 이라크
            "965",  // 쿠웨이트
            "966",  // 사우디아라비아
            "967",  // 예멘
            "968",  // 오만
            "970",  // 팔레스타인
            "971",  // 아랍에미리트
            "972",  // 이스라엘
            "973",  // 바레인
            "974",  // 카타르
            "975",  // 부탄
            "976",  // 몽골
            "977",  // 네팔
            "992",  // 타지키스탄
            "993",  // 투르크메니스탄
            "994",  // 아제르바이잔
            "995",  // 조지아
            "996",  // 키르기스스탄
            "998"   // 우즈베키스탄
        };

        // 가장 긴 매칭되는 국가 코드 찾기
        foreach (var code in countryCodes.OrderByDescending(c => c.Length))
        {
            if (digits.StartsWith(code))
            {
                return ('+' + code, digits[code.Length..]);
            }
        }

        // 매칭되는 국가 코드가 없으면 첫 번째 자리를 국가 코드로 간주
        return ('+' + digits[0], digits[1..]);
    }

    /// <summary>
    /// 전화번호 마스킹 (개인정보 보호)
    /// 예: +82-10-1234-5678 → +82-10-****-5678
    /// </summary>
    public string ToMaskedString()
    {
        if (NationalNumber.Length <= 4)
            return $"{CountryCode}-****";

        var lastFour = NationalNumber[^4..];
        var prefix = NationalNumber[..Math.Min(3, NationalNumber.Length - 4)];
        var masked = new string('*', Math.Max(NationalNumber.Length - prefix.Length - 4, 1));

        return $"{CountryCode}-{prefix}-{masked}-{lastFour}";
    }

    /// <summary>
    /// 국제 형식으로 포맷팅
    /// 예: +82-10-1234-5678
    /// </summary>
    public string ToInternationalFormat()
    {
        return CountryCode == "+82" ? 
            $"{CountryCode}-{NationalNumber[0..2]}-{NationalNumber[2..6]}-{NationalNumber[6..]}" :
            $"{CountryCode}-{NationalNumber}";
    }

    /// <summary>
    /// 국내 형식으로 포맷팅 (한국만 지원)
    /// 예: 010-1234-5678
    /// </summary>
    public string ToNationalFormat()
    {
        if (CountryCode != "+82")
            return ToInternationalFormat();

        return $"0{NationalNumber[0..2]}-{NationalNumber[2..6]}-{NationalNumber[6..]}";
    }

    /// <summary>
    /// 검증된 전화번호로 변경
    /// </summary>
    public PhoneNumber MarkAsVerified() => this with { IsVerified = true };

    /// <summary>
    /// 미검증 전화번호로 변경
    /// </summary>
    public PhoneNumber MarkAsUnverified() => this with { IsVerified = false };

    /// <summary>
    /// 암시적 변환: PhoneNumber → string
    /// </summary>
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;

    /// <summary>
    /// 명시적 변환: string → PhoneNumber
    /// </summary>
    public static explicit operator PhoneNumber(string value) => new(value);

    public override string ToString() => ToInternationalFormat();
}

/// <summary>
/// 전화번호 유형
/// </summary>
public enum PhoneNumberType
{
    /// <summary>
    /// 알 수 없음
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// 휴대전화
    /// </summary>
    Mobile = 1,

    /// <summary>
    /// 유선전화
    /// </summary>
    Landline = 2,

    /// <summary>
    /// VoIP
    /// </summary>
    Voip = 3,

    /// <summary>
    /// 무료 번호
    /// </summary>
    TollFree = 4,

    /// <summary>
    /// 프리미엄 번호
    /// </summary>
    Premium = 5
}