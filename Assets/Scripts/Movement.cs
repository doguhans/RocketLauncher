using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor
    
    //CACHE - e.g. regerences for readability or speed

    //STATE - private instance (member) variables
    [SerializeField] float mainThrust= 1000f;
    [SerializeField] float sideThrust= 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;

    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

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
            StartTrusting();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    void StartTrusting()
    {
        // Debug.Log("Pressed Space!! Thrusting through SPACE...");        // float yAxis= Time.deltaTime * speed;        // transform.Translate(0,yAxis,0);
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void ProcessRotation()
    {
    if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }

        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }

    }

    private void RotateRight()
    {
        // Debug.Log("Pressed D!! Turning Right...");

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
        ApplyRotation(-sideThrust);
    }

    private void RotateLeft()
    {
        // Debug.Log("Pressed A!! Turning Left...");

        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        ApplyRotation(sideThrust);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // rb.freezeRotation= true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        // rb.freezeRotation= false;
    }
}
