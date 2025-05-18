using System;
using UnityEngine;

public class BakeColorController : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private Color baseColor;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseColor = spriteRenderer.color; // 初期の芋の色を保存
    }

    public void SetBakeProgress(float progress) {
        float t = Mathf.Clamp01(progress); // 0〜1に制限

        // 元の色を t に応じて暗くする（0→そのまま、1→真っ黒）
        Color newColor = baseColor * (1f - t);
        newColor.a = baseColor.a; // 透明度は変えない
        spriteRenderer.color = newColor;
        Console.WriteLine(baseColor);
    }
}
