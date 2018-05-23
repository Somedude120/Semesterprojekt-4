public static class Constants
{
    //Delimiters
    public const char EndDelimiter = (char)29;  //Ends all messages
    //public const char MiddleDelimiter = (char)30;
    public const char GroupDelimiter = (char)59;    //Splits a message into its different groups of data
    //public const char DataDelimiter = (char) 31;
    public const char DataDelimiter = (char)58; //Splits a data group into its different data parts

    /*
     * MESSAGES
     */
    public const string MessageAcknowledged = "OK"; //Will be sent by server when some specific message types are acknowledged
    public const string MessageNotAcknowledged = "NOK"; //Will be sent by server when some specific message types are not acknowledged
    public const string Write = "W"; // W + GroupDelimiter + Receiver + GroupDelimiter + Message
    public const string MessageReceived = "R"; // R + GroupDelimiter + Sender + GroupDelimiter + Receiver + GroupDelimiter + Message

    /*
     * LOGIN (SIGN IN)/LOGOUT
     */
    public const string LoginResponse = "LR"; // LR + GroupDelimiter + Response
    public const string RequestLogin = "L"; // L + GroupDelimiter + Username + GroupDelimiter + Password
    public const string Logout = "Q"; // Q

    /*
     * SIGN UP
     */
    public const string Signup = "S"; // S + GroupDelimiter + Username + GroupDelimiter + Password
    public const string SignupResponse = "SR"; // SR + GroupDelimiter + Username + GroupDelimiter + Password

    /*
     * FRIEND REQUESTS
     */
    public const string SendFriendRequest = "SFR"; // SFR + MiddelDelimiter + Username
    public const string RemoveFriend = "RF"; // RF + MiddeDelimiter + Username
    public const string AcceptFriendRequest = "AFR"; // AFR + GroupDelimiter + Username
    public const string DeclineFriendRequest = "DFR"; // DFR + GroupDelimiter + Username
    public const string BlockFriendRequest = "BFR"; // BFR + GroupDelimiter + Username
    public const string FriendRequestReceived = "FRR"; // FR + GroupDelimiter + Username
    public const string FriendRequestDeclined = "FRD"; // FRD + GroupDelimiter + Username

    /*
     * NOTIFICATIONS
     */
    public const string RemoveNotification = "RN"; // RN + GroupDelimiter + Notification
    public const string NotificationReceived = "NR"; // NR + GroupDelimiter + Notification
    public const string RemoveFriendReceived = "RFR"; // RF + GroupDelimiter + Username

    /*
     * GROUP
     */
    public const string RemoveGroup = "RG"; // RG + GroupDelimiter + GroupName
    public const string SendGroupRequest = "SGR"; // SGR + GroupDelimiter + GroupName

    /*
    * TAGS AND OLD MESSAGES
    */
    public const string SendTag = "ST"; // ST + GroupDelimiter + Tag
    public const string GetUsernamesByTag = "GUBT"; // GUBT + GroupDelimiter + Username + DataDelimiter + Username + DataDelimiter + Username + ...
    public const string GetOldMessages = "GOM"; // GOM;

    /*
    * REQUEST USER DATA
    */
    public const string RequestProfile = "RP"; // RU + GroupDelimiter + Username
    public const string RequestFriendList = "RF"; // RF + GroupDelimiter + Username
    public const string GetProfile = "P"; // P + GroupDelimiter + Username + GroupDelimiter + Descrip. + Group + Tag + DataDel. + Tag ..
    public const string GetFriendList = "FL"; // FL + GroupDelimiter + FriendUsername + DataDelimiter + Friend ...

    /*
     * UPDATE PROFILE
     */
    public const string UpdateProfile = "U"; // U + GroupDelimiter + Username + GroupDelimiter + Descrip. + Group + Tag + DataDel. + Tag ..
}
