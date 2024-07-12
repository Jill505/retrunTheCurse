using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debuger001 : MonoBehaviour
{
    public GameCore gCore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            //Debug.Log("there's something in my detecingZOne");
            gCore.facingDiraction = 0;
            /*
            switch (gameObject.name)
            {
                case ("touchDebuggerB"):
                    if (gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x < 0)
                    {
                        Debug.Log("left functioning");
                        //gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.y);
                        //gCore.facingDiraction = 0;
                    }
                    break;
                case ("touchDebuggerA"):
                    if (gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x > 0)
                    {
                        Debug.Log("right functioning");
                        gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.y);
                        gCore.facingDiraction = 0;   
                    }
                    break;
                case ("JumpDebug"):
                    break;
            }*/
        }
    }
}
