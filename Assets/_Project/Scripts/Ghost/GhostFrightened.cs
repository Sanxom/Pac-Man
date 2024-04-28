using UnityEngine;

public class GhostFrightened : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer blue;
    public SpriteRenderer white;

    // Properties
    public bool hasBeenEaten { get; private set; }

    private void OnEnable()
    {
        Ghost.Movement.speedMultiplier *= 0.5f;
        hasBeenEaten = false;
    }

    private void OnDisable()
    {
        Ghost.Movement.speedMultiplier = 1;
        hasBeenEaten = false;
    }

    /// <summary>
    /// Calculates the farthest route to the Ghost's target.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
                float distance = (Ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (enabled)
            {
                Eaten();
            }
        }
    }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        body.enabled = false;
        eyes.enabled = false;
        blue.enabled = true;
        white.enabled = false;

        Invoke(nameof(Flash), duration * 0.5f);
    }

    public override void Disable()
    {
        base.Disable();

        body.enabled = true;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }

    /// <summary>
    /// Visual representation for a Ghost coming out of the Frightened state.
    /// </summary>
    private void Flash()
    {
        if (!hasBeenEaten)
        {
            blue.enabled = false;
            white.enabled = true;
            white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        hasBeenEaten = true;

        Vector3 position = Ghost.Home.homeInside.position;
        position.z = Ghost.transform.position.z;
        Ghost.transform.position = position;
        Ghost.Home.Enable(duration);

        body.enabled = false;
        eyes.enabled = true;
        blue.enabled = false;
        white.enabled = false;
    }
}
