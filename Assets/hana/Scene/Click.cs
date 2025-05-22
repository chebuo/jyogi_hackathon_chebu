using System;
using UnityEngine;

public class ObjectShrinkOnClick : MonoBehaviour
{
    public float shrinkFactor = 0.9f; // 縮小率
    public float minScale = 0.1f;    // 最小スケール



    void OnMouseDown()
    {
        // 現在のスケールを取得
        Vector3 currentScale = transform.localScale;

        // 縮小後のスケールを計算
        Vector3 newScale = currentScale * shrinkFactor;

        // 最小スケールを超えないように制限
        bool isAboveMinScale = Math.Min(newScale.x, Math.Min(newScale.y, newScale.z)) >= minScale;
        {
            transform.localScale = newScale;
        }



       

        }

}
