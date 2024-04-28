using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class Pacman : MonoBehaviour
{
    // Properties
    public CharacterMovement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Movement.SetDirection(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Movement.SetDirection(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Movement.SetDirection(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Movement.SetDirection(Vector2.right);
        }

        float angle = Mathf.Atan2(Movement.Direction.y, Movement.Direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void ResetState()
    {
        gameObject.SetActive(true);
        Movement.ResetState();
    }
}