using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform target;
    public GhostBehavior initialBehavior;

    public int points = 200;

    // Properties
    public CharacterMovement Movement { get; private set; }
    public GhostHome Home { get; private set; }
    public GhostScatter Scatter { get; private set; }
    public GhostChase Chase { get; private set; }
    public GhostFrightened Frightened { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<CharacterMovement>();
        Home = GetComponent<GhostHome>();
        Scatter = GetComponent<GhostScatter>();
        Chase = GetComponent<GhostChase>();
        Frightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetState();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (Frightened.enabled)
            {
                GameManager.instance.GhostEaten(this);
            }
            else
            {
                GameManager.instance.PacmanEaten();
            }
        }
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();

        Frightened.Disable();
        Chase.Disable();
        Scatter.Enable();

        if(Home != initialBehavior)
        {
            Home.Disable();
        }

        if(initialBehavior != null)
        {
            initialBehavior.Enable();
        }
    }
}
