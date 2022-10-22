using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorHelper
{
    public static Vector2 Collect(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.y);
    }
}
