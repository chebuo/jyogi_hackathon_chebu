using System;
using UnityEngine;

public class ObjectShrinkOnClick : MonoBehaviour
{
    public float shrinkFactor = 0.9f; // 縮小率
    public float minScale = 0.5f;    // 最小スケール(これ以上小さくならない)
    public int clickLimit =50;
    [SerializeField] int Countdown;
    private void Start()
    {
        //Countdown = GetComponent<CountdownTimer>();

    }

    void OnMouseDown()
    {
        if (Countdown >= 0)
        {


            // 現在のスケールを取得
            Vector3 currentScale = transform.localScale;

            // 縮小後のスケールを計算
            Vector3 newScale = currentScale * shrinkFactor;

            // 最小スケールを超えないように制限
            if (newScale.x >= minScale && newScale.y >= minScale && newScale.z >= minScale)
            {
                transform.localScale = newScale;
            }
            else
            {
                Debug.Log("これ以上縮小できません！");
            }

            if (newScale.x <= minScale && newScale.y <= minScale && newScale.z <= minScale)
            {
                Destroy(gameObject);
            }
        }
    }

   
}




