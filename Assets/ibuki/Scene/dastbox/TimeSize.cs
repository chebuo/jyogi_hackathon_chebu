using UnityEngine;

public class Timesize: MonoBehaviour
{
    public Vector3 startScale = Vector3.one; // �����T�C�Y
    public Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f); // �ŏI�T�C�Y
    public float duration = 2f; // �T�C�Y�ύX�ɂ����鎞��

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