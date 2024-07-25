using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_zombie_damageZone : MonoBehaviour
{

    mobAI_zombie aiZombie;
    // Start is called before the first frame update
    void Start()
    {
        aiZombie = gameObject.transform.parent.gameObject.GetComponent<mobAI_zombie>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (aiZombie.mobCore.aiFunctioning)
        {
            if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
            {
                Debug.Log("zombie À»¤¤ª±®a¡I");
                aiZombie.hurtPlayer();
            }
        }
    }
}
