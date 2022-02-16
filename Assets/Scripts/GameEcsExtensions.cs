using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameEcsExtensions
{
    public static T Instantiate<T>(this T item, Transform parent = null, Vector3 pos = default, Quaternion rot = default) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate(item, pos, rot, parent);
    }
}
