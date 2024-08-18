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

    public float knockForceV = 8;//�������h�O�D
    public float knockForceH = 3;//�������h�O�D
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
            //�Q���a���� ��boss�y���ˮ`?
            if (deadClug == false)
            {
                //��boss�y���ˮ`?
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

        gCore.deadReason = "�j�s����Ĩ����";
        pCore.injured(damage);
    }
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
        pCore.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(knockForceH * dir, knockForceV);
        yield return new WaitForSeconds(knockTime);

        gCore.ableToControl = true;
    }
}
