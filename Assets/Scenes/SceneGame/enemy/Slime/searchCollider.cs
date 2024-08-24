using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchCollider : MonoBehaviour
{
    public slime slime;

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
       slime.setAttackTarget(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        slime.removeAttackTarget(collision.gameObject);
    }
}
