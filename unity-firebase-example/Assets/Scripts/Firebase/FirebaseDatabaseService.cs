using Firebase;
using Firebase.Analytics;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class FirebaseDatabaseService
{
    private const string EditorDatabaseUrl = "https://unity-firebase-example.firebaseio.com/";

    public static DatabaseReference Reference
    {
        get { return FirebaseSet.Database.RootReference; }
    }

    public static void Initialize()
    {
#if UNITY_EDITOR
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(EditorDatabaseUrl);
#endif
    }

    #region Database usage

    public static void SetValue<T>(string route, T data, Action<Task> callback)
    {
        route.ToDatabase().SetValueAsync(data).ContinueWith((task) =>
        {
            FirebaseLogger.Log(FirebaseType.Database, "SetValue<T>()", task);
            callback(task);
        });
    }

    public static void PushValue<T>9string route, T data, Action<Task> callback)
    {
        route.ToDatabase().Push().SetValueAsync(data).ContinueWith((task) =>
        {
            FirebaseLogger.Log(FirebaseType.Database, "PushValue<T>()", task);
            callback(task);
        });
    }

    public static void UpdateValue<T>(string route, T data, Action<Task> callback)
    {
        route.ToDatabase().UpdateChildrenAsync(data.ToDictionary()).ContinueWith((task) =>
        {
            FirebaseLogger.Log(FirebaseType.Database, "UpdateValue<T>()", task);
            callback(task);
        });
    }

    public static void RunTransaction<T>(string route, Func<MutableData, TransactionResult> transaction)
    {
        route.ToDatabase().RunTransaction(transaction);
    }

    #endregion
}