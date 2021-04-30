using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechCommands : MonoBehaviour
{
    public Transform ball;
    public Transform playerCamera;
    public GameObject fox;
    public Renderer foxRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Fetch();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            Come();
        }

        if(fox.GetComponent<Pet>().isFetching)
        {
            if (!fox.GetComponent<Pet>().withBall)
            {
                fox.GetComponent<Pet>().SetWalkTo(ball);
            }
            else
            {
                fox.GetComponent<Pet>().SetWalkTo(playerCamera);
                ball.position = fox.transform.position + 0.25f * fox.transform.forward.normalized;
            }
        }
        else if(fox.GetComponent<Pet>().isReturning)
        {
            fox.GetComponent<Pet>().SetWalkTo(playerCamera);
        }
    }

    public void Fetch()
    {
        fox.GetComponent<Pet>().isFetching = true;
        fox.GetComponent<Pet>().SetWalkTo(ball);
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetFloat("Bored", 0.0f);
    }

    public void Come()
    {
        fox.GetComponent<Pet>().SetWalkTo(playerCamera);
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetFloat("Bored", 0.0f);
    }

    public void ChangeColor()
    {
        
        foxRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void Point()
    {
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetBool("Chasing", true);
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetFloat("Bored", 0.0f);
    }

    public void NotPoint()
    {
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetBool("Chasing", false);
    }
}
