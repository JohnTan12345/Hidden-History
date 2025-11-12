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
            DatabaseReference UserDatadatabase = FirebaseDatabase.DefaultInstance.GetReference("UserData");
            return UserDatadatabase.Child(userID).GetValueAsync();
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
}
