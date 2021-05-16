using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulMethods : MonoBehaviour
{
    public static Vector2 GetObjectSize(GameObject obj)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        float width = renderer.bounds.size.x;
        float height = renderer.bounds.size.y;

        Vector2 dimensions = new Vector2(width, height);

        return dimensions;
    }
}
