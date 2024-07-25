using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;

public class MobCore : MonoBehaviour
{
    public GameCore gCore;

    public float hp;
    public bool dead;

    public GameObject deadSound;
    public bool aiFunctioning = true;

    public enum deadCallType { noDeadCall, explode };//¤`»yÃþ«¬
    public deadCallType callType = deadCallType.noDeadCall;

    public bool deadClug = false;


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
        else
        {
            aiFunctioning = false;

            //Ä²µo¤`»y
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

            Instantiate(deadSound);
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
