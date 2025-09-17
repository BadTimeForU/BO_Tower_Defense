using UnityEngine;
using UnityEngine.UI;

public class SimpleButtonAnim : MonoBehaviour
{
    public Sprite[] sprites;
    public Image buttonImage;
    public float fps = 10f;

    private int index;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f / fps)
        {
            timer = 0f;
            index = (index + 1) % sprites.Length;
            buttonImage.sprite = sprites[index];
        }
    }
}
