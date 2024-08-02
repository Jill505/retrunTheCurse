using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class MobCore : MonoBehaviour
{
    public GameCore gCore;
    public Transform playerTransform;

    public float hp;
    public bool dead;

    public GameObject deadSound;
    public bool aiFunctioning = true;

    public enum deadCallType { noDeadCall, explode };//亡語類型
    public deadCallType callType = deadCallType.noDeadCall;

    public bool deadClug = false;

    public float trackingDistance = 8.5f;
    public float distanceWithPlayer;

    public bool casting = false;//怪物冷卻中 


    //public string mobType = "Zombie";

    // Start is called before the first frame update
    void Start()
    {
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        playerTransform = GameObject.Find("thePlayer").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceWithPlayer = Vector2.Distance(gameObject.transform.position, playerTransform.position);


        if (dead == false)
        {
            cal();
            if (distanceWithPlayer < trackingDistance)//玩家在追蹤範圍內
            {
                if (casting == false)
                {
                    aiFunctioning = true;
                }
            }
            else
            {
                aiFunctioning = false;
            }
        }
        else
        {
            aiFunctioning = false;

            //觸發亡語
            deadCall();
        }
    }

    void cal()
    {
        if (hp <= 0)
        {
            //dead
            Debug.Log(gameObject.name + "is dead");
            dead = true;

            if (deadSound != null)
            {
                Instantiate(deadSound);
            }

            gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);

            gCore.slashJam();
        }
    }

    void deadCall()
    {
        if (deadClug == false)
        {
            deadClug = true;
            GameObject.Find("CameraTracker").GetComponent<cameraTrack>().closeUp();
        }


        switch(callType)
        {
            case deadCallType.noDeadCall:
                break;

            case deadCallType.explode:

                break;
        }
    }
}
