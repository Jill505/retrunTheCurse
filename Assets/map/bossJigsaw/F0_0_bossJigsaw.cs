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

    public bool deadClug = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (boss_Core.dead == true)
        {
            if (deadClug == false)
            {
                Debug.Log("trigger");
                GameObject.Find("GameCore").GetComponent<GameCore>().winGame();
                deadClug = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cast == false)
        {
            if (collision.gameObject.tag == "Player")
            {
                GameObject.Find("BossLevelBloodCanvas").GetComponent<bossBloodTrack>().switchOn();
                cast = true;
                Debug.Log("Player get into the zone!");

                //������a
                openNextStage();

                GameObject.Find("GameCore").GetComponent<GameCore>().bossNameShowText.text = "�~�ӱj�s����\n����";
                GameObject.Find("gmaeCanvas").GetComponent<Animator>().SetTrigger("bossTrigger");
                //����a�L�k����

                //�l��boss

                //�}�񪱮a����


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

        //TEMP
    }
    public void closeRoad()
    {

    }
}
