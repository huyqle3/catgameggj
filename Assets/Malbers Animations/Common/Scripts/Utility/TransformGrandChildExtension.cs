using UnityEngine;
using System.Collections;

public static class TransformGrandChildExtension
{
    //Breadth-first search
    public static Transform FindGrandChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindGrandChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
}