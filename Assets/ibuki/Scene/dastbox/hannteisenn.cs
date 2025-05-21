using UnityEngine;

public class hannteisen : MonoBehaviour
{
    public Vector3 startScale = Vector3.one; // �����T�C�Y
    public Vector3 endScale = new Vector3(0.5f, 0.5f, 0.5f); // �ŏI�T�C�Y
    public float duration = 3f; // �T�C�Y�ύX�ɂ����鎞��

    private float elapsedTime = 0f;
    public float destroyScale = 0.2f;

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


        {
            // �X�P�[�������ɍŏI�T�C�Y�ȉ��Ȃ牽�����Ȃ��i�������^�[���j
            if (transform.localScale.x <= destroyScale)
            {
                Destroy(gameObject);
                return;
            }

        }
    }
}
