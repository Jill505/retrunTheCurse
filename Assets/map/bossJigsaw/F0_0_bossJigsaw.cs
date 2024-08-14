using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F0_0_bossJigsaw : MonoBehaviour
{
    public Collider2D triggerZone;
    public bool cast = false;
    public Animator bossJigsawAnimator;

    public mobAI_bossF0_0 boss_Ai;
    public MobCore boss_Core;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cast == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                cast = true;
                Debug.Log("Player get into the zone!");

                //限制場地
                openNextStage();

                //控制玩家無法移動

                //召喚boss

                //開放玩家移動


                //Start boss fight
                StartCoroutine(coroutine());
            }
        }
    }

    IEnumerator coroutine()
    {
        yield return new WaitForSeconds(3f)
            ;
        boss_Core.trackingDistance = 20;
        boss_Core.aiFunctioning = true;
    }

    public void openNextStage()
    {
        //Play Animate
        bossJigsawAnimator.SetTrigger("active");
    }

    public void closeRoad()
    {

    }
}
