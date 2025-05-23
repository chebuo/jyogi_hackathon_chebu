using UnityEngine;

public class ImoDrag : MonoBehaviour {
    private Vector2 offset;
    private Rigidbody2D rb;
    private Vector2 minBounds, maxBounds;
    // private bool isGameOver = false; // GameManagerが管理するので不要に

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        minBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        maxBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
    }

    void OnMouseDown() {
        // GameManagerでenabledがfalseの場合、ドラッグできないようにする
        if (!this.enabled) return; 
        offset = (Vector2)(transform.position - GetMouseWorldPos());
    }

    void OnMouseDrag() {
        // GameManagerでenabledがfalseの場合、ドラッグできないようにする
        if (!this.enabled) return;
        Vector2 targetPosition = (Vector2)GetMouseWorldPos() + offset;
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        rb.MovePosition(targetPosition);
    }

    // SetGameOverはGameManagerが呼ぶので不要、このスクリプト自身のenabledで制御する
    // public void SetGameOver() {
    //     isGameOver = true;
    // }

    private Vector3 GetMouseWorldPos() {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}