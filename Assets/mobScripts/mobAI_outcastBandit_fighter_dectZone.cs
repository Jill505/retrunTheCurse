using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_outcastBandit_fighter_dectZone : MonoBehaviour
{
    mobAI_outcastBandit_fighter outcastBandit;
    // Start is called before the first frame update
    void Start()
    {
        outcastBandit = gameObject.transform.parent.gameObject.GetComponent<mobAI_outcastBandit_fighter>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (outcastBandit.mobCore.aiFunctioning)
        {
            if (collision.gameObject.tag == "Player")
            {
                //triggerAttack
                outcastBandit.AttackPlayer();
            }
        }
    }
}
