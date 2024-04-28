using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform homeInside;
    public Transform homeOutside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if(gameObject.activeSelf)
            StartCoroutine(ExitTransition());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            Ghost.Movement.SetDirection(-Ghost.Movement.Direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        Ghost.Movement.SetDirection(Vector2.up, true);
        Ghost.Movement.Rigidbody.isKinematic = true;
        Ghost.Movement.enabled = false;

        Vector3 position = transform.position;
        float duration = 0.5f;
        float elapsedTime = 0;

        while(elapsedTime < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, homeInside.position, elapsedTime / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime < duration)
        {
            Vector3 newPosition = Vector3.Lerp(homeInside.position, homeOutside.position, elapsedTime / duration);
            newPosition.z = position.z;
            Ghost.transform.position = newPosition;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Ghost.Movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0), true);
        Ghost.Movement.Rigidbody.isKinematic = false;
        Ghost.Movement.enabled = true;
    }
}
