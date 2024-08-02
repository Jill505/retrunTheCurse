using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowBullet : MonoBehaviour
{
    Rigidbody2D rb2d;

    public float speed;
    public float damage;


    public PlayerCore pCore;
    GameCore gCore;

    public bool knockCast = false;

    public float knockForceV = 8;//垂直擊退力道
    public float knockForceH = 3;//水平擊退力道
    public float knockTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();

        transform.rotation = Quaternion.Euler(0,0,0);

        Vector2 hitDiraction;
        Vector2 playerPosition = pCore.gameObject.transform.position;
        hitDiraction = playerPosition - (Vector2)gameObject.transform.position;

        float angleInRadians = Mathf.Atan2(hitDiraction.y, hitDiraction.x);
        float hitAngle = angleInRadians * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(0,0,hitAngle);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject.GetComponent<PlayerCore>().ivaincible == false)
        {
            //pCore.injured(damage);
            gCore.deadReason = "弓箭手的箭矢";
            collision.gameObject.GetComponent<PlayerCore>().injured(damage);
            Debug.Log("Triggered");
            StartCoroutine(knockBack());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

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
        pCore.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(knockForceH * dir, knockForceV);
        yield return new WaitForSeconds(knockTime);

        gCore.ableToControl = true;

        Destroy(gameObject);
    }
}
