using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collider : MonoBehaviour
{
    BoxCollider2D col;
    [SerializeField] Material[] materialArray;
    
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        
        //始めはオフ
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
    }

    public void OnCollider()
    {
        col.enabled = true;
    }

    public void OffCollider()
    {
        col.enabled = false;
    }

    public void changeColor()
    {
        if (col.enabled)
        {
            GetComponent<Renderer>().material = materialArray[0];
        }
        else
        {
            GetComponent<Renderer>().material = materialArray[1]    ;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        // 衝突したオブジェクトの位置を取得
        Vector2 collisionPos = col.ClosestPoint(transform.position);
        
        GetComponentInParent<baseEnemy>().attack(col.gameObject, collisionPos);
    }
}
