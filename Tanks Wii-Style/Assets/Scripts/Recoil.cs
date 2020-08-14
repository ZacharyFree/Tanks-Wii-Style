using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Animation recoil;
    public Animator recoiler;
    private int timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetRecoilBool();
        
    }

    void SetRecoilBool()
    {
        if (Input.GetButton("Fire1"))
        {

                recoiler.SetBool("OnFire", true);

        }

        if (timer != 5)
        {
            timer++;
        }
        else
        {
            recoiler.SetBool("OnFire", false);
            timer = 0;
        }
    }

    
}
