using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mobAI_bossF0_0 : MonoBehaviour
{
    public PlayerCore pCore;
    public GameCore gCore;
    public MobCore mCore;

    // Start is called before the first frame update
    void Start()
    {
        pCore = GameObject.Find("thePlayer").GetComponent<PlayerCore>();
        gCore = GameObject.Find("GameCore").GetComponent<GameCore>();

        mCore = gameObject.GetComponent<MobCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mCore.aiFunctioning)
        {
            //∞ı¶ÊAi
        }
    }
}
