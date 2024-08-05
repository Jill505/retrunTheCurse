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

    public float knockForceV =  8;//�������h�O�D
    public float knockForceH = 3;//�������h�O�D
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
            gCore.deadReason = "�y��";
            pCore.injured(damage);
            StartCoroutine(knockBack());
            //Debug.Log(gameObject.transform.parent.name + "�������\�I");
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false && knockCast == false)
        {
            pCore.injured(damage);
            StartCoroutine(knockBack());
            Debug.Log(gameObject.transform.parent.name + "�������\�I");

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
            //���a�b�k�� �V�k�����h
            dir = -1;
        }
        else
        {
            //���a�b���� �V�������h
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
