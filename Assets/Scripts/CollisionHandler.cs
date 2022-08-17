using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float restartLevelDelay = 1f;
    [SerializeField] float levelPassDelay = 2.5f;
    [SerializeField] AudioClip rocketCrash, levelPass;
    [SerializeField] ParticleSystem crashParticles, levelPassParticles;

    AudioSource aS;
    

    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start() 
    {
        aS = GetComponent<AudioSource>();
               
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if(Input.GetKey(KeyCode.L))
        {
           LoadNextLevel();
        }
        else if(Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
     void OnCollisionEnter(Collision other) 
    {  
        if(isTransitioning || collisionDisabled){ return; }

        else if(isTransitioning== false)
        {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("Starting Zone");
            break;

            case "Fuel":
            Debug.Log("Fueled up!");
            break;

            case "Finish":
            LevelPassSequence();
            break;

            default:
            StartCrashSequence();
            break;
        }
        }
    }

    void LevelPassSequence()
    {
        isTransitioning=true;                                    // Debug.Log("Congratulations!!!");

        aS.Stop();
        levelPassParticles.Play();
        aS.PlayOneShot(levelPass);
        
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel", levelPassDelay);
    }

    void StartCrashSequence()
    {
        isTransitioning=true;       // Debug.Log("You've crashed.");

        aS.Stop();
        crashParticles.Play();
        aS.PlayOneShot(rocketCrash);

    
        GetComponent<Movement>().enabled= false;
        Invoke("ReloadLevel", restartLevelDelay);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex= 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void ReloadLevel()
    {
        // SceneManager.LoadScene(0); same same but different.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    
}
