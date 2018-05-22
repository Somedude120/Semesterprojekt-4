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
    public static string AcceptFriendRequest = "AFR"; // AFR + MiddleDelimiter + Username
    public static string DeclineFriendRequest = "DFR"; // DFR + MiddleDelimiter + Username
    public static string BlockFriendRequest = "BFR"; // BFR + MiddleDelimiter + Username
    public static string RemoveNotification = "RN"; // RN + MiddleDelimiter + Notification
    public static string RemoveFriendReceived = "RF"; // RF + MiddleDelimiter + Username
    public static string FriendRequestReceived = "FR"; // FR + MiddleDelimiter + Username
    public static string NotificationReceived = "NR"; // NR + MiddleDelimiter + Notification
}
