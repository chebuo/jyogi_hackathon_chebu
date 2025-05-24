using UnityEngine;
using System.Collections;
using System;

public class FireController : MonoBehaviour
{
    [Header("■ 火のスケールアニメーション設定 (ランダム範囲)")]
    [Tooltip("火の初期スケール（生成直後の小ささ）")]
    public Vector3 initialScale = new Vector3(0.1f, 0.1f, 1f);

    [Tooltip("最大スケールの最小値（X軸、Y軸共通）")] // 説明をX,Y共通に変更
    public float minMaxScale = 1.0f; // 最小の最大スケール
    [Tooltip("最大スケールの最大値（X軸、Y軸共通）")] // 説明をX,Y共通に変更
    public float maxMaxScale = 4.0f; // 最大の最大スケール
    // minMaxScaleX, maxMaxScaleX, minMaxScaleY, maxMaxScaleY は不要になるので削除

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
        transform.localScale = initialScale;

        // --- 修正箇所：XとYで共通のランダム値を生成し、円形を保つ ---
        float randomUniformScale = UnityEngine.Random.Range(minMaxScale, maxMaxScale);
        currentMaxScale = new Vector3(randomUniformScale, randomUniformScale, 1f);
        // --- 修正ここまで ---

        currentGrowTime = UnityEngine.Random.Range(minGrowTime, maxGrowTime);
        currentSustainTime = UnityEngine.Random.Range(minSustainTime, maxSustainTime);
        currentShrinkTime = currentGrowTime;

        StartCoroutine(FireLifecycleRoutine());
    }

    // ... (FireLifecycleRoutine、DestroyFire、GetCurrentScale メソッドは変更なし) ...

    IEnumerator FireLifecycleRoutine()
    {
        // 1. 成長フェーズ
        float timer = 0f;
        while (timer < currentGrowTime)
        {
            timer += Time.deltaTime;
            float progress = timer / currentGrowTime;
            transform.localScale = Vector3.Lerp(initialScale, currentMaxScale, progress);
            yield return null;
        }
        transform.localScale = currentMaxScale;

        // 2. 維持フェーズ
        yield return new WaitForSeconds(currentSustainTime);

        // 3. 縮小フェーズ
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
        OnFireDestroyed?.Invoke();
        Debug.Log($"{gameObject.name} が消滅しました。");
        Destroy(gameObject);
    }

    public Vector3 GetCurrentScale()
    {
        return transform.localScale;
    }
}