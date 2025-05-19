using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapObject : MonoBehaviour
{
    [SerializeField] string tagetTag;
    [SerializeField] GameObject snapTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag(tag))
        {
            snapTarget = other.gameObject;
            transform.position = snapTarget.transform.position;
            transform.rotation = snapTarget.transform.rotation;
        }
    }
}
