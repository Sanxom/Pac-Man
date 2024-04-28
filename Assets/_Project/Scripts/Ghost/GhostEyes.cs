using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;

    // Properties
    public SpriteRenderer SpriteRenderer { get; private set; }
    public CharacterMovement Movement { get; private set; }

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        Movement = GetComponentInParent<CharacterMovement>();
    }

    private void Update()
    {
        if(Movement.Direction == Vector2.up)
        {
            SpriteRenderer.sprite = up;
        }
        else if(Movement.Direction == Vector2.down)
        {
            SpriteRenderer.sprite = down;
        }
        else if(Movement.Direction == Vector2.left)
        {
            SpriteRenderer.sprite = left;
        }
        else if (Movement.Direction == Vector2.right)
        {
            SpriteRenderer.sprite = right;
        }
    }
}
