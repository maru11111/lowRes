using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class guardEffect : MonoBehaviour
{
    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void guardEffectPlay(Vector2 pos)
    {
        GameObject guard = Instantiate(prefab, pos, Quaternion.identity);

        Destroy(guard, 1f);
    }

}
