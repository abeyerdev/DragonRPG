using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour
{
    [SerializeField]
    Texture2D walkCursor = null;
    [SerializeField]
    Texture2D targetCursor = null;
    [SerializeField]
    Texture2D unknownCursor = null;

    [SerializeField]
    Vector2 cursorHotspot = new Vector2(0, 0);

    //[SerializeField]
    //Texture2D walkCursor = null;

    //[SerializeField]
    //Texture2D walkCursor = null;

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
    }
    
    void LateUpdate()
    {
        if (cameraRaycaster.NewLayerHit())
        {
            switch (cameraRaycaster.LayerHit)
            {
                case Layer.Enemy:
                    Cursor.SetCursor(targetCursor, cursorHotspot, CursorMode.Auto);
                    break;
                case Layer.Walkable:
                    Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(unknownCursor, cursorHotspot, CursorMode.Auto);
                    break;
            }
        }        
    }
}
