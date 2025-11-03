using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Slider slider;
    private Transform target;
    private Vector3 offset = new Vector3(0, 1.2f, 0);

    public void Setup(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void UpdateHealth(float normalizedValue)
    {
        slider.value = normalizedValue;
    }
}