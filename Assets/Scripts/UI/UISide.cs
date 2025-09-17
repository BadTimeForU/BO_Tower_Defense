using UnityEngine;

public class RightSideDetector : MonoBehaviour
{
    [SerializeField] private RectTransform uiElement;
    [SerializeField] private float moveLeftAmount = 50f; 
    [SerializeField] private float moveSpeed = 3f;

    private Vector2 originalPos;
    private Vector2 targetPos;

    void Start()
    {
        originalPos = uiElement.anchoredPosition;
        targetPos = originalPos;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        
        if (mousePos.x >= Screen.width * 0.75f)
        {
            targetPos = originalPos + new Vector2(-moveLeftAmount, 0); 
            Debug.Log("Moving left because mouse is on the right!");
        }
        else
        {
            targetPos = originalPos;
        }

        

        uiElement.anchoredPosition = Vector2.Lerp(
            uiElement.anchoredPosition,
            targetPos,
            moveSpeed * Time.deltaTime
        );
    }
}
