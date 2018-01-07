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

    CameraRaycaster cameraRaycaster;

    // Use this for initialization
    void Start()
    {
        cameraRaycaster = GetComponent<CameraRaycaster>();
        BindEvents();
    }

    private void OnDestroy()
    {
        UnBindEvents();
    }

    void LateUpdate()
    {
             
    }

    void BindEvents()
    {
        cameraRaycaster.OnLayerChange += UpdateCursorOnLayerChange;
    }

    void UnBindEvents()
    {
        cameraRaycaster.OnLayerChange -= UpdateCursorOnLayerChange;
    }

    void UpdateCursorOnLayerChange(Layer newLayer)
    {
        switch (newLayer)
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
