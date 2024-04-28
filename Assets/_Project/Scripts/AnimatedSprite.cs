using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float animationTime = 0.25f;
    public bool loop = true;

    // Properties
    public SpriteRenderer SpriteRenderer { get; private set; }
    public int AnimationFrame { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(AdvanceAnimation), animationTime, animationTime);
    }

    /// <summary>
    /// Restarts an Animation back to the beginning.
    /// </summary>
    public void Restart()
    {
        AnimationFrame = -1;

        AdvanceAnimation();
    }

    /// <summary>
    /// Changes the sprite to the next sprite in the Animation.
    /// </summary>
    private void AdvanceAnimation()
    {
        if (!SpriteRenderer.enabled)
            return;

        AnimationFrame++;

        if(AnimationFrame >= sprites.Length && loop)
        {
            AnimationFrame = 0;
        }

        if(AnimationFrame >= 0 && AnimationFrame < sprites.Length)
        {
            SpriteRenderer.sprite = sprites[AnimationFrame];
        }
    }
}
