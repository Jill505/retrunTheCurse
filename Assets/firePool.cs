using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<bumbBarrel>() != null)
        {
            //Trigger it
            collision.gameObject.GetComponent<bumbBarrel>().triggerBumb();
        }
    }
}
