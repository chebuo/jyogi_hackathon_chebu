using UnityEngine;

public class ImoDrag : MonoBehaviour {
    private Vector2 offset;
    private Rigidbody2D rb;
    private Vector2 minBounds, maxBounds;
    private bool isGameOver = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        minBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void OnMouseDown() {
        if (isGameOver) return;
        offset = (Vector2)(transform.position - GetMouseWorldPos());
    }

    void OnMouseDrag() {
        if (isGameOver) return;
        Vector2 targetPosition = (Vector2)GetMouseWorldPos() + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        rb.MovePosition(targetPosition);
    }

    public void SetGameOver() {
        isGameOver = true;
    }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}