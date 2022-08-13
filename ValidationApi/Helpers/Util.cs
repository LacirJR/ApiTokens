namespace ValidationApi.Helpers
{
    public static class Util
    {
        public static string ObterConfiguracao(string value)
        {
            return new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AppSettings")[value];

        }

    }
}
