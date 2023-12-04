#region

using UnityEngine;
using UnityEngine.UI;

#endregion

public class ZoomCamera : MonoBehaviour
{
    [SerializeField]
    Camera cameraZoom;

    [SerializeField]
    float maxFieldOfView = 67f; //porcentaje aximo de zoomm de camara.

    [SerializeField]
    Scrollbar scrollbar;

    [SerializeField]
    private float scrollBarDefaultValue = 0.5f;

    private void Awake()
    {
        if (scrollbar != null)
        {
            scrollbar.value = scrollBarDefaultValue;
        }
        else
        {
            Debug.LogError("Scrollbar is null");
        }
    }

    private void Start()
    {
        cameraZoom.fieldOfView = scrollbar.value * maxFieldOfView;
    }

    private void Update()
    {
        ChangeZoom();
    }

    public void ChangeZoom()
    {
        cameraZoom.fieldOfView = scrollbar.value * maxFieldOfView;
    }
}
