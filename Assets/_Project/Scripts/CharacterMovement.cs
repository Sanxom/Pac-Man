using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public Vector2 initialDirection;
    public float speed = 8;
    public float speedMultiplier = 1;

    // Properties
    public Rigidbody2D Rigidbody { get; private set; }
    public Vector2 Direction { get; private set; }
    public Vector2 NextDirection { get; private set; }
    public Vector3 StartingPosition { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        StartingPosition = transform.position;
    }

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if(NextDirection != Vector2.zero)
        {
            SetDirection(NextDirection);
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody.position;
        Vector2 translation = speed * speedMultiplier * Time.fixedDeltaTime * Direction;
        Rigidbody.MovePosition(position + translation);
    }

    public void ResetState()
    {
        speedMultiplier = 1;
        Direction = initialDirection;
        NextDirection = Vector2.zero;
        transform.position = StartingPosition;
        Rigidbody.isKinematic = false;
        enabled = true;
    }

    /// <summary>
    /// Logic for changing direction with Pacman.
    /// </summary>
    /// <param name="direction"></param>
    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !IsOccupied(direction))
        {
            Direction = direction;
            NextDirection = Vector2.zero;
        }
        else
        {
            NextDirection = direction;
        }
    }

    /// <summary>
    /// Logic for being able to detect if you can change direction with Pacman.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    public bool IsOccupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.75f, 0, direction, 1.5f, obstacleLayer);
        return hit.collider != null;
    }
}
