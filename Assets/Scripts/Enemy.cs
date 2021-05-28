using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] Transform spawnedObjects;
    ScoreBoard scoreBoard;

    [SerializeField] int enemyScoreValue;

    private void Start()
    {
        // Reference ScoreBoard object
        scoreBoard = FindObjectOfType<ScoreBoard>();
        if (scoreBoard == null)
        {
            Debug.Log("ScoreBoard object not found.");
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessScore();
        Debug.Log($"Enemy value: {enemyScoreValue}");
        EnemyDestroySequence();
    }

    private void ProcessScore()
    {
        SetEnemyScoreValue();
        scoreBoard.AddScore(enemyScoreValue);
    }

    private void SetEnemyScoreValue()
    {
        if (gameObject.CompareTag("Enemy3"))
        {
            enemyScoreValue = 3;
        } else
        {
            enemyScoreValue = 5;
        }
    }

    private void EnemyDestroySequence()
    {
        // Grab reference to explosion prefab gameObject
        GameObject instantiatedExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
        // Put instantiated transform into a parent gameObject
        instantiatedExplosion.transform.SetParent(spawnedObjects);
        // Destroy this enemy object
        Destroy(this.gameObject);
    }
}




