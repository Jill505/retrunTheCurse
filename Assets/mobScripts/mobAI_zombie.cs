using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_zombie : MonoBehaviour
{

    public PlayerCore pCore;
    public Rigidbody2D rb2d;

    public GameObject zombieDamageZone;

    public bool aiFunctioning = true;

    //殭屍技能組數據
    public float zombieDamage = 1;
    public float zombieAttackSpeed = 0.8f; //casting
    
    public float zombieMoveSpeed = 0.2f;
    public float zombieMaxmentSpeed = 3f;
    public float zombieFacingDiraction = 0;
    float myVelocity;

    // Start is called before the first frame update
    void Start()
    {
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        zombieDamageZone = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiFunctioning)
        {
            Vector2 playerPosition = pCore.gameObject.transform.position;

            if (transform.position.x > playerPosition.x)
            {
                //player is at mob's right
                zombieFacingDiraction = -1;
                transform.localRotation = Quaternion.Euler(0,180,0);
            }
            else
            {
                //player is at mob's left
                zombieFacingDiraction = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            rb2d.velocity = new Vector2(myVelocity * zombieFacingDiraction, rb2d.velocity.y);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(myVelocity) < zombieMaxmentSpeed)
        {
            myVelocity += zombieMoveSpeed;
        }
    }

    public void hurtPlayer()
    {
        myVelocity = 0;
        zombieFacingDiraction = 0;

        pCore.injured(zombieDamage);
        zombieDamageZone.SetActive(false);

        aiFunctioning = false;

        Invoke("damageRecover", zombieAttackSpeed);
    }
    public void damageRecover()
    {
        zombieDamageZone.SetActive(true);
        aiFunctioning = true;

        myVelocity = 0;
        zombieFacingDiraction = 0;
    }
}
