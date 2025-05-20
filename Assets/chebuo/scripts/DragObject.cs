using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool finished = false;
    [SerializeField] GameObject Snap;
    PolygonCollider2D pc;
    SpriteRenderer sr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        pc=this.AddComponent<PolygonCollider2D>();
        sr = this.GetComponent<SpriteRenderer>();
        rb=this.AddComponent<Rigidbody2D>();
        pc.isTrigger = false;
        rb.gravityScale = 0;
        sr.sortingOrder = 1;
    }
    private void OnMouseDrag()
    {
        if (finished) return;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("ugoiteru");
    }
    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            this.transform.position = Snap.transform.position;
            this.transform.rotation = Snap.transform.rotation;
        }
    }
}
