namespace ApplicationConfigurationTracker
{
    public static class ApplicationConfig
    {
        public static string ApplicationName { get; set; }
        public static string Enviornment { get; set; }
        public static int AccessCount { get; set; }
        public static bool IsInitiliz { get; set; }

        static ApplicationConfig()
        {
            ApplicationName = "MyApp";
            Enviornment = "Development";
            AccessCount = 0;
            IsInitiliz = false;

            Console.WriteLine("Static construtor executed");
        }

        public static void Initialize(string appName, String enviornment)
        {
            ApplicationName = appName;
            Enviornment = enviornment;
            IsInitiliz = true;
            AccessCount++;
        }

        public static string GetConfigurationSummary()
        {
            AccessCount++;
            return $"appName : {ApplicationName}, enviornment : {Enviornment}, accessCount : {AccessCount}, Initialization_Status : {IsInitiliz}";
        }

        public static void RestConfiguration()
        {
            // set all values back to default
            ApplicationName = "MyApp";
            Enviornment = "Development";
            AccessCount = 0;
            IsInitiliz = false;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"appName (before Init) --> " + ApplicationConfig.ApplicationName);

            string AppNaming = "ConfigTracker";
            string EnvNaming = "Production";
            ApplicationConfig.Initialize(AppNaming, EnvNaming);
            string getConfig = ApplicationConfig.GetConfigurationSummary();
            Console.WriteLine("\nConfiguration Summary ");
            Console.WriteLine("GetConfig : " + getConfig);
            ApplicationConfig.RestConfiguration();

            Console.WriteLine("\nAfter Reset ");
            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());
        }
    }
}
