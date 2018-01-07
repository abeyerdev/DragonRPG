using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField]
    float distanceToBackground = 100f;

    Camera viewCamera;

    RaycastHit rayCastHit;
    public RaycastHit Hit { get { return rayCastHit; } }

    public delegate void LayerChanged(Layer newLayer);
    public event LayerChanged OnLayerChange;

    Layer prevLayerHit;
    Layer layerHit;
    public Layer LayerHit { get { return layerHit; } }

    void Start()
    {
        viewCamera = Camera.main;
    }

    void Update()
    {
        // Look for and return priority layer hit
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            if (hit.HasValue)
            {
                rayCastHit = hit.Value;
                UpdateLayerHit(layer);
                return;
            }
        }

        // Otherwise return background hit
        rayCastHit.distance = distanceToBackground;
        UpdateLayerHit(Layer.RaycastEndStop);
    }

    void UpdateLayerHit(Layer layer)
    {
        prevLayerHit = layerHit;
        layerHit = layer;
        if(prevLayerHit != layerHit)
        {
            OnLayerChange(layerHit);
        }
    }

    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; // See Unity docs for mask formation
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit, distanceToBackground, layerMask);

        if (hasHit)
        {
            return hit;
        }

        return null;
    }
}
