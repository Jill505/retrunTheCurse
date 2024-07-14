using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCore : MonoBehaviour
{
    public GameCore gCore;

    public float hp;
    public bool dead;

    public GameObject deadSound;

    //public string mobType = "Zombie";

    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == false)
        {
            cal();
        }
    }

    void cal()
    {
        if (hp <= 0)
        {
            //dead
            Debug.Log(gameObject.name + "is dead");
            dead = true;

            Instantiate(deadSound);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

            gCore.slashJam();
        }
    }
}
