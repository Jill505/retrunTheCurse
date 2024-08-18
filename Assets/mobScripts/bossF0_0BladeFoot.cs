using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossF0_0BladeFoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            gameObject.transform.parent.gameObject.GetComponent<mobAI_bossF0_0>().skill_bladeSwordHitGround();
            this.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            gameObject.transform.parent.gameObject.GetComponent<mobAI_bossF0_0>().skill_bladeSwordHitGround();
            this.enabled = false;
        }

    }
}
