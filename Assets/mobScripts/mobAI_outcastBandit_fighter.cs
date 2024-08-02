using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_outcastBandit_fighter : MonoBehaviour
{
    public PlayerCore pCore;
    public Rigidbody2D rb2d;

    public GameObject banditDamageZone;
    public GameObject banditDetecZone;
    public MobCore mobCore;

    //殭屍技能組數據
    public float banditDamage = 1;
    public float banditAttackSpeed = 0.8f; //casting

    public float banditMoveSpeed = 0.2f;
    public float banditMaxmentSpeed = 3f;
    public float banditFacingDiraction = 0;
    float myVelocity;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        mobCore = gameObject.GetComponent<MobCore>();
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        banditDamageZone = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (mobCore.aiFunctioning)
        {
            Vector2 playerPosition = pCore.gameObject.transform.position;

            if (transform.position.x > playerPosition.x)
            {
                //player is at mob's right
                banditFacingDiraction = -1;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                animator.SetBool("running",true);
            }
            else
            {
                //player is at mob's left
                banditFacingDiraction = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("running", true);
            }

            rb2d.velocity = new Vector2(myVelocity * banditFacingDiraction, rb2d.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(myVelocity) < banditMoveSpeed)
        {
            myVelocity += banditMoveSpeed;
        }
    }

    public void AttackPlayer()
    {
        myVelocity = 0;
        banditFacingDiraction = 0;

        animator.SetTrigger("attack");
        animator.SetBool("running", false);

        mobCore.aiFunctioning = false;
        mobCore.casting = true;
        Debug.Log("stoped");

        Invoke("damageRecover", banditAttackSpeed);
    }
    public void damageRecover()
    {
        Debug.Log("Active");
        mobCore.aiFunctioning = true;
        mobCore.casting = false;

        myVelocity = 0;
        banditFacingDiraction = 0;
    }
    public void hurtPlayer()
    {
        GameObject.Find("GameCore").GetComponent<GameCore>().deadReason = "外來強盜進戰攻擊";
        pCore.injured(banditDamage);
    }

    public void banditDead()
    {
        mobCore.aiFunctioning = false;
        banditFacingDiraction = 0;
    }
}
