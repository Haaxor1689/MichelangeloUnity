namespace Michelangelo.Session {
    public static class URLConstants {
        public static readonly string MainPage = @"https://michelangelo.graphics";
        public static readonly string LogInAPI = $@"{MainPage}/Account/Login";
        public static readonly string LogOutAPI = $@"{MainPage}/Account/LogOff";
        public static readonly string MeAPI = $@"{MainPage}/api/Me";
        public static readonly string GrammarAPI = $@"{MainPage}/api/Grammar";
        public static readonly string SceneAPI = $@"{MainPage}/api/Scene";
        public static readonly string SharedAPI = $@"{MainPage}/api/Shared/0";
        public static readonly string TutorialAPI = $@"{MainPage}/api/Tutorial";
    }
}
