using System.Collections.Generic;

public static class Users // Class to call for when u need the list of users
{
    private static Dictionary<string, string> usernameTable = new Dictionary<string, string>{}; // Pair usernames to userID
    private static Dictionary<string, User> users = new Dictionary<string, User>{}; // Pairs userID to User
    public static bool DefaultUserLoaded = false; // Check if the default user is loaded

    public static string UsernameToUserID(string username) // Get a UserID with a Username
    {
        return usernameTable[username];
    }
    public static Dictionary<string, User> GetUsers() // Return list of users
    {
        return users;
    }

    public static User GetUser(string userID) // Get a specific user
    {
        return users[userID];
    }

    public static User GetDefaultUser() // Get the default User
    {
        return users[usernameTable["Default"]];
    }

    public static void AddUser(User user, string username = "Default") // Add a user to the user list
    {
        users.Add(user.UserID, user);
        usernameTable.Add(username, user.UserID);

        if (username == "Default") // Check if its the default user
        {
            DefaultUserLoaded = true;
        }
    }
}