using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;

    // Properties
    public List<Vector2> availableDirections { get; private set; }

    private void Start()
    {
        availableDirections = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
        CheckAvailableDirection(Vector2.right);
    }

    /// <summary>
    /// Logic for making sure a Ghost has information about which way it can turn.
    /// </summary>
    /// <param name="direction"></param>
    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.5f, 0, direction, 1, obstacleLayer);

        if(hit.collider == null)
        {
            availableDirections.Add(direction);
        }
    }
}
