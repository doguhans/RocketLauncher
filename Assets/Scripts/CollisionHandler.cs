using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float delay = 1f;
    private void OnCollisionEnter(Collision other) 
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

    void LevelPassSequence()
    {
        Debug.Log("Congratulations!!!");
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel", delay);
    }

    void StartCrashSequence()
    {
        Debug.Log("You've crashed.");
        GetComponent<Movement>().enabled= false;
        Invoke("ReloadLevel", delay);

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
