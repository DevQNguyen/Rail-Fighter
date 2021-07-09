using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int SingleHitValue = 1;
    
    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] GameObject enemyHitPrefab;

    SoundEffectsPlayer SFXPlayer;
    GameObject spawnedObjectHolder;
    ScoreBoard scoreBoard;
    Rigidbody enemyRigidBody;
    
    int dragoValue = 5;
    int hornetValue = 5;
    int mantaValue = 3;
    int medusaValue = 3;
    int pythonValue = 2;
    int warhogValue = 2;
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

        // Assign reference to SpawnObjHolder transform type in Heirarchy
        spawnedObjectHolder = GameObject.FindWithTag("SpawnedObjHolder");
        if (spawnedObjectHolder == null)
        {
            Debug.Log("SpawnedObjHolder object not found.");
        }

        // Assign reference to SFX AudioSource player
        SFXPlayer = FindObjectOfType<SoundEffectsPlayer>();
        if (SFXPlayer == null)
        {
            Debug.Log("SoundEffectsPlayer object not found.");
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

    private void OnParticleCollision()
    {
        EnemyHitSequence();
    }

    private void EnemyHitSequence()
    {
        hitCount++;

        HitVFXSequence();
        
        ProcessScore();
        
        // Destroy enemy?
        if (toDestroy == true)
        {
            EnemyDestroySequence();
        }
    }

    private void HitVFXSequence()
    {
        // Instantiate the hit vfx on this gameObject
        GameObject instantiateHitVFX = Instantiate(enemyHitPrefab, transform.position, Quaternion.identity);
        // Put this hit vfx object into the 'spawnObjectHolder'
        instantiateHitVFX.transform.SetParent(spawnedObjectHolder.transform);

        // ***Play single hit VFX
        //SFXPlayer.PlayImpactClip();
        
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

        // ***Play enemy explosion VFX
        //SFXPlayer.PlayExplosionClip();

        // Destroy this enemy object
        Destroy(this.gameObject);
    }
}











