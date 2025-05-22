using System;
using UnityEngine;

public class ObjectShrinkOnClick : MonoBehaviour
{
    public float shrinkFactor = 0.9f; // �k����
    public float minScale = 0.1f;    // �ŏ��X�P�[��



    void OnMouseDown()
    {
        // ���݂̃X�P�[�����擾
        Vector3 currentScale = transform.localScale;

        // �k����̃X�P�[�����v�Z
        Vector3 newScale = currentScale * shrinkFactor;

        // �ŏ��X�P�[���𒴂��Ȃ��悤�ɐ���
        bool isAboveMinScale = Math.Min(newScale.x, Math.Min(newScale.y, newScale.z)) >= minScale;
        {
            transform.localScale = newScale;
        }



       

        }

}
