using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust= 1000f;
    [SerializeField] float sideThrust= 100f;
    Rigidbody rb ;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        audioSource= GetComponent<AudioSource>();
        rb.freezeRotation = true;
        audioSource.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
        // Debug.Log("Pressed Space!! Thrusting through SPACE...");        // float yAxis= Time.deltaTime * speed;        // transform.Translate(0,yAxis,0);
        
        rb.AddRelativeForce(Vector3.up* mainThrust* Time.deltaTime);
        if(!audioSource.isPlaying)
        { 
            audioSource.Play();
        }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
    if(Input.GetKey(KeyCode.A))
        {
            // Debug.Log("Pressed A!! Turning Left...");

            ApplyRotation(sideThrust);

        }


        else if(Input.GetKey(KeyCode.D))
        {
        // Debug.Log("Pressed D!! Turning Right...");

            ApplyRotation(-sideThrust);

        }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        // rb.freezeRotation= true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // rb.freezeRotation= false;
    }
}
