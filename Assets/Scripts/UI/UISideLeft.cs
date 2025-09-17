using UnityEngine;

public class LeftSideDetector : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement1;
    [SerializeField] private float moveRightAmount1 = 50f;
    [SerializeField] private float moveSpeed1 = 3f;

    private Vector2 originalPos1;
    private Vector2 targetPos1;

    void Start()
    {
        originalPos1 = uiElement1.anchoredPosition;
        targetPos1 = originalPos1;
    }

    void Update()
    {
        Vector3 mousePos1 = Input.mousePosition;


        if (mousePos1.x <= Screen.width * 0.25f)
        {
            targetPos1 = originalPos1 + new Vector2(moveRightAmount1, 0);
            Debug.Log("Moving left because mouse is on the right!");
        }
        else
        {
            targetPos1 = originalPos1;
        }



        uiElement1.anchoredPosition = Vector2.Lerp(
            uiElement1.anchoredPosition,
            targetPos1,
            moveSpeed1 * Time.deltaTime
        );
    }
}
