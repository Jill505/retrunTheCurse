using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
    public enum platform { phone, PC, console}
    public platform ControlMode = platform.phone;//0=手機 1=電腦 2=手把

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

    private Vector2 startTouchPosition; // 起始觸碰位置
    private Vector2 endTouchPosition; // 終止觸碰位置
    public float swipeRange = 50; // 判定滑動的距離範圍
    public float tapRange = 10; // 判定點擊的距離範圍



    //玩家參數
    public bool deadClug;

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

    //遊戲參數
    public Animator canvasAnimator;
    public Image deadPic;
    public Sprite deadCondition;
    public Sprite RevCondition;
    public Text deadText;
    public Button retryButton;
    public string deadReason;

    public bool ableToRev = false;

    public int revChance = 50;//0=不會復活 1=100%必定復活 2=50% 3=33.3% 4=25%... 以此類推


    // Start is called before the first frame update
    void Start()
    {
        slashCoolDownTime = slashCoolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (deadClug == false)
        {
            switch (ControlMode)
            {
                case platform.phone:
                    DetectSwipePhone();
                    break;

                case platform.PC:
                    DetectSwipeComputer();
                    break;

                case platform.console:
                    DetectSwipeConsole();
                    break;

                default:
                    break;
            }



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
        else
        {
            pCore.ivaincible = true;
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

    public void restartGame()
    {
        deadClug = false;
    }

    public void PlayerDead()
    {
        deadClug = true;
        //countingIfRev
        int randomRevEvent = Random.Range(1,revChance);
        if (randomRevEvent == 1)
        {
            //rev
            deadPic.sprite = RevCondition;
            ableToRev = true;
            retryButton.gameObject.transform.GetChild(0).GetComponent<Text>().text = "復活！";
            deadReason = "你死了... 嗎?";
            //play music
        }
        else
        {
            deadPic.sprite = deadCondition;
        }
        deadText.text = "死亡原因："+deadReason;

        //camera stop tracking

        //stop allowing player moving
        facingDiraction = 0;
        //show retry
        canvasAnimator.SetBool("dead",true);
        //record grade
    }

    public void RevOrRetry()
    {
        if (ableToRev == false)
        {
            //重試 重新加載區塊
            SceneManager.LoadScene("game");
        }
        else
        {
            //復活
            PlayerRevive();
        }
    }

    public void PlayerRevive()
    {
        canvasAnimator.SetBool("dead", false);
        pCore.health = 1;
        deadClug = false;
        pCore.ivaincible = false;
    }

    void DetectSwipePhone()
    {

        // 偵測到手指按下螢幕
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            startTouchPosition = Input.mousePosition;
        }

        // 偵測到手指放開螢幕
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            Vector2 distance = endTouchPosition - startTouchPosition;

            if (actionClug == false)
            {
                // 如果手指滑動的距離在點擊範圍內，判定為點擊
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
                // 否則計算滑動的角度
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
        // 根據滑動角度判定滑動方向
        if (angle > -45 && angle <= 45)
        {
            //Debug.Log("向右滑動");
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingDiraction = 1;
        }
        else if (angle > 45 && angle <= 135)
        {
            //Debug.Log("向上滑動");
            Jump();
        }
        else if (angle > -135 && angle <= -45)
        {
            //Debug.Log("向下滑動");

            facingDiraction = 0;
            mySpeed = 0;
        }
        else
        {
            //Debug.Log("向左滑動");
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingDiraction = -1;
        }
    }


    void DetectSwipeComputer()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            player.transform.localRotation = Quaternion.Euler(0, 180, 0);
            facingDiraction = -1;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.transform.localRotation = Quaternion.Euler(0, 0, 0);
            facingDiraction = 1;

        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.J))
        {
            if (slashCooldowning == false)
            {
                slash();
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            facingDiraction = 0;
            mySpeed = 0;
        }

    }

    void DetectSwipeConsole()
    {

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
