namespace HeadHunter.Shared.Auth;

public class UserInfo
{
    public string Country { get; set; }
    public string Sub { get; set; }
    public bool EmailVerified { get; set; }
    public object PlayerPlocale { get; set; }
    public long CountryAt { get; set; }
    public Pw Pw { get; set; }
    public bool PhoneNumberVerified { get; set; }
    public bool AccountVerified { get; set; }
    public object Ppid { get; set; }
    public string PlayerLocale { get; set; }
    public Acct Acct { get; set; }
    public int Age { get; set; }
    public string Jti { get; set; }
    public Affinity Affinity { get; set; }
}

public class Pw
{
    public long CngAt { get; set; }
    public bool Reset { get; set; }
    public bool MustReset { get; set; }
}

public class Acct
{
    public int Type { get; set; }
    public string State { get; set; }
    public bool Adm { get; set; }
    public object GameName { get; set; }
    public object TagLine { get; set; }
    public long CreatedAt { get; set; }
}

public class Affinity
{
    public string Pp { get; set; }
}
