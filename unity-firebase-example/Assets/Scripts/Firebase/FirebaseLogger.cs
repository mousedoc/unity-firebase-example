using System.Threading.Tasks;
using UnityEngine;

public enum FirebaseType
{
    Auth,
    Database,
    Analytics,
    RemoteConfig,
}

public static class FirebaseLogger
{ 
    public static void Log(FirebaseType type, string method, Task task)
    {
        if (task.IsCanceled)
            Debug.LogErrorFormat("[{0}] {1} canceled", type, method);

        if (task.IsFaulted)
            Debug.LogErrorFormat("[{0}] {1} fault : " + task.Exception.ToString(), type, method);

        if (task.IsCompleted)
            Debug.LogFormat("[{0}] {1} complete", type, method);
    }
}
