using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;

    [Tooltip("Float value for number of seconds to delay before reloading scene.")]
    [SerializeField] float reloadDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        CrashSequence(other);
    }

    private void CrashSequence(Collider other)
    {
        // Play Player Ship explosion particles
        explosionVFX.Play();

        // Destroy other gameObject
        Destroy(other.gameObject, 3f);

        // Disable player controls
        GetComponent<PlayerControls>().enabled = false;
        // Disable player ship mesh render
        GetComponent<MeshRenderer>().enabled = false;
        // Disable BoxCollider
        GetComponent<BoxCollider>().enabled = false;

        RestartSequence();
    }

    private void RestartSequence()
    {
        // Wait x sec, invoke ReloadScene()
        Invoke("ReloadScene", reloadDelay);
    }

    private void ReloadScene()
    {
        // Get current scene index
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}



