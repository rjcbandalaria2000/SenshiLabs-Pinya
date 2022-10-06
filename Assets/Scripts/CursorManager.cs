using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    public Texture2D        defaultTexture;
    public Texture2D        enterCursorTexture;
    public Texture2D        dragCursorTexture;
    public Texture2D        pressDownCursorTexture;
    private Vector2         cursorHotspot = new Vector2(0,0);

    [Header("Hotspot Positions")]
    public CursorHotspotPos cursorHotspotPos = CursorHotspotPos.TopLeft;
    

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if(enterCursorTexture == null) { return; }
        SetHotSpot();
        Cursor.SetCursor(enterCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnMouseDown()
    {
        if(pressDownCursorTexture == null) { return; }
        Cursor.SetCursor(pressDownCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnMouseDrag()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (dragCursorTexture == null) { return; }
        SetHotSpot();
        Cursor.SetCursor(dragCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if(defaultTexture == null) { return; }
        Cursor.SetCursor(defaultTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        SetHotSpot();
        Cursor.SetCursor(enterCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    private void OnDestroy()
    {
        if (defaultTexture == null) { return; }
        Cursor.SetCursor(defaultTexture, Vector2.zero, CursorMode.ForceSoftware);
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
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pressDownCursorTexture == null) { return; }
            Cursor.SetCursor(pressDownCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(defaultTexture == null) { return; }  
            Cursor.SetCursor(defaultTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

}
