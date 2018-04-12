using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class FirebaseAuthService
{
    public static FirebaseAuth CurrentAuth
    {
        get
        {
            return FirebaseAuth.GetAuth(FirebaseSet.App);
        }
    }

    public static void Initialize()
    {

    }

    #region Sign Up

    public static void SignUpByEmailPassword(string email, string password, Action<Task> callback)
    {
        FirebaseSet.Auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            FirebaseLogger.Log(FirebaseType.Auth, "SignUpByEmailPassword()", task);
            callback(task);
        });
    }

    #endregion

    #region Sign In

    public static void SignInByCredential(Credential credential, Action<Task> callback)
    {
        FirebaseSet.Auth.SignInWithCredentialAsync(credential).ContinueWith(task =>
        {
            FirebaseLogger.Log(FirebaseType.Auth, "SignInByCredential()", task);
            callback(task);
        });
    }

    public static void SignInByEmailPassword(string email, string password, Action<Task> callback)
    {
        var credential = Firebase.Auth.EmailAuthProvider.GetCredential(email, password);

        SignInByCredential(credential, (task) =>
        {
            FirebaseLogger.Log(FirebaseType.Auth, "SignInByEmailPassword()", task);
            callback(task);
        });
    }

    #endregion
}
