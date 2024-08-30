using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchColliderForAttack : MonoBehaviour
{

    public baseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (baseEnemy.target != null)
        {
            //当たった物体がターゲットかどうか
            if (collision.gameObject == baseEnemy.target)
            {
                baseEnemy.withinAttackRange = true;
            }
        }

    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        if (baseEnemy.target != null)
        {
            //当たった物体がターゲットかどうか
            if (collision.gameObject == baseEnemy.target)
            {
                baseEnemy.withinAttackRange = false;
            }
        }
    }
}
