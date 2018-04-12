using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class ObjectExtension
{
    // Instanc property만 맵에 들어갑니다 (field는 안들어갑니다)
    public static IDictionary<string, object> ToDictionary(this object instasnce)
    {
        return instasnce.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .ToDictionary(prop => prop.Name, prop => prop.GetValue(instasnce, null));
    }
}
