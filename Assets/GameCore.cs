using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCore : MonoBehaviour
{
    public PlayerCore pCore;

    public GameObject player;
    public Rigidbody2D rb2d;

    public GameObject damageZone;

    public Image slashCooldownImage;

    public GameObject slashSoundEffect;


    public bool jumpClug;
    public bool slashing;
    public bool slashClug;
    public bool actionClug;
    public bool slashCooldowning;

    public bool ableToControl = true;

    private Vector2 startTouchPosition; // �_�lĲ�I��m
    private Vector2 endTouchPosition; // �פ�Ĳ�I��m
    public float swipeRange = 50; // �P�w�ưʪ��Z���d��
    public float tapRange = 10; // �P�w�I�����Z���d��



    //���a�Ѽ�
    public float mySpeed;

    public float injuredImortalTime = 0.8f;

    public float facingDiraction = 1f;

    public float movementIncreaseSpeed;
    public float maxmentSpeed;
    public float jumpForce;
    
    public float slashDurition;
    public float slashDistance;
    public float flipDurition;
    public float flipDistance;

    public float slashCoolDown=0.8f;
    public float slashCoolDownTime;




    // Start is called before the first frame update
    void Start()
    {
        slashCoolDownTime = slashCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        DetectSwipe();
        if (ableToControl)
        {
            rb2d.velocity = new Vector2(mySpeed * facingDiraction, rb2d.velocity.y);
        }

        if (slashClug == true)
        {
            //Debug.Log("slash clug functioning");
            rb2d.velocity = new Vector2(0, 0);
        }
        if (slashing == true)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        }
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(mySpeed + movementIncreaseSpeed) <= maxmentSpeed)
        {
            //Debug.Log("speedIncresing");
            mySpeed += movementIncreaseSpeed;
        }

        if (slashCoolDown >= slashCoolDownTime)
        {
            slashCoolDownTime += 0.02f;
            slashCooldownImage.fillAmount = (slashCoolDownTime / slashCoolDown);
        }
        else
        {
            slashCooldowning = false;
        }
    }

    void DetectSwipe()
    {

        // �����������U�ù�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startTouchPosition = Input.mousePosition;
        }

        // ����������}�ù�
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            Vector2 distance = endTouchPosition - startTouchPosition;

            if (actionClug == false)
            {
                // �p�G����ưʪ��Z���b�I���d�򤺡A�P�w���I��
                if (distance.magnitude < tapRange)
                {
                    Debug.Log("slash");

                    //test stop funciotn
                    //facingDiraction = 0;
                    //
                    if (slashCooldowning == false)
                    {
                        slash();
                    }

                }
                // �_�h�p��ưʪ�����
                else
                {
                    float angle = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;
                    DetectSwipeDirection(angle);
                }
            }
        }
    }

    void DetectSwipeDirection(float angle)
    {
        // �ھڷưʨ��קP�w�ưʤ�V
        if (angle > -45 && angle <= 45)
        {
            //Debug.Log("�V�k�ư�");
            player.transform.localRotation = Quaternion.Euler(0,0,0);
            facingDiraction = 1;
        }
        else if (angle > 45 && angle <= 135)
        {
            //Debug.Log("�V�W�ư�");
            Jump();
        }
        else if (angle > -135 && angle <= -45)
        {
            //Debug.Log("�V�U�ư�");

            facingDiraction = 0;
            mySpeed = 0;
        }
        else
        {
            //Debug.Log("�V���ư�");
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingDiraction = -1;
        }
    }

    void Jump()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
        rb2d.velocity += new Vector2(0,jumpForce);
    }
    void flip()
    {

    }
    void slash()
    {
        Instantiate(slashSoundEffect);
        StartCoroutine(slashCoroutine());
    }
    IEnumerator slashCoroutine()
    {
        slashCoolDownTime = 0;
        slashCooldowning = true;

        actionClug = true;
        slashing = true;
        mySpeed = slashDistance;

        //DamageZone Open
        damageZone.SetActive(true);
        pCore.ivaincible = true;

        yield return new WaitForSeconds(slashDurition);

        mySpeed = 0;
        slashClug = true;
        yield return new WaitForSeconds(0.1f);

        //DamageZone Close
        damageZone.SetActive(false);
        slashClug = false;
        actionClug = false;
        slashing = false;
        mySpeed = maxmentSpeed;
        yield return new WaitForSeconds(0.1f);

        pCore.ivaincible = false; 
    }

    void mapGen()
    {

    }

    public void stopClugging()
    {
        slashClug = false;
    }
    public void stopCluggingForJumpPad()
    {
        slashClug = false;
        slashing = false;
        slashCoolDownTime = slashCoolDown;
        slashCooldowning = false;
    }

    public void slashJam()
    {
        stopClugging();
        actionClug = false;
        slashCoolDownTime = slashCoolDown;
        slashCooldowning = false;
    }
}
