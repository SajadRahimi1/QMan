namespace QMan.Application.Dtos.Base;

public sealed class ConfigurationModel
{
    private ConfigurationModel(){}
    private static ConfigurationModel? instance = null;
    public static ConfigurationModel Instance => instance ??= new ConfigurationModel();

    public string JwtToken { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
}