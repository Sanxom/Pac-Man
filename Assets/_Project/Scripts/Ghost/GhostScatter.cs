using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        Ghost.Chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Node node = collision.GetComponent<Node>();

        if(node != null && enabled && !Ghost.Frightened.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -Ghost.Movement.Direction && node.availableDirections.Count > 1)
            {
                index++;

                if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            Ghost.Movement.SetDirection(node.availableDirections[index]);
        }
    }
}
