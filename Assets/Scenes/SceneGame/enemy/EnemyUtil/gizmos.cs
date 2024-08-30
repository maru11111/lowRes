using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmos : MonoBehaviour
{
    // Update is called once per frame
    void OnDrawGizmos()
    {
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {

            if (collider is BoxCollider2D)
            {
                Gizmos.color = Color.red; // 色を指定

                BoxCollider2D box = (BoxCollider2D)collider;
                Gizmos.DrawWireCube(transform.position + (Vector3)box.offset, box.bounds.size);
            }
            else if (collider is CircleCollider2D)
            {
                Gizmos.color = Color.yellow; // 色を指定

                CircleCollider2D circle = (CircleCollider2D)collider;
                Gizmos.DrawWireSphere(transform.position, circle.radius);
            }
        }
    }
}
