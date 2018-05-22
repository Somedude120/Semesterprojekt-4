public static class Constants
{
    public static string EndDelimiter = ((char)29).ToString();
    //public static string MiddleDelimiter = ((char)31).ToString();
    public static char MiddleDelimiter = (char)59;
    public static string Write = "W"; // W + MiddleDelimiter + Receiver + MiddleDelimiter + Message
    public static string MessageReceived = "R"; // R + MiddleDelimiter + Sender + MiddleDelimiter + Receiver + MiddleDelimiter + Message
    public static string LoginResponse = "LR"; // LR + MiddleDelimiter + Response
    public static string RequestLogin = "L"; // L + MiddleDelimiter + Sender
    public static string RequestCreateUser = "C"; // C + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string Login = "L"; // L + MiddleDelimiter + Username + MiddleDelimiter + Password
    public static string Signup = "S"; // S + MiddleDelimiter + Username + MiddleDelimiter + Password
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
}
