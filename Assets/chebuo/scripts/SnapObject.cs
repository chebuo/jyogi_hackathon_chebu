using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SnapObject : MonoBehaviour
{
    string targetTag;
    [SerializeField] GameObject snapTarget;
    DragObject dragObject;
    // Start is called before the first frame update
    void Start()
    {
        targetTag = this.gameObject.name;
        dragObject=snapTarget.GetComponent<DragObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag(targetTag))
        {
            snapTarget = other.gameObject;
            snapTarget.transform.position=snapTarget.transform.position;
            snapTarget.transform.rotation=snapTarget.transform.rotation;
            Debug.Log("kasanatta");
            dragObject.finished = true;
        }
    }
}
