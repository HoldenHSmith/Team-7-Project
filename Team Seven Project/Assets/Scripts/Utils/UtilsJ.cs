using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsJ
{
    /// <summary>
    /// Find's a child gameobject by it's name
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool FindChildByName(string name, GameObject parent, out GameObject child)
    {
        child = null;

        foreach (Transform go in parent.transform)
        {
            if (go.name == name)
            {
                child = go.gameObject;
                return true;
            }
        }

        return false;
    }
}
