using UnityEngine;
using UnityEngine.EventSystems;

public class DragInput : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Camera _camera;

    private void Start () 
    {
        _camera = Camera.main;
	}
	
    public void OnDrag(PointerEventData eventData)
    {
        Ray raycastFromCamera = _camera.ScreenPointToRay(Input.mousePosition);
        Vector3 startPositionObject = transform.position;
        Vector3 cameraPlaneNormal = -_camera.transform.forward;        
        float distancionFromCameraToObject = Vector3.Dot(startPositionObject - raycastFromCamera.origin, cameraPlaneNormal) / Vector3.Dot(raycastFromCamera.direction, cameraPlaneNormal);
        Vector3 newPositionObject = raycastFromCamera.origin + raycastFromCamera.direction * distancionFromCameraToObject; 


        transform.position = new Vector3(newPositionObject.x, transform.position.y, transform.position.z);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Do stuff when dragging begins. For example suspend camera interaction.
        Debug.Log("Begin");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Do stuff when draggin ends. For example restore camera interaction.
    }
}