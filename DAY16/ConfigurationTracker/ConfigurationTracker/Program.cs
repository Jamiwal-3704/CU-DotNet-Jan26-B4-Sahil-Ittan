namespace ConfigurationTracker
{
    internal class Program
    {
        static class ApplicationConfig
        {
            // static members
            public static string ApplicationName { get; private set; }
            public static string Environment { get; private set; }
            public static int AccessCount { get; private set; }
            public static bool IsInitialized { get; private set; }

            // static constructor
            static ApplicationConfig()
            {
                ApplicationName = "MyApp";
                Environment = "Development";
                AccessCount = 0;
                IsInitialized = false;

                Console.WriteLine("static constructor executed");
            }

            // static method -> A
            public static void Initialize(string appName, string Enviornment)
            {
                ApplicationName = appName;
                Environment = Enviornment;
                IsInitialized = true;
                AccessCount++;
            }

            // static method -> B
            public static string GetConfigurationSummary()
            {
                AccessCount++;
                return $"Application: {ApplicationName}, Environment: {Environment}, AccessCount: {AccessCount}, IsInitialized: {IsInitialized}";
            }

            // static method -> C
            public static void ResetConfiguration()
            {
                ApplicationName = "MyApp";
                Environment = "Development";
                IsInitialized = false;
                AccessCount++;
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine(ApplicationConfig.ApplicationName);
            ApplicationConfig.Initialize("YourApp", "Production");
            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());
            ApplicationConfig.ResetConfiguration();
            Console.WriteLine(ApplicationConfig.GetConfigurationSummary());
        }
    }
}
