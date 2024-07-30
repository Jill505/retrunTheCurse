using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_outcastBandit_archer : MonoBehaviour
{
    public PlayerCore pCore;
    public Rigidbody2D rb2d;
    public MobCore mobCore;

    public GameObject arrow;

    public float attackDamage = 1f;
    public float facingDiraction = 0f;

    public float hitAngle;
    public Vector2 hitDiraction;
    // Start is called before the first frame update
    void Start()
    {
        mobCore = gameObject.GetComponent<MobCore>();
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (mobCore.aiFunctioning)
        {
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

            //hitDiraction = (Vector2)gameObject.transform.position - playerPosition;
            hitDiraction = playerPosition-(Vector2)gameObject.transform.position;

            float angleInRadians = Mathf.Atan2(hitDiraction.y, hitDiraction.x);
            hitAngle = angleInRadians * Mathf.Rad2Deg;
        }
    }

    private void FixedUpdate()
    {
        //shooting();
    }

    public void shooting()
    {
        Debug.Log(hitAngle);
        GameObject theArrow = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, hitAngle));
        //theArrow.transform.localEulerAngles = new Vector3(0,0,hitAngle);
    }
}
