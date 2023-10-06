using UnityEngine;

public class ObstaclesDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            collision.gameObject.layer = LayerMask.NameToLayer("obstacle");
            Destroy(this.transform.parent.gameObject);
        }
    }
}
