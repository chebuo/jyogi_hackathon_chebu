using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool finished = false;
    [SerializeField] GameObject snap;
    PolygonCollider2D pc;
    SpriteRenderer sr;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        snap = GameObject.Find("chebuhome_matome");
        var matomeTrans = snap.transform;
        var children = new GameObject[matomeTrans.childCount];
        for(int i = 0; i < children.Length; i++)
        {
            children[i] = matomeTrans.GetChild(i).gameObject;
            if (children[i].name == this.name)
            {
                snap= children[i];
            }
        }
        pc=this.AddComponent<PolygonCollider2D>();
        sr = this.GetComponent<SpriteRenderer>();
        rb=this.AddComponent<Rigidbody2D>();
        pc.isTrigger = false;
        rb.gravityScale = 0;
        sr.sortingOrder = 1;
        float setposition = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x+1f,Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x-1f);
        transform.position = new Vector3(setposition, -3, 0);
        transform.localScale=new Vector3(2,2,2);
    }
    private void OnMouseDrag()
    {
        if (finished) return;
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    // Update is called once per frame
    void Update()
    {
        if (finished)
        {
            this.transform.position = snap.transform.position;
            this.transform.rotation = snap.transform.rotation;
        }
    }
}
