namespace Topic.Infraestructure.News;

public sealed class NewsSettings
{
    public const string SettingsKey = "News";

    public string Url { get; set; }

    public string ApiKey { get; set; }
}
