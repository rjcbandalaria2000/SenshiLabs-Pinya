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
    public Texture2D enterCursorTexture;
    public Vector2 cursorHotspot = new Vector2(0,0);

    [Header("Hotspot Positions")]
    public CursorHotspotPos cursorHotspotPos = CursorHotspotPos.TopLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseEnter()
    {
        if(enterCursorTexture == null) { return; }
        //cursorHotspot = new Vector2(handCursor.width / 2, handCursor.height / 2);
        SetHotSpot();
        Cursor.SetCursor(enterCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    

    public void OnMouseExit()
    {
        if (enterCursorTexture == null) { return; }
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
