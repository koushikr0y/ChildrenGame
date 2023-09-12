using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyIfNotUsed", 30f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.gameObject.transform.GetComponent<PlayerController>().ChangeSpeed();
            gameObject.layer = LayerMask.NameToLayer("ignoredObstacles");
            Destroy(this.transform.parent.gameObject);
        }
    }
    private void DestroyIfNotUsed()
    {
        bool isSomeThingVisible = false;
        foreach (SpriteRenderer sp in transform.GetComponents<SpriteRenderer>())
        {
            if (sp.isVisible)
            {
                isSomeThingVisible = true;
                break;
            }
        }
        if (!isSomeThingVisible)
        {
            Destroy(this.transform.parent.gameObject, 0.001f);
        }

    }
}
