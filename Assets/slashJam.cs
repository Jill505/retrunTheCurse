using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slashJam : MonoBehaviour
{
    public GameCore gCore;

    public bool jamActive = true;
    public float jamCooldown = 2f;

    public SpriteRenderer sr;

    public GameObject jamSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jamActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Jam Active!");

                Instantiate(jamSoundEffect);

                gCore.slashJam();
                jamActive = false;
                Invoke("recov", jamCooldown);

                sr.color = new Color(1,1,1,0.5f);
            }
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Someone Entry my zone Collision");
        if (jamActive)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Jam Active!");

                gCore.slashJam();
                jamActive = false;
                Invoke("recov", jamCooldown);

                sr.color = new Color(0.3337487f, 0.9029127f, 0.9433962f, 0.5f);
            }
        }
    }*/

    void recov()
    {
        jamActive = true;

        sr.color = new Color(0.3337487f, 0.9029127f, 0.9433962f, 1);

    }
}
