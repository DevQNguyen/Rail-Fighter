using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("Float value for number of seconds to delay.")]
    [SerializeField] float loadDelay = 1f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} triggered: {other.name}!!");
        RestartSequence();
    }

    void RestartSequence()
    {
        // Disable Player Controls script
        GetComponent<PlayerControls>().enabled = false;
        Debug.Log($"In [RestartSequence()] ");
        // Wait 1 sec, invoke ReloadScene()
        Invoke("ReloadScene", loadDelay);
    }

    void ReloadScene()
    {
        // Get current scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log($"In [RestartScene()] sceneIndex: {sceneIndex}");
        SceneManager.LoadScene(sceneIndex);
    }
}



