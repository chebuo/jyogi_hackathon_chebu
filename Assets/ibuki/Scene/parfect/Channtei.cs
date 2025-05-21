using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Channtei : MonoBehaviour
{
    public Vector3 startScale = Vector3.one; // 初期サイズ
    public Vector3 endScale = new Vector3(0.2f, 0.2f, 0.2f); // 最終サイズ
    public float duration = 4f; // サイズ変更にかかる時間

    private float elapsedTime = 0f;
    public float destroyScale = 0.3f;

    void Start()
    {
        transform.localScale = startScale;
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }


        
        if (transform.localScale.x <= destroyScale)
        {
            Destroy(gameObject);
            return;
        }


       


    }
}
