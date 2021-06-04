using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int SingleHitValue = 1;

    [SerializeField] int dragoValue = 5;
    [SerializeField] int hornetValue = 5;
    [SerializeField] int mantaValue = 3;
    [SerializeField] int medusaValue = 3;
    [SerializeField] int pythonValue = 2;
    [SerializeField] int warhogValue = 2;
    [SerializeField] int capsuleValue = 2;
    [SerializeField] int sphereValue = 3;

    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] GameObject enemyHitPrefab;
    GameObject spawnedObjectHolder;
    ScoreBoard scoreBoard;
    Rigidbody enemyRigidBody;

    int enemyScoreValue;
    int hitCount;
    bool toDestroy;


    private void Start()
    {
        toDestroy = false;
        // Reference ScoreBoard script
        scoreBoard = FindObjectOfType<ScoreBoard>();
        if (scoreBoard == null)
        {
            Debug.Log("ScoreBoard object not found.");
        }

        // Add a Rigidbody to this gameObject
        AddRigidbody();

        // Assign reference to SpawnObjTransforms gameObject in Heirarchy
        spawnedObjectHolder = GameObject.Find("SpawnedObjTransforms");
        if (spawnedObjectHolder == null)
        {
            Debug.Log("SpawnedObjTransforms object not found.");
        }
    }

    private void AddRigidbody()
    {
        enemyRigidBody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        if (enemyRigidBody == null)
        {
            Debug.Log("Rigidbody not found on this gameObject.");
        }
        enemyRigidBody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        EnemyHitSequence();
    }

    private void EnemyHitSequence()
    {
        hitCount++;

        PlayHitVFX();
        
        ProcessScore();
        
        // Destroy enemy?
        if (toDestroy == true)
        {
            EnemyDestroySequence();
        }
    }

    private void PlayHitVFX()
    {
        // Instantiate the hit vfx on this gameObject
        GameObject instantiateHitVFX = Instantiate(enemyHitPrefab, transform.position, Quaternion.identity);
        // Put this hit vfx object into the 'spawnObjectHolder.transform'
        instantiateHitVFX.transform.SetParent(spawnedObjectHolder.transform);
    }

    private void ProcessScore()
    {
        SetEnemyScoreValue();
        // If enemy's hitcount greater than it's score value
        if (hitCount > enemyScoreValue)
        {
            toDestroy = true;
            Debug.Log($"You Destroyed [{gameObject.transform.tag}]!!!");
            scoreBoard.AddScore(enemyScoreValue);
        } else
        {
            scoreBoard.AddScore(SingleHitValue);
        }
    }

    private void SetEnemyScoreValue()
    {
        // Ship tag name dynamically pulled based on gameObject
        string shipTagName = gameObject.transform.tag;
        
        switch (shipTagName)
        {
            case "Drago":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = dragoValue;
                break;
            case "Hornet":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = hornetValue;
                break;
            case "Manta":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = mantaValue;
                break;
            case "Medusa":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = medusaValue;
                break;
            case "Python":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = pythonValue;
                break;
            case "Warhog":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = warhogValue;
                break;
            case "Enemy3":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = capsuleValue;
                break;
            case "Enemy5":
                Debug.Log($"You hit {shipTagName}!!");
                enemyScoreValue = sphereValue;
                break;
            default:
                Debug.Log("Error, could not find object tag!");
                break;
        }
    }

    private void EnemyDestroySequence()
    {
        // Grab reference to explosion prefab gameObject
        GameObject instantiatedExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
        // Put instantiated transform into a parent gameObject
        instantiatedExplosion.transform.SetParent(spawnedObjectHolder.transform);
        // Destroy this enemy object
        Destroy(this.gameObject);
    }
}











