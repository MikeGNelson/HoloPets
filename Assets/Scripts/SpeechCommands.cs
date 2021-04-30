using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechCommands : MonoBehaviour
{
    public Transform ball;
    public Transform camera;
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
            Return();
        }

    }

    public void Fetch()
    {
        fox.GetComponent<Pet>().SetWalkTo(ball);
    }

    public void Return()
    {
        fox.GetComponent<Pet>().SetWalkTo(camera);
    }

    public void ChangeColor()
    {
        
        foxRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    public void Point()
    {
        fox.GetComponent<AnimationControllerScript>().selfAnimator.SetBool("Chasing", true);
    }
}
