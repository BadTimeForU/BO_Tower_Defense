using UnityEngine;

public class TowerAnimator : MonoBehaviour
{
    [Header("Animatie Frames")]
    public Sprite[] idleSprites;      
    public Sprite[] attackSprites;    
    public float frameRate = 10f;     

    [Header("Renderer")]
    public SpriteRenderer spriteRenderer;

    private float timer;
    private int frameIndex;
    private Sprite[] currentAnimation;

    void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        PlayIdle();
    }

    void Update()
    {
        if (currentAnimation == null || currentAnimation.Length == 0)
            return;

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer = 0f;
            frameIndex = (frameIndex + 1) % currentAnimation.Length;
            spriteRenderer.sprite = currentAnimation[frameIndex];
        }
    }

    public void PlayIdle()
    {
        currentAnimation = idleSprites;
        frameIndex = 0;
    }

    public void PlayAttack()
    {
        currentAnimation = attackSprites;
        frameIndex = 0;
    }
}
