using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public bool IsVisibleByDefault = true;
    // Use this for initialization
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = IsVisibleByDefault;
    }

    public static void HideCoursor()
    {
        Cursor.visible = false;
    }

    public static void ShowCoursor()
    {
        Cursor.visible = true;
    }
}
