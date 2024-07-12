using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashZOne : MonoBehaviour
{
    public GameCore gCore;
    public PlayerCore pCore;
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
        if (collision.gameObject.tag == "mob")
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<MobCore>().hp -= pCore.slashDamage;
            Debug.Log(pCore.slashDamage);
        }
    }
}
