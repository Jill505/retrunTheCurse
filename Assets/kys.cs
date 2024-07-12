using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kys : MonoBehaviour
{
    public float deadTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,deadTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
