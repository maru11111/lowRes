using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    private int hp;
    private bool isDefeat=false;
    public TextMeshProUGUI t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�[�o�[����
        if(hp < 0)
        {
            isDefeat = true;
        }

        //�Q�[���I�[�o�[������
        if (isDefeat)
        {
            t.text = "GameOver";
        }
    }

    public void damage(int damage)
    {
        hp -= damage;
    }

}
