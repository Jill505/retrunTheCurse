using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bossBloodTrack : MonoBehaviour
{
    public bool Tracking = false;

    public MobCore mCore;

    public Image bloodImage;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Tracking)
        {
            if (mCore != null)
            {
                bloodImage.fillAmount = mCore.hp / mCore.maxHp;
            }
        }
    }
   
    public void switchOn()
    {
        animator.SetBool("active",true);
    }
    public void switchOff()
    {
        animator.SetBool("active", false);
    }
}
