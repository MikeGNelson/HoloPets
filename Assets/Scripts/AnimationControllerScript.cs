using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    // -------------------------------------
    private float BORED_THRESHOLD = 2.0f;
    // -------------------------------------

    private Animator selfAnimator;

    // Start is called before the first frame update
    void Start()
    {
        selfAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            if (selfAnimator.GetFloat("VSpeed") < 1.0f)
                selfAnimator.SetFloat("VSpeed", selfAnimator.GetFloat("VSpeed") + 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            if (selfAnimator.GetFloat("VSpeed") > -1.0f)
                selfAnimator.SetFloat("VSpeed", selfAnimator.GetFloat("VSpeed") - 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }
        else
        {
            if (selfAnimator.GetFloat("VSpeed") > 0.1f)
                selfAnimator.SetFloat("VSpeed", selfAnimator.GetFloat("VSpeed") - 0.1f);
            else if (selfAnimator.GetFloat("VSpeed") < -0.1f)
                selfAnimator.SetFloat("VSpeed", selfAnimator.GetFloat("VSpeed") + 0.1f);
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (selfAnimator.GetFloat("HSpeed") < 1.0f)
                selfAnimator.SetFloat("HSpeed", selfAnimator.GetFloat("HSpeed") + 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            if (selfAnimator.GetFloat("HSpeed") > -1.0f)
                selfAnimator.SetFloat("HSpeed", selfAnimator.GetFloat("HSpeed") - 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }
        else
        {
            if (selfAnimator.GetFloat("HSpeed") > 0.1f)
                selfAnimator.SetFloat("HSpeed", selfAnimator.GetFloat("HSpeed") - 0.1f);
            else if (selfAnimator.GetFloat("HSpeed") < -0.1f)
                selfAnimator.SetFloat("HSpeed", selfAnimator.GetFloat("HSpeed") + 0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selfAnimator.SetBool("Jumping", true);
            Invoke("StopJumping", 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selfAnimator.SetBool("SittingChange", true);
            Invoke("StopSittingChange", 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selfAnimator.SetBool("FallingLeft", true);
            Invoke("StopFallingLeft", 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selfAnimator.SetBool("ChaseTail", true);
            Invoke("StopChaseTail", 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            selfAnimator.SetBool("Roll", true);
            Invoke("StopRoll", 0.1f);
            selfAnimator.SetFloat("Bored", 0.0f);
        }

        if (selfAnimator.GetFloat("Bored") < BORED_THRESHOLD)
        {
           // selfAnimator.SetFloat("Bored", selfAnimator.GetFloat("Bored") + Time.deltaTime);
            selfAnimator.SetBool("Wandering", false);
        }
        else if (selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fox_Idle"))
        {
            selfAnimator.SetBool("Wandering", true);
        }
        
        if (selfAnimator.GetBool("Wandering"))
        {
            selfAnimator.SetFloat("WanderingRand", Random.Range(0.0f, 20.0f));
        }
    }
    public void StartSit()
    {
        selfAnimator.SetBool("SittingChange", true);
        Invoke("StopSittingChange", 0.1f);
        selfAnimator.SetFloat("Bored", 0.0f);
    }
    void StopJumping()
    {
        selfAnimator.SetBool("Jumping", false);
    }
    void StopSittingChange()
    {
        selfAnimator.SetBool("SittingChange", false);
    }
    void StopFallingLeft()
    {
        selfAnimator.SetBool("FallingLeft", false);
    }
    void StopChaseTail()
    {
        selfAnimator.SetBool("ChaseTail", false);
    }
    void StopRoll()
    {
        selfAnimator.SetBool("Roll", false);
    }
}
