using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechCommands : MonoBehaviour
{
    public Transform ball;
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
    }

    public void Fetch()
    {

    }

    public void Return()
    {
        fox.GetComponent<Pet>().isReturning = true;
    }

    public void ChangeColor()
    {
        
        foxRenderer.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
}
