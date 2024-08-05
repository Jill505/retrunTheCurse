using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thorn : MonoBehaviour
{
    PlayerCore pCore;
    GameCore gCore;

    public float damage = 1f;
    public float cd = 0.8f;

    public bool knockCast = false;

    public float knockForceV =  8;//垂直擊退力道
    public float knockForceH = 3;//水平擊退力道
    public float knockTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
        {
            gCore.deadReason = "尖刺";
            pCore.injured(damage);
            StartCoroutine(knockBack());
            //Debug.Log(gameObject.transform.parent.name + "攻擊成功！");
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false && knockCast == false)
        {
            pCore.injured(damage);
            StartCoroutine(knockBack());
            Debug.Log(gameObject.transform.parent.name + "攻擊成功！");

            knockCast = true;
            Invoke("castRec",cd);
        }
    }*/

    IEnumerator knockBack()
    {
        float dir = 0;
        gCore.ableToControl = false;

        if (gameObject.transform.position.x - pCore.gameObject.transform.position.x > 0)
        {
            //玩家在右側 向右側擊退
            dir = -1;
        }
        else
        {
            //玩家在左側 向左側擊退
            dir = 1;
        }

        pCore.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        pCore.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(knockForceH*dir, knockForceV);
        yield return new WaitForSeconds(knockTime);

        gCore.ableToControl = true; 
    }

    void castRec()
    {
        knockCast = false;
    }
}
