using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public Transform target;
    public bool isReturning = false;
    public bool isFetching = false;
    public bool withBall = false;
    private float speed = 5;

    public enum State
    {
        Idle,
        Sit,
        Walk,
        Sleep,
        Eat,
        Pet
    }

    public State state;
    private Animator selfAnimator;
    // Start is called before the first frame update
    void Start()
    {
        selfAnimator = GetComponent<Animator>();
        state = State.Idle;
    }

    public void SetWalkTo(Transform t)
    {
        isReturning = true;
        state = State.Walk;
        target = t;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Walk:
                if (isReturning)
                {
                    float step = speed * Time.deltaTime /10f;
                    Vector3 altTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
                    selfAnimator.SetFloat("VSpeed", selfAnimator.GetFloat("VSpeed") + 0.1f);
                    transform.position = Vector3.MoveTowards(transform.position, altTarget, step);
                    transform.rotation = Quaternion.LookRotation(altTarget - transform.position, this.transform.up);
                    if (Vector3.Distance(transform.position, altTarget) <= 0.5f)
                    {
                        if(isFetching)
                        {
                            if (!withBall)
                            {
                                withBall = true;
                            }
                            else
                            {
                                isFetching = false;
                                withBall = false;
                            }
                        }
                        isReturning = false;
                        state = State.Idle;
                    }
                }
                break;
            case State.Idle:
                isReturning = false;
                break;
            case State.Sit:
                //this.GetComponent<AnimationControllerScript>().StartSit();
                //state = State.Idle;
                isReturning = false;
                break;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 1.0f, transform.position.z), Time.deltaTime / 30f);
    }

    public void SetStateSit()
    {

            this.GetComponent<AnimationControllerScript>().StartSit();
            state = State.Sit;
    }

    public void SetStateIdle()
    {
        if (state == State.Sit)
        {
            this.GetComponent<AnimationControllerScript>().StopSit();
        }
        state = State.Idle;
    }
}
