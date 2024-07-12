using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footDec : MonoBehaviour
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {

        }
    }
}
