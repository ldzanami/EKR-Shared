namespace EKR_Shared.Data
{
    public static class AuthCommands
    {
        public static string Register => "register";
        public static string Authorize => "authorize";
        public static string Refresh => "refresh";
        public static string Revoke => "revoke";
        public static string RevokeOthers => "revoke-others";
        public static string RevokeAll => "revoke-all";
        public static string GetActive => "get-active-sessions";
        public static string GetPublicKey => "get-public-key";
    }
}
