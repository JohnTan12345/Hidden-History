//-----------------------------------------------------------------------------------------------------------------
// Created By: John Tan
// Description: Handles CRUD for user data
//-----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

public class DatabaseHandler
{
    public static Task<DataSnapshot> GetUserDataAsync(string userID) // Getting user data
    {
        try
        {
            FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false); // Stops firebase from making a cache of the database (keeps making the function get user data before it's edited)
            DatabaseReference userDatadatabase = FirebaseDatabase.DefaultInstance.GetReference("UserData"); // Go straight to user data
            return userDatadatabase.Child(userID).GetValueAsync();
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Database Fetch was cancelled.");
            return null;
        }
        catch (Exception exception) // Catch any error and log the reason for failing
        {
            Debug.Log($"Failed to fetch data\nException: {exception.Message}");
            return null;
        }
    }

    public static Task SaveUserDataAsync(User user) // Setting user data
    {
        UserData userData = user.userData;

        try
        {
             FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false); // Stops firebase from making a cache of the database (keeps making the function get user data before it's edited)
            DatabaseReference userDatadatabase = FirebaseDatabase.DefaultInstance.GetReference("UserData"); // Go straight to user data 
            return userDatadatabase.Child(user.UserID).SetRawJsonValueAsync(JsonUtility.ToJson(userData));  // Save the user data to UID
        }
        catch (TaskCanceledException)
        {
            Debug.Log("Database Saving was cancelled.");
            return null;
        }
        catch (Exception exception) // Catch any error and log the reason for failing
        {
            Debug.Log($"Failed to save data\nException: {exception.Message}");
            return null;
        }
    }
}
