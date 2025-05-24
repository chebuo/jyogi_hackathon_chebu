using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AddScriptChild : MonoBehaviour
{
    int i = 0;
    bool start=false;
    float fseconds=4;
    int seconds;
    [SerializeField] GameObject counttext;
    Text text;
    public Transform[] children;
    SnapObject snap;
    // Start is called before the first frame update
    void Start()
    {
        text=counttext.GetComponent<Text>();

    }

    public void OnClick()
    {
        start= true;
    }
    // Update is called once per frame
    void Update()
    {
        fseconds -= Time.deltaTime;
        if (fseconds > 1)
        { 
            seconds = (int)fseconds;
            text.text = seconds.ToString();
            
        }
        if(fseconds<=1)
        {
            text.text = "START";
            start = true;
        }
        if (fseconds<=-0.5) 
        {
            text.enabled = false;
        }
        //Debug.Log(fseconds);
        if (!start||i==1) return;

        children = new Transform[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            children[i] = this.transform.GetChild(i);
            snap = children[i].AddComponent<SnapObject>();
            Debug.Log(children[i]);
        }
        transform.localScale = new Vector3(2, 2, 2);
        Debug.Log("jiko");
        i++;
    }
}
