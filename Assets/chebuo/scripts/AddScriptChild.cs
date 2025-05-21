using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AddScriptChild : MonoBehaviour
{
    public Transform[] children;
    SnapObject snap;
    // Start is called before the first frame update
    void Start()
    {
        children = new Transform[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children[i]=this.transform.GetChild(i);
            snap = children[i].AddComponent<SnapObject>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
