using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bumbArea : MonoBehaviour
{
    public bool damageToPlayer = true;
    public bumbBarrel barrel;
    // Start is called before the first frame update
    void Start()
    {
        barrel = gameObject.transform.parent.gameObject.GetComponent<bumbBarrel>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (boss.mCore.aiFunctioning)
        //{

        if (damageToPlayer == true)
        { 
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
            {
                Debug.Log("Hit Player by boss!");
                barrel.hurtPlayer();
            }
        }
        else
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("boss"))
            {
                Debug.Log("Hit boss!");
                collision.gameObject.GetComponent<MobCore>().hp -= 2f;
            }
        }

        //}
    }
}
