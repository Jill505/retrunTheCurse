using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bumbBarrel : MonoBehaviour
{
    PlayerCore pCore;
    GameCore gCore;
    MobCore mCore;

    public float damage = 1;

    public Animator animator;
    public GameObject baua;

    public float knockForceV = 8;//垂直擊退力道
    public float knockForceH = 3;//水平擊退力道
    public float knockTime = 0.3f;

    public bool deadClug;

    // Start is called before the first frame update
    void Start()
    {

        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        mCore = gameObject.GetComponent<MobCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mCore.dead == true)
        {
            //Play Animation
            //被玩家殺死 對boss造成傷害?
            if (deadClug == false)
            {
                //對boss造成傷害?
                baua.GetComponent<bumbArea>().damageToPlayer = false;
                baua.SetActive(true);
                deadClug = true;
            }
            Destroy(gameObject,0.1f);
        }
    }

    public void triggerBumb()
    {
        Debug.Log("Triggered");
        //animator.SetTrigger("");
        baua.SetActive(true);

        Destroy(gameObject, 0.3f);
    }

    public void hurtPlayer()
    {
        baua.SetActive(false);

        gCore.deadReason = "強盜首領衝刺攻擊";
        pCore.injured(damage);
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
    }
}
