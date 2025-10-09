using UnityEngine;

public class StartScreenCursor : MonoBehaviour
{
    [Header("Custom Cursor")]
    public Texture2D cursorTexture;  
    public Vector2 hotspot = Vector2.zero;
    public CursorMode cursorMode = CursorMode.Auto;

    void Start()
    {
       
        if (cursorTexture != null)
        {
            Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
        }

        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
