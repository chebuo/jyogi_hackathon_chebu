using System;
using UnityEngine;

public class ObjectShrinkOnClick : MonoBehaviour
{
    public float shrinkFactor = 0.9f; // �k����
    public float minScale = 0.5f;    // �ŏ��X�P�[��(����ȏ㏬�����Ȃ�Ȃ�)
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


            // ���݂̃X�P�[�����擾
            Vector3 currentScale = transform.localScale;

            // �k����̃X�P�[�����v�Z
            Vector3 newScale = currentScale * shrinkFactor;

            // �ŏ��X�P�[���𒴂��Ȃ��悤�ɐ���
            if (newScale.x >= minScale && newScale.y >= minScale && newScale.z >= minScale)
            {
                transform.localScale = newScale;
            }
            else
            {
                Debug.Log("����ȏ�k���ł��܂���I");
            }

            if (newScale.x <= minScale && newScale.y <= minScale && newScale.z <= minScale)
            {
                Destroy(gameObject);
            }
        }
    }

   
}




