using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public GameCore gCore;
    public float health;

    public float slashDamage;

    public bool ivaincible = false;

    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void injured(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //player dead;
        }
        else
        {
            StartCoroutine(injuredIm());
        }
    }

    IEnumerator injuredIm()
    {
        ivaincible = true;
        yield return new WaitForSeconds(gCore.injuredImortalTime);
        //Play Animation
        ivaincible = false;
    }
}
