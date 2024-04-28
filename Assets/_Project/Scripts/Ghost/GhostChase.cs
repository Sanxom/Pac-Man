using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        Ghost.Scatter.Enable();
    }

    /// <summary>
    /// Calculates the shortest route to the Ghost's target.
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if (node != null && enabled && !Ghost.Frightened.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y, 0);
                float distance = (Ghost.target.position - newPosition).sqrMagnitude;

                if(distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            Ghost.Movement.SetDirection(direction);
        }
    }
}
