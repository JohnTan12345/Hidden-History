using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public static class Users
{
    private static Dictionary<string, string> usernameTable = new Dictionary<string, string>{};
    private static Dictionary<string, User> users = new Dictionary<string, User>{};

    public static string UsernameToUserID(string username)
    {
        return usernameTable[username];
    }
    public static Dictionary<string, User> GetUsers()
    {
        return users;
    }

    public static User GetUser(string userID)
    {
        return users[userID];
    }

    public static void AddUser(User user, string username = "Default")
    {
        users.Add(user.UserID, user);
        usernameTable.Add(username, user.UserID);
    }
}
public class User
{
    // User ID Parameter
    private string userID;
    private bool isUserIDSet = false;
    public string UserID { get { return userID; } set { SetUserID(value); } }

    // Other Parameters
    public UserData userData;

    private bool dataLoaded;
    public bool DataLoaded { get { return dataLoaded; } }

    // Functions
    private void SetUserID(string newUserID)
    {
        if (!isUserIDSet) // Check if User ID has already been set before
        {
            userID = newUserID;
            isUserIDSet = true; // Lock the User ID from further edits
        }
        else
        {
            Debug.Log("You can't update the userID after it's set!");
        }
    }

    public async Task CreateNewUserAsync(string userID) // Try out using async task instead of coroutines since it wont be affecting gameobjects
    {
        UserID = userID;
        DataSnapshot UserDataSnapshot = null;
        for (int tries = 0; tries < 5; tries++)
        {
            UserDataSnapshot = await DatabaseHandler.GetUserDataAsync(userID);
            if (UserDataSnapshot != null)
            {
                break;
            }
        }

        if (UserDataSnapshot == null)
        {
            Debug.Log("Failed to load data");
        }
        else if (UserDataSnapshot.Exists)
        {
            Debug.Log("It exists");
            userData = JsonUtility.FromJson<UserData>(UserDataSnapshot.GetRawJsonValue());
        }
        else
        {
            Debug.Log("It works but no data");
        }

        Debug.Log(UserDataSnapshot.GetRawJsonValue());
        Debug.Log(userData.artifactFixed);
    }

    public async Task SaveUserDataAsync()
    {
        Debug.Log("Saving User Data");
        await DatabaseHandler.SaveUserDataAsync(this);
        
    }

}

public class UserData
{
    public List<string> collectedPieces = new List<string>();
    public bool artifactFixed = false;

}