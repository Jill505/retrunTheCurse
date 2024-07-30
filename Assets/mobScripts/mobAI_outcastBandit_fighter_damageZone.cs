using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_outcastBandit_fighter_damageZone : MonoBehaviour
{
    mobAI_outcastBandit_fighter fighter;
    // Start is called before the first frame update
    void Start()
    {
        fighter = gameObject.transform.parent.gameObject.GetComponent<mobAI_outcastBandit_fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (fighter.mobCore.aiFunctioning)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
            {
                fighter.hurtPlayer();
                Debug.Log(gameObject.transform.parent.name+"§ðÀ»¦¨¥\¡I");
            }
        }
    }
}
