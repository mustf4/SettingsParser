namespace SettingsParser
{
    public interface IParser
    {
        dynamic Parse(string configuration);
    }
}