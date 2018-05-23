public static class Constants
{
    //Delimiters
    public const char EndDelimiter = (char)29;  //Ends all messages
    //public const char MiddleDelimiter = (char)30;
    public const char GroupDelimiter = (char)59;    //Splits a message into its different groups of data
    //public const char DataDelimiter = (char) 31;
    public const char DataDelimiter = (char)58; //Splits a data group into its different data parts

    public const string MessageAcknowledged = "OK"; //Will be sent by server when some specific message types are acknowledged
    public const string MessageNotAcknowledged = "NOK"; //Will be sent by server when some specific message types are not acknowledged

    public const string Write = "W"; // W + GroupDelimiter + Receiver + GroupDelimiter + Message
    public const string MessageReceived = "R"; // R + GroupDelimiter + Sender + GroupDelimiter + Receiver + GroupDelimiter + Message

    public const string LoginResponse = "LR"; // LR + GroupDelimiter + Response
    public const string RequestLogin = "L"; // L + GroupDelimiter + Username + GroupDelimiter + Password
    public const string Logout = "Q"; // Q

    public const string RequestCreateUser = "C"; // C + GroupDelimiter + Username + GroupDelimiter + Password
    public const string Signup = "S"; // S + GroupDelimiter + Username + GroupDelimiter + Password
    public const string SignupResponse = "SR"; // SR + GroupDelimiter + Username + GroupDelimiter + Password

    public const string SendFriendRequest = "SFR"; // SFR + MiddelDelimiter + Username
    public const string RemoveFriend = "RF"; // RF + MiddeDelimiter + Username
    public const string AcceptFriendRequest = "AFR"; // AFR + GroupDelimiter + Username
    public const string DeclineFriendRequest = "DFR"; // DFR + GroupDelimiter + Username
    public const string BlockFriendRequest = "BFR"; // BFR + GroupDelimiter + Username

    public const string RemoveNotification = "RN"; // RN + GroupDelimiter + Notification
    public const string RemoveFriendReceived = "RFR"; // RF + GroupDelimiter + Username

    public const string FriendRequestReceived = "FRR"; // FR + GroupDelimiter + Username
    public const string FriendRequestDeclined = "FRD"; // FRD + GroupDelimiter + Username

    public const string NotificationReceived = "NR"; // NR + GroupDelimiter + Notification
    public const string RemoveGroup = "RG"; // RG + GroupDelimiter + GroupName
    public const string SendGroupRequest = "SGR"; // SGR + GroupDelimiter + GroupName
    public const string SendTag = "ST"; // ST + GroupDelimiter + Tag
    public const string GetUsernamesByTag = "GUBT"; // GUBT + GroupDelimiter + Username + DataDelimiter + Username + DataDelimiter + Username + ...
    public const string GetOldMessages = "GOM"; // GOM;

    public const string RequestUser = "RU"; // RU
    public const string GetProfile = "P"; // P + GroupDelimiter + Username + GroupDelimiter + Tag + DataDel. + Tag ..
    public const string GetFriendList = "FL"; // FL + GroupDelimiter + FriendUsername + DataDelimiter + Friend ...
}
