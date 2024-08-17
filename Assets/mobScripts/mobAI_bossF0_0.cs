using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_bossF0_0 : MonoBehaviour
{
    public PlayerCore pCore;
    public GameCore gCore;
    public MobCore mCore;

    public Rigidbody2D rb2d;

    public GameObject thorns;

    //boss AI把计
    public float castCD = 0.5f;
    public bool casting = false;

    public float bossDashCD = 5f;
    public float bossDashCDcount = 7f;

    public float facingDiraction = 0;

    //boss skill把计
    public float skill_SpawnCrystalCD = 7f;
    public float skill_SpawnCrystalCDcount = 1f;

    public float skill_SpwanCrystalInterval = 0.3f;
    public int skill_SpawnCrystalNumber = 5;

    public float skill_dashAttackCD = 5f;
    public float skill_dashAttackCDcount = 3f;

    public float skill_bombBucketCD = 4f;
    public float skill_bombBucketCDcount = 0.5f;

    public float skill_bladeSwordCD = 7f;
    public float skill_bladeSwordCDcount = 5f;

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
            //磅︽Ai
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
        Debug.Log("垂");
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
            yield return new WaitForSeconds(0.1f);
        }
    }
public void skill_dashAttack()
    {
        Debug.Log("侥脓");

    }
    public void skill_bladeSword()
    {
        Debug.Log("疨糃");

    }

    public void skill_bombBucket()
    {
        Debug.Log("媚表");

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
