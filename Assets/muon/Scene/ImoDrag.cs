using UnityEngine;

public class ImoDrag : MonoBehaviour {
    private Vector3 offset;

    void OnMouseDown() {
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag() {
        transform.position = GetMouseWorldPos() + offset;
    }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
