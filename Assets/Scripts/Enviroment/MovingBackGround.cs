using UnityEngine;
using UnityEngine.UI;

public class BackgroundTremble : MonoBehaviour
{
    [Header("Target Background (UI RawImage)")]
    public RawImage backgroundImage; // Sleep hier je RawImage in

    [Header("Tremble Settings")]
    public float trembleStrength = 0.1f; // Hoeveel hij beweegt
    public float trembleSpeed = 5f;      // Hoe snel hij trilt
    public bool enableTremble = true;    // Zet dit aan/uit in Inspector

    private Vector3 startPos;

    void Start()
    {
        if (backgroundImage != null)
        {
            startPos = backgroundImage.rectTransform.localPosition;
        }
    }

    void Update()
    {
        if (backgroundImage == null) return;

        if (enableTremble)
        {
            float x = Mathf.Sin(Time.time * trembleSpeed) * trembleStrength;
            float y = Mathf.Cos(Time.time * trembleSpeed) * trembleStrength;

            backgroundImage.rectTransform.localPosition = startPos + new Vector3(x, y, 0f);
        }
        else
        {
            backgroundImage.rectTransform.localPosition = startPos;
        }
    }
}
