using Firebase;
using Firebase.Auth;
using Firebase.Database;

public static class FirebaseSet
{
    public static FirebaseApp App { get { return FirebaseApp.DefaultInstance; } }

    public static FirebaseDatabase Database { get { return FirebaseDatabase.DefaultInstance; } }

    public static FirebaseAuth Auth { get { return FirebaseAuth.DefaultInstance; } }
}
