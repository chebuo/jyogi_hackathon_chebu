using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public bool finished = false;
    [SerializeField] GameObject Snap;
    // Start is called before the first frame update
    void Start()
    {
        
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
        }
    }
}
