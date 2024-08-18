using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bossF0_0SlashZone : MonoBehaviour
{
    public mobAI_bossF0_0 boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = gameObject.transform.parent.gameObject.GetComponent<mobAI_bossF0_0>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (boss.mCore.aiFunctioning)
        //{

            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
            {
                Debug.Log("Hit Player by boss!");
                boss.slashHurtPlayer();
            }
        //}
    }
}
