using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpPad : MonoBehaviour
{
    public GameCore gCore;

    public GameObject jumpSoundEffect;

    public float jumpPadForce = 14f;
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
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(jumpSoundEffect);

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0f,jumpPadForce);

            gCore.stopCluggingForJumpPad();
        }
    }
}
