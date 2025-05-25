using UnityEngine;

public class Timesize: MonoBehaviour
{
    public Vector3 startScale = Vector3.one; // 初期サイズ
    public Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f); // 最終サイズ
    public float duration = 2f; // サイズ変更にかかる時間

    private float elapsedTime = 0f;

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
    }
}