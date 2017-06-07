using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour {
    public Texture2D normalCursor, earthCursor, woodCursor, fireCursor, waterCursor;

    static CursorManager _instance;
    public static CursorManager Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    public void SetCursorNormal()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorEarth()
    {
        Cursor.SetCursor(earthCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorWood()
    {
        Cursor.SetCursor(woodCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorFire()
    {
        Cursor.SetCursor(fireCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorWater()
    {
        Cursor.SetCursor(waterCursor, Vector2.zero, CursorMode.Auto);
    }
}
