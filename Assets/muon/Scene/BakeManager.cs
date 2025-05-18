using UnityEngine;

public class BakeManager : MonoBehaviour {
    public Transform fireTransform;
    public float maxHeatDistance = 2.0f;
    public float heatSpeed = 0.5f;

    private float bakeProgress = 0f;
    private BakeColorController colorController;

    void Start() {
        colorController = GetComponent<BakeColorController>();
    }

    void Update() {
        float distance = Vector2.Distance(transform.position, fireTransform.position);
        float heatFactor = Mathf.Clamp01(1f - (distance / maxHeatDistance));
        bakeProgress += heatFactor * heatSpeed * Time.deltaTime;

        colorController.SetBakeProgress(bakeProgress);
    }
}
