using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_towerKeeper : MonoBehaviour
{
    public bool allowTalking = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Force player to listen shit
            
        }
    }
    
    IEnumerator talkShit()
    {
        yield return null;
    }
}
