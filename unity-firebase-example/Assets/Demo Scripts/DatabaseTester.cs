using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserName { get; set; }
    public string CreatedData { get; set; }
    public int RandomValue { get; set; }

    public UserData()
    {
        UserName = "default_name";
        CreatedData = DateTime.UtcNow.ToString();
        RandomValue = 0;
    }
}

public class DatabaseTester : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        while (FirebaseAuth.DefaultInstance == null || FirebaseApp.DefaultInstance == null)
            yield return null;

        var checkTask = FirebaseApp.FixDependenciesAsync();
        yield return new WaitUntil(() => checkTask.IsCompleted);


        FirebaseAuthService.Initialize();
        FirebaseDatabaseService.Initialize();
    }

    public void SetValue()
    {
        if (string.IsNullOrEmpty(FirebaseAuthService.UserId))
            return;

        FirebaseDatabaseService.SetValue("user/" + FirebaseAuthService.UserId, new UserData().ToDictionary(), (task) =>
        {
            if (task.IsCompleted)
            {

            }
            else
            {

            }
        });
    }

    public void Transaction()
    {
        FirebaseDatabaseService.RunTransaction("user/" + FirebaseAuthService.UserId, (mutableData) =>
        {
            if (mutableData.Value == null)
            {
                var data = new UserData();
                data.RandomValue += 1;
                var user = data.ToDictionary();

                mutableData.Value = user;
            }
            else
            {
                var user = mutableData.Value as Dictionary<string, object>;
                user["RandomValue"] = 1 + (long)user["RandomValue"];

                mutableData.Value = user;
            }

            return TransactionResult.Success(mutableData);
        });
    }
}
