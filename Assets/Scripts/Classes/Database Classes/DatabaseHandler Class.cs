//-------------
// Created by: John Tan
// Description: Database call wrappers
//------------

using System;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class DatabaseHandler
{

    public static Task<DataSnapshot> GetUserDataAsync(string userID)
    {
        try
        {
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
            DatabaseReference userDatadatabase = FirebaseDatabase.DefaultInstance.GetReference("UserData");
            return userDatadatabase.Child(userID).GetValueAsync();
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Database Fetch was cancelled.");
            return null;
        }
        catch (Exception exception)
        {
            Debug.Log($"Failed to fetch data\nException: {exception.Message}");
            return null;
        }
    }

    public static Task SaveUserDataAsync(User user)
    {
        UserData userData = user.userData;

        try
        {
            DatabaseReference userDatadatabase = FirebaseDatabase.DefaultInstance.GetReference("UserData");
            return userDatadatabase.Child(user.UserID).SetRawJsonValueAsync(JsonUtility.ToJson(userData));
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Database Saving was cancelled.");
            return null;
        }
        catch (Exception exception)
        {
            Debug.Log($"Failed to save data\nException: {exception.Message}");
            return null;
        }
    }
}
