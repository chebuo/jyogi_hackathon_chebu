using UnityEngine;
using System.Collections;
using System;

public class FireController : MonoBehaviour
{
    [Header("■ 火のスケールアニメーション設定 (ランダム範囲)")]
    [Tooltip("火の初期スケール（生成直後の小ささ）")]
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 1f);

    [Tooltip("最大スケールの最小値（X軸、Y軸共通）")] 
    public float minMaxScale = 1.0f; 
    [Tooltip("最大スケールの最大値（X軸、Y軸共通）")] 
    public float maxMaxScale = 4.0f; 

    [Tooltip("火が最大スケールになるまでの時間の最小値")]
    public float minGrowTime = 1.0f;
    [Tooltip("火が最大スケールになるまでの時間の最大値")]
    public float maxGrowTime = 4.0f;

    [Tooltip("火が最大スケールで維持される時間の最小値")]
    public float minSustainTime = 1.0f;
    [Tooltip("火が最大スケールで維持される時間の最大値")]
    public float maxSustainTime = 2.0f;

    private Vector3 currentMaxScale;
    private float currentGrowTime;
    private float currentSustainTime;
    private float currentShrinkTime;

    [Header("■ 火の消滅条件")]
    [Tooltip("このスケール以下になったら火が消滅します。（Y軸のスケールを基準）")]
    public float destroyThresholdScaleY = 0.15f;
    [Tooltip("火の消滅時に通知を送るイベント")]
    public event Action OnFireDestroyed;

    void Start()
    {
        Debug.Log($"FireController: {gameObject.name} のStartが呼び出されました。初期スケール: {transform.localScale}"); // 追加
        transform.localScale = initialScale;

        float randomUniformScale = UnityEngine.Random.Range(minMaxScale, maxMaxScale);
        currentMaxScale = new Vector3(randomUniformScale, randomUniformScale, 1f);

        currentGrowTime = UnityEngine.Random.Range(minGrowTime, maxGrowTime);
        currentSustainTime = UnityEngine.Random.Range(minSustainTime, maxSustainTime);
        currentShrinkTime = currentGrowTime;

        StartCoroutine(FireLifecycleRoutine()); 
    }

    IEnumerator FireLifecycleRoutine()
    {
        float timer = 0f;
        while (timer < currentGrowTime)
        {
            timer += Time.deltaTime;
            float progress = timer / currentGrowTime;
            transform.localScale = Vector3.Lerp(initialScale, currentMaxScale, progress);
            yield return null;
        }
        transform.localScale = currentMaxScale;

        yield return new WaitForSeconds(currentSustainTime);

        timer = 0f;
        while (timer < currentShrinkTime)
        {
            timer += Time.deltaTime;
            float progress = timer / currentShrinkTime;
            transform.localScale = Vector3.Lerp(currentMaxScale, initialScale, progress);
            
            if (transform.localScale.y <= destroyThresholdScaleY)
            {
                DestroyFire();
                yield break;
            }
            yield return null;
        }
        transform.localScale = initialScale;

        DestroyFire();
    }

    void DestroyFire()
    {
        Debug.Log($"FireController: {gameObject.name} がDestroyFireを呼び出しました。消滅します。"); // 追加
        OnFireDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public Vector3 GetCurrentScale()
    {
        return transform.localScale;
    }
}