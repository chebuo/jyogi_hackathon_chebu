using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Channtei : MonoBehaviour
{
    public Vector3 startScale = Vector3.one; // �����T�C�Y
    public Vector3 endScale = new Vector3(0.2f, 0.2f, 0.2f); // �ŏI�T�C�Y
    public float duration = 4f; // �T�C�Y�ύX�ɂ����鎞��

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
