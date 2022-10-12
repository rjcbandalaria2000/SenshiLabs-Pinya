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
    [Header("CursorSettings")]
    public Texture2D        defaultCursorTexture;
    public CursorHotspotPos defaultHotspotPos = CursorHotspotPos.TopLeft;
    public Texture2D        enterCursorTexture;
    public CursorHotspotPos enterCursorHotspotPos = CursorHotspotPos.TopLeft;
    public Texture2D        dragCursorTexture;
    public CursorHotspotPos dragCursorHotspotPos = CursorHotspotPos.TopLeft;
    public Texture2D        pressDownCursorTexture;
    public CursorHotspotPos pressDownHotSpotPos = CursorHotspotPos.TopLeft;
    private Vector2         cursorHotspot = new Vector2(0,0); // sets the cursors hotspots 

    public void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if(enterCursorTexture == null) { return; }
        SetHotSpot(enterCursorHotspotPos, enterCursorTexture);
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
        SetHotSpot(dragCursorHotspotPos, dragCursorTexture);
        Cursor.SetCursor(dragCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnMouseExit()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if(defaultCursorTexture == null) { return; }
        SetHotSpot(defaultHotspotPos, defaultCursorTexture);
        Cursor.SetCursor(defaultCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    public void OnMouseUp()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        SetHotSpot(enterCursorHotspotPos, enterCursorTexture);
        Cursor.SetCursor(enterCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
    }

    private void OnDestroy()
    {
        if (defaultCursorTexture == null) { return; }
        SetHotSpot(defaultHotspotPos, defaultCursorTexture);
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void SetHotSpot(CursorHotspotPos hotSpotPos, Texture2D cursorTexture)
    {
        switch (enterCursorHotspotPos)
        {
            case CursorHotspotPos.Center:
                cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
                break;

            case CursorHotspotPos.TopLeft:
                cursorHotspot = Vector2.zero;
                break;

            case CursorHotspotPos.TopCenter:
                cursorHotspot = new Vector2(cursorTexture.width / 2, 0);
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
            SetHotSpot(pressDownHotSpotPos, pressDownCursorTexture);
            Cursor.SetCursor(pressDownCursorTexture, cursorHotspot, CursorMode.ForceSoftware);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if(defaultCursorTexture == null) { return; }
            SetHotSpot(defaultHotspotPos, defaultCursorTexture);
            Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

}
