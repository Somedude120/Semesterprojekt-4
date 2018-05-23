public static class Constants
{
    public static string EndDelimiter = ((char)29).ToString();
    //public static string MiddleDelimiter = ((char)31).ToString();
    public static char MiddleDelimiter = (char)59;

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
}
