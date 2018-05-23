public static class Constants
{
<<<<<<< HEAD
    public static string EndDelimiter = ((char)29).ToString();
    //public static string MiddleDelimiter = ((char)31).ToString();
    public static char MiddleDelimiter = (char)59;
    public static string Write = "W"; // W + MiddleDelimiter + Receiver + MiddleDelimiter + Message
    public static string MessageReceived = "R"; // R + MiddleDelimiter + Sender + MiddleDelimiter + Receiver + MiddleDelimiter + Message
    public static string LoginResponse = "LR"; // LR + MiddleDelimiter + Response
    public static string RequestLogin = "L"; // L + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string Logout = "Q"; // Q
    public static string RequestCreateUser = "C"; // C + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string Signup = "S"; // S + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string SignupResponse = "SR"; // SR + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string SendFriendRequest = "SFR"; // SFR + MiddelDelimiter + Username
    public static string RemoveFriend = "RF"; // RF + MiddeDelimiter + Username
    public static string AcceptFriendRequest = "AFR"; // AFR + MiddleDelimiter + Username
    public static string DeclineFriendRequest = "DFR"; // DFR + MiddleDelimiter + Username
    public static string BlockFriendRequest = "BFR"; // BFR + MiddleDelimiter + Username
    public static string RemoveNotification = "RN"; // RN + MiddleDelimiter + Notification
    public static string RemoveFriendReceived = "RFR"; // RF + MiddleDelimiter + Username
    public static string FriendRequestReceived = "FRR"; // FR + MiddleDelimiter + Username
    public static string FriendRequestDeclined = "FRD"; // FRD + MiddleDelimiter + Username
    public static string NotificationReceived = "NR"; // NR + MiddleDelimiter + Notification
    public static string RemoveGroup = "RG"; // RG + MiddleDelimiter + GroupName
    public static string SendGroupRequest = "SGR"; // SGR + MiddleDelimiter + GroupName
=======
    public const char EndDelimiter = (char)29;
    //public static string MiddleDelimiter = ((char)30).ToString();
    public const char GroupDelimiter = (char)59;
    //public const char DataDelimiter = (char) 31;
    public const char DataDelimiter = (char)58;

    public const string Write = "W"; // W + MiddleDelimiter + Receiver + MiddleDelimiter + Message
    public const string MessageReceived = "R"; // R + MiddleDelimiter + Sender + MiddleDelimiter + Receiver + MiddleDelimiter + Message

    public const string LoginResponse = "LR"; // LR + MiddleDelimiter + Response
    public const string RequestLogin = "L"; // L + MiddleDelimiter + Username + MiddleDelimiter + Password

    public const string Signup = "S"; // S + MiddleDelimiter + Username + MiddleDelimiter + Password
    public const string SignupResponse = "SR"; // SR + MiddleDelimiter + Username + MiddleDelimiter + Password

    public const string SendFriendRequest = "SFR"; // SFR + MiddelDelimiter + Username
    public const string RemoveFriend = "RF"; // RF + MiddeDelimiter + Username
    public const string AcceptFriendRequest = "AFR"; // AFR + MiddleDelimiter + Username
    public const string DeclineFriendRequest = "DFR"; // DFR + MiddleDelimiter + Username
    public const string BlockFriendRequest = "BFR"; // BFR + MiddleDelimiter + Username

    public const string RemoveNotification = "RN"; // RN + MiddleDelimiter + Notification
    public const string RemoveFriendReceived = "RFR"; // RF + MiddleDelimiter + Username

    public const string FriendRequestReceived = "FRR"; // FR + MiddleDelimiter + Username
    public const string FriendRequestDeclined = "FRD"; // FRD + MiddleDelimiter + Username

    public const string NotificationReceived = "NR"; // NR + MiddleDelimiter + Notification
>>>>>>> 6755574c64b5ddbad44cbd2e49635802642b3d7d
}
