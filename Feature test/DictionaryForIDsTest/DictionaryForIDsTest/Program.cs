using System;
using System.Collections.Generic;
using System.Linq;

public class Example
{
    public static void Main()
    {
        // Create a new dictionary of strings, with string keys.
        //
        Dictionary<string, string> userID =
            new Dictionary<string, string>();
        userID.Add("192.168.165.1,8675", "Marto");
        userID.Add("192.168.165.1,8676", "Fatimau");
        userID.Add("192.168.165.1,8677", "Bechy");

        Console.WriteLine(userID["192.168.165.1,8675"]);

        userID.Remove("192.168.165.1,8675");

        if (!userID.ContainsKey("192.168.165.1,8675"))
        {
            Console.WriteLine("Key \"192.168.165.1,8675\" is not found.");
        }
        else
        {
            Console.WriteLine(userID["192.168.165.1,8675"]);

        }

        //Get key by value
        var myKey = userID.FirstOrDefault(x => x.Value == "Fatimau").Key;
        Console.WriteLine("Key: " + myKey);
    }
}