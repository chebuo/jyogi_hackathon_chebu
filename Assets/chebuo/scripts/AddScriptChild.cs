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
    int countdown;
    float timer=20;
    int timerint=0;
    [SerializeField] GameObject counttext;
    [SerializeField] GameObject timetext;
    [SerializeField] GameObject matome;
    Text countdowntext;
    Text timertext;
    public Transform[] children;
    SnapObject snap;
    PazzleScore pazzleScore;
    // Start is called before the first frame update
    void Start()
    {
        countdowntext=counttext.GetComponent<Text>();
        timertext=timetext.GetComponent<Text>();
        pazzleScore = matome.GetComponent<PazzleScore>();
    }

    public void OnClick()
    {
        start= true;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(start);
        if (pazzleScore.AllFinished)
        {
            start = false;
            timertext.enabled = false;
        }
        if (start)
        {
            timertext.enabled = true;
            timer-= Time.deltaTime;
            timerint = (int)timer;
            timertext.text = "TIME:"+((int)timerint).ToString();
            Debug.Log(timer);
        }
        fseconds -= Time.deltaTime;
        if (fseconds > 1)
        { 
            seconds = (int)fseconds;
            countdowntext.text = seconds.ToString();
        }
        if(fseconds<=1)
        {
            countdowntext.text = "START";
            start = true;
        }
        if (fseconds<=-0.5) 
        {
            countdowntext.enabled = false;
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
