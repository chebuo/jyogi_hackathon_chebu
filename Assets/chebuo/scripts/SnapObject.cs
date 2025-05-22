using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnapObject : MonoBehaviour
{
    public GameObject snap;
    string targetTag;
    [SerializeField] GameObject snapTarget;
    DragObject dragObject; 
    CircleCollider2D cc;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        snap=this.gameObject;
        targetTag = this.gameObject.name;
        snapTarget = GameObject.Find(this.name);
        dragObject=snapTarget.AddComponent<DragObject>();
        cc=this.AddComponent<CircleCollider2D>();
        rb=this.AddComponent<Rigidbody2D>();
        cc.isTrigger=true;
        cc.radius = 0.0001f;
        rb.gravityScale=0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject==snapTarget)
        {
            snapTarget = other.gameObject;
            snapTarget.transform.position=snapTarget.transform.position;
            snapTarget.transform.rotation=snapTarget.transform.rotation;
            dragObject.finished = true;
        }
    }
}
