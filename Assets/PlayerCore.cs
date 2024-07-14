using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public float health;

    public float slashDamage;

    public bool ivaincible = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
