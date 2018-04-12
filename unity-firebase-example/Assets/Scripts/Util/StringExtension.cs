using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtension
{
    public static DatabaseReference ToDatabase(this string route)
    {
        var reference = FirebaseDatabaseService.Reference;

        foreach (var child in route.Split('/'))
            reference = reference.Child(child);

        return reference;
    }
}
