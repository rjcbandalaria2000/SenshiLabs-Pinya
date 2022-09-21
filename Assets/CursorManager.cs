using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorHotspotPos
{
    TopLeft = 0,
    TopRight = 1,
    TopCenter = 2,
    MiddleLeft = 3,
    Center = 4,
    MiddleRight = 5,
    BottomLeft  =6,
    BottomCenter = 7,
    BottomRight = 8,
}

public class CursorManager : MonoBehaviour
{
    [Header("MouseTextures")]
    public Texture2D        enterCursorTexture;
    public Texture2D        dragCursorTexture;
    private Vector2         cursorHotspot = new Vector2(0,0);

    [Header("Hotspot Positions")]
    public CursorHotspotPos cursorHotspotPos = CursorHotspotPos.TopLeft;
    

    public void OnMouseEnter()
    {
        if(enterCursorTexture == null) { return; }
      
        SetHotSpot();
        Cursor.SetCursor(enterCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void OnMouseDrag()
    {
        if (dragCursorTexture == null) { return; }
        SetHotSpot();
        Cursor.SetCursor(dragCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseUp()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void SetHotSpot()
    {
        switch (cursorHotspotPos)
        {
            case CursorHotspotPos.Center:
                cursorHotspot = new Vector2(enterCursorTexture.width / 2, enterCursorTexture.height / 2);
                break;

            case CursorHotspotPos.TopLeft:
                cursorHotspot = Vector2.zero;
                break;

            default:
                cursorHotspot = Vector2.zero;
                break;
        }
        Debug.Log("Cursor Hotspot" + cursorHotspot);
    }

}
