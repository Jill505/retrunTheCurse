using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawCornerStone : MonoBehaviour
{
    public bool[] top = new bool[18];
    public bool[] button = new bool[18];

    public bool upCast = false;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("thePlayer").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (upCast == false)
        {
            if (playerTransform.position.y > transform.position.y)
            {
                upCast = true;
                GameObject.Find("GameCore").GetComponent<GameCore>().highScore++;
                Debug.Log(gameObject.name);
            }

        }
    }
}
