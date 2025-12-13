//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: User class
//-----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;
public class User // One user
{
    // User ID Parameter
    private string userID;
    private bool isUserIDSet = false;
    public string UserID { get { return userID; } set { SetUserID(value); } }

    // Other Parameters
    public UserData userData = new UserData();

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
        SetUserID(userID);
        DataSnapshot UserDataSnapshot = null;
        for (int tries = 0; tries < 5; tries++)
        {
            UserDataSnapshot = await DatabaseHandler.GetUserDataAsync(userID);
            if (UserDataSnapshot != null)
            {
                break;
            }
        }

        if (UserDataSnapshot == null) // If database errors out after 5 tries
        {
            Debug.Log("Failed to load data");
        }
        else if (UserDataSnapshot.Exists) // If there is userdata
        {
            Debug.Log("It exists");
            userData = JsonUtility.FromJson<UserData>(UserDataSnapshot.GetRawJsonValue());
            dataLoaded = true;
        }
        else // If there is no userdata
        {
            Debug.Log("No data found");
            dataLoaded = true;
        }
        Debug.Log("New User Created!");
        Users.AddUser(this); // Add this user to the user list
    }

    public async Task SaveUserDataAsync() // Saving data
    {
        Debug.Log("Saving User Data");
        await DatabaseHandler.SaveUserDataAsync(this);
    }

}

public class UserData // Userdata saved in database
{
    public List<string> collectedPieces = new List<string>() {};
    public string currentMission = "Find the hidden artifacts";

}