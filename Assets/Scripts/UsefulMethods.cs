using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedFunctions : MonoBehaviour
{
    public Vector2 GetObjectSize(GameObject obj)
    {
        RectTransform rt = (RectTransform)obj.transform;

        float width = rt.rect.width;
        float height = rt.rect.height;

        Vector2 dimensions = new Vector2(height, width);

        return dimensions;
    }
}
