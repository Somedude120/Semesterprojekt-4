public static class Constants
{
    //Delimiters
    public const char EndDelimiter = (char)29;
    //public const char MiddleDelimiter = (char)30;
    public const char GroupDelimiter = (char)59;
    //public const char DataDelimiter = (char) 31;
    public const char DataDelimiter = (char)58;

    public static string Write = "W"; // W + GroupDelimiter + Receiver + GroupDelimiter + Message
    public static string MessageReceived = "R"; // R + GroupDelimiter + Sender + GroupDelimiter + Receiver + GroupDelimiter + Message
    public static string LoginResponse = "LR"; // LR + GroupDelimiter + Response
    public static string RequestLogin = "L"; // L + GroupDelimiter + Username + GroupDelimiter + Password
    public static string Logout = "Q"; // Q
    public static string RequestCreateUser = "C"; // C + GroupDelimiter + Username + GroupDelimiter + Password
    public static string Signup = "S"; // S + GroupDelimiter + Username + GroupDelimiter + Password
    public static string SignupResponse = "SR"; // SR + GroupDelimiter + Username + GroupDelimiter + Password
    public static string SendFriendRequest = "SFR"; // SFR + MiddelDelimiter + Username
    public static string RemoveFriend = "RF"; // RF + MiddeDelimiter + Username
    public static string AcceptFriendRequest = "AFR"; // AFR + GroupDelimiter + Username
    public static string DeclineFriendRequest = "DFR"; // DFR + GroupDelimiter + Username
    public static string BlockFriendRequest = "BFR"; // BFR + GroupDelimiter + Username
    public static string RemoveNotification = "RN"; // RN + GroupDelimiter + Notification
    public static string RemoveFriendReceived = "RFR"; // RF + GroupDelimiter + Username
    public static string FriendRequestReceived = "FRR"; // FR + GroupDelimiter + Username
    public static string FriendRequestDeclined = "FRD"; // FRD + GroupDelimiter + Username
    public static string NotificationReceived = "NR"; // NR + GroupDelimiter + Notification
    public static string RemoveGroup = "RG"; // RG + GroupDelimiter + GroupName
    public static string SendGroupRequest = "SGR"; // SGR + GroupDelimiter + GroupName
}
