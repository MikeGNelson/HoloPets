using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerScript : MonoBehaviour
{
    // -------------------------------------
    private float BORED_THRESHOLD = 3.0f;
    private float WANDER_RADIUS = 5.0f;
    // -------------------------------------

    public Transform playerCamera;
    public GameObject fox;
    public GameObject chasePoint;
    public GameObject chaseLine;
    public Transform headPosition;
    public Transform palmR;
    public Transform palmL;
    public BoxCollider frontBox, leftBox, rightBox;

    public Animator selfAnimator;
    private bool shouldReturn = false;
    private bool frontBlocked = false;
    private bool leftBlocked = false;
    private bool rightBlocked = false;

    // Start is called before the first frame update
    void Start()
    {
        selfAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ----- moving around -----
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

        // ----- chasing -----
        if (selfAnimator.GetBool("Chasing") == true)
        {
            selfAnimator.SetFloat("Bored", 0.0f);
            chasePoint.SetActive(true);
            chaseLine.SetActive(true);
            chasePoint.GetComponent<MeshRenderer>().enabled = true;
            chaseLine.GetComponent<LineRenderer>().enabled = true;

            Vector3 toPoint = new Vector3(chasePoint.transform.position.x, 0, chasePoint.transform.position.z) - new Vector3(fox.transform.position.x, 0, fox.transform.position.z);
            float toPointAng = Vector3.SignedAngle(toPoint, fox.transform.forward, Vector3.up);

            if (toPoint.magnitude > 0.2f)
            {
                if (Mathf.Abs(toPointAng) > 5.0f)
                {
                    if (toPointAng > 0.0f)
                    {
                        selfAnimator.SetFloat("HSpeed", -1.0f);
                    }
                    else
                    {
                        selfAnimator.SetFloat("HSpeed", 1.0f);
                    }
                }
                else
                {
                    selfAnimator.SetFloat("HSpeed", 0.0f);
                }
                selfAnimator.SetFloat("VSpeed", Mathf.Min(1.0f, toPoint.magnitude));
            }
            else
            {
                selfAnimator.SetFloat("VSpeed", 0.0f);
            }
        }
        else
        {
            chasePoint.GetComponent<MeshRenderer>().enabled = false;
            chaseLine.GetComponent<LineRenderer>().enabled = false;
            chasePoint.SetActive(false);
            chaseLine.SetActive(false);
        }

        // ----- special movement -----
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

        // ----- petted -----
        if(Vector3.Distance(headPosition.position, palmR.position) < fox.transform.localScale.x ||
            Vector3.Distance(headPosition.position, palmL.position) < fox.transform.localScale.x)
        {
            selfAnimator.SetBool("Petted", true);
        }
        else
        {
            selfAnimator.SetBool("Petted", false);
        }

        // ----- idle -----
        if(selfAnimator.GetComponent<Pet>().state != Pet.State.Idle)
        {
            selfAnimator.SetFloat("Bored", 0.0f);
            selfAnimator.SetBool("Wandering", false);
            selfAnimator.SetFloat("WanderingRand", 0.0f);
        }
        else if (selfAnimator.GetFloat("Bored") < BORED_THRESHOLD)
        {
            selfAnimator.SetFloat("Bored", selfAnimator.GetFloat("Bored") + Time.deltaTime);
            selfAnimator.SetBool("Wandering", false);
        }
        else if (selfAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fox_Idle"))
        {
            selfAnimator.SetBool("Wandering", true);
        }

        if (selfAnimator.GetComponent<Pet>().state != Pet.State.Sit)
        {
            selfAnimator.SetBool("ShouldSit", false);
        }

        if (selfAnimator.GetBool("Wandering"))
        {
            selfAnimator.SetFloat("WanderingRand", Random.Range(0.0f, 20.0f));
            if(frontBlocked)
            {
                if(leftBlocked)
                {
                    if (rightBlocked)
                    {
                        selfAnimator.SetBool("ShouldBack", true);
                    }
                    else
                    {
                        selfAnimator.SetBool("ShouldTurnRight", true);
                    }
                }
                else
                {
                    if (rightBlocked)
                    {
                        selfAnimator.SetBool("ShouldTurnLeft", true);
                    }
                }
            }
            else
            {
                selfAnimator.SetBool("ShouldTurnLeft", false);
                selfAnimator.SetBool("ShouldTurnRight", false);
            }
            if(!(leftBlocked && rightBlocked))
            {
                selfAnimator.SetBool("ShouldBack", false);
            }
            // check if too far away from the player
            Vector3 toPlayer = new Vector3(0, 0, 0) - new Vector3(fox.transform.position.x, 0, fox.transform.position.z);
            float toPlayerAng = Vector3.SignedAngle(toPlayer, fox.transform.forward, Vector3.up);
            if (toPlayer.magnitude >= WANDER_RADIUS && Mathf.Abs(toPlayerAng) > 90.0f)
            {
                shouldReturn = true;
                selfAnimator.SetBool("Wandering", false);
            }
        }

        if(shouldReturn)
        {
            Vector3 toPlayer = new Vector3(playerCamera.position.x, 0, playerCamera.position.z) - new Vector3(fox.transform.position.x, 0, fox.transform.position.z);
            float toPlayerAng = Vector3.SignedAngle(toPlayer, fox.transform.forward, Vector3.up);

            if (toPlayer.magnitude < WANDER_RADIUS)
            {
                shouldReturn = false;
                selfAnimator.SetBool("Wandering", true);
            }

            if (toPlayer.magnitude > 0.2f)
            {
                if (Mathf.Abs(toPlayerAng) > 5.0f)
                {
                    if (toPlayerAng > 0.0f)
                    {
                        selfAnimator.SetFloat("HSpeed", -1.0f);
                    }
                    else
                    {
                        selfAnimator.SetFloat("HSpeed", 1.0f);
                    }
                }
                else
                {
                    selfAnimator.SetFloat("HSpeed", 0.0f);
                }
                selfAnimator.SetFloat("VSpeed", Mathf.Min(1.0f, toPlayer.magnitude));
            }
            else
            {
                selfAnimator.SetFloat("VSpeed", 0.0f);
            }
        }
    }
    
    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.Equals(frontBox))
        {
            frontBlocked = true;
        }
        if (collision.collider.Equals(leftBox))
        {
            leftBlocked = true;
        }
        if (collision.collider.Equals(rightBox))
        {
            rightBlocked = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.Equals(frontBox))
        {
            frontBlocked = false;
        }
        if (collision.collider.Equals(leftBox))
        {
            leftBlocked = false;
        }
        if (collision.collider.Equals(rightBox))
        {
            rightBlocked = false;
        }
    }

    public void StartSit()
    {
        //selfAnimator.SetBool("SittingChange", true);
        //Invoke("StopSittingChange", 0.1f);
        selfAnimator.SetFloat("Bored", 0.0f);
        selfAnimator.SetBool("Wandering", false);
        selfAnimator.SetFloat("VSpeed", 0.0f);
        selfAnimator.SetBool("ShouldSit", true);
    }
    public void StopSit()
    {
        //selfAnimator.SetBool("SittingChange", true);
        //Invoke("StopSittingChange", 0.1f);
        selfAnimator.SetFloat("Bored", 0.0f);
        selfAnimator.SetBool("ShouldSit", false);
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
