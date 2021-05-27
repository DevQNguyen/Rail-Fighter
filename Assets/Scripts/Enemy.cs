using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // serialize particle system for explosion
    [SerializeField] GameObject enemyExplosionPrefab;
    [SerializeField] Transform spawnedObjects;


    void OnParticleCollision(GameObject other)
    {
        // Grab reference to explosion prefab gameObject
        GameObject instantiatedExplosion = Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
        // Put instantiated transform into a parent gameObject
        instantiatedExplosion.transform.SetParent(spawnedObjects);
        // Destroy this enemy object
        Destroy(this.gameObject);
    }
}


