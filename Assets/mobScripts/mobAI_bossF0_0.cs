using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class mobAI_bossF0_0 : MonoBehaviour
{
    public PlayerCore pCore;
    public GameCore gCore;
    public MobCore mCore;

    public Rigidbody2D rb2d;

    public GameObject thorns;
    public GameObject firePool;
    public GameObject bumbBarrel;
    public GameObject foot;

    //boss AI°Ñ¼Æ
    public float castCD = 0.5f;
    public bool casting = false;

    public float bossDashCD = 5f;
    public float bossDashCDcount = 7f;

    public float facingDiraction = 0;

    //boss skill°Ñ¼Æ
    public float skill_SpawnCrystalCD = 7f;
    public float skill_SpawnCrystalCDcount = 1f;
    public float skill_SpwanCrystalInterval = 0.3f;
    public int skill_SpawnCrystalNumber = 5;

    public GameObject slashZone;
    public float skill_dashAttackCD = 5f;
    public float skill_dashAttackCDcount = 3f;
    public float skill_dashAttackHorForce = 14f;
    public float skill_dashAttackVarForce = 4f;
    public float skill_dashAttackDamage = 1f;

    public float skill_bombBucketCD = 4f;
    public float skill_bombBucketCDcount = 0.5f;
    public float skill_bombBucketHorForce = 3f;
    public float skill_bombBucketVarForce = 3f;

    public float skill_bladeSwordCD = 7f;
    public float skill_bladeSwordCDcount = 5f;
    public float skill_bladeSwordHorForce = 3f;
    public float skill_bladeSwordVarForce = 10f;
    public float skill_bladeSwordDamage = 1f;

    // Start is called before the first frame update
    void Start()
    {
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();

        mCore = gameObject.GetComponent<MobCore>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mCore.aiFunctioning)
        {
            //°õ¦æAi
            Vector2 playerPosition = pCore.gameObject.transform.position;

            if (transform.position.x > playerPosition.x)
            {
                //player is at mob's right
                facingDiraction = -1;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                //player is at mob's left
                facingDiraction = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        if (mCore.aiFunctioning)
        {
            if (casting == false)
            {
                if (skill_SpawnCrystalCDcount >= 0)
                {
                    skill_SpawnCrystalCDcount -= Time.fixedDeltaTime;
                }
                else
                {
                    skill_SpawnCrystal();
                    skill_SpawnCrystalCDcount = 0f;
                    skill_SpawnCrystalCDcount += skill_SpawnCrystalCD;
                    skill_SpawnCrystalCDcount += Random.Range(0.1f,1) ;
                }
            }

            if (casting == false)
            {
                if (skill_dashAttackCDcount >= 0)
                {
                    skill_dashAttackCDcount -= Time.fixedDeltaTime;
                }
                else
                {
                    skill_dashAttack();
                    skill_dashAttackCDcount = 0f;
                    skill_dashAttackCDcount += skill_dashAttackCD;
                    skill_dashAttackCDcount += Random.Range(0.1f, 1);
                }
            }

            if (casting == false)
            {
                if (skill_bladeSwordCDcount >= 0)
                {
                    skill_bladeSwordCDcount -= Time.fixedDeltaTime;
                }
                else
                {
                    skill_bladeSword();
                    skill_bladeSwordCDcount = 0f;
                    skill_bladeSwordCDcount += skill_bladeSwordCD;
                    skill_bladeSwordCDcount += Random.Range(0.1f, 1);
                }
            }

            if (casting == false)
            {
                if (skill_bombBucketCDcount >= 0)
                {
                    skill_bombBucketCDcount -= Time.fixedDeltaTime;
                }
                else
                {
                    skill_bombBucket();
                    skill_bombBucketCDcount = 0f;
                    skill_bombBucketCDcount += skill_bombBucketCD;
                    skill_bombBucketCDcount += Random.Range(0.1f, 1);
                }
            }
        }
    }

    public void skill_SpawnCrystal()
    {
        Debug.Log("¤ô´¹¨ë");
        castBreaking(Random.Range(0.8f, 1.8f));
        StartCoroutine(spawnCrystalCoroutine());
    }

    public float intervalDistance;
    IEnumerator spawnCrystalCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        Vector2 setpos = new Vector2(transform.position.x,transform.position.y);
        float setDir = facingDiraction;

        for (int i = 0; i < skill_SpawnCrystalNumber; i++)
        {
            setpos.x += intervalDistance* setDir;
            Instantiate(thorns,setpos,Quaternion.identity);
            yield return new WaitForSeconds(0.155f);
        }
    }
    public void skill_dashAttack()
    {
        Debug.Log("½Ä¨ë¬ðÅ§");
        rb2d.velocity += new Vector2(1*skill_dashAttackHorForce*facingDiraction,1*skill_dashAttackVarForce);
        //openSlashDec    
        slashZone.SetActive(true);
        Invoke("swapSkillDashAttack",2f);

        castBreaking(Random.Range(1f,1.8f));
    }
    public void swapSkillDashAttack()
    {
        slashZone.SetActive(false);
    }
    public void slashHurtPlayer()
    {
        GameObject.Find("GameCore").GetComponent<GameCore>().deadReason = "±jµs­º»â½Ä¨ë§ðÀ»";
        pCore.injured(skill_dashAttackDamage);
    }

    public bool bladJumping = false;
    public void skill_bladeSword()
    {
        Debug.Log("¯P¤õ¼C");
        castBreaking(Random.Range(2.2f, 4f));
        rb2d.velocity += new Vector2(1 * skill_bladeSwordHorForce * facingDiraction, 1 * skill_bladeSwordVarForce);

        rb2d.gravityScale = 3;

        bladJumping = true;
        //
        Invoke("bladeInvoke",0.1f);
    }
    public void bladeInvoke()
    {
        foot.GetComponent<Collider2D>().enabled = true;
    }
    public void skill_bladeSwordHitGround()
    {
        Instantiate(firePool,new Vector2(transform.position.x,transform.position.y-0.3f),Quaternion.identity);
        rb2d.gravityScale = 1;
    }

    public void skill_bombBucket()
    {
        Debug.Log("¬µÃÄ±í");
        castBreaking(Random.Range(0.1f, 0.8f));

        GameObject insBumb = Instantiate(bumbBarrel, new Vector2(transform.position.x, transform.position.y +0.8f), Quaternion.identity);
        insBumb.GetComponent<Rigidbody2D>().velocity += new Vector2(Random.RandomRange(1.5f,4f),Random.RandomRange(3f,6.3f));
    }


    public void castBreaking(float time)
    {
        Invoke("castInvoke", time);
    }
    public void castInvoke()
    {
        casting = false;
    }
}
