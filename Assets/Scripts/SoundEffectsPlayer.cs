using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
    [SerializeField] AudioClip laserBlasterClip;
    [SerializeField] AudioClip impactClip;
    [SerializeField] AudioClip explosionClip;

    [SerializeField] float laserVol = 0.5f;
    [SerializeField] float impactVol = 0.5f;
    [SerializeField] float explosionVol = 0.75f;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        if (audioSource == null)
        {
            Debug.Log("AudioSource component not found.");
        }
    }

    public void PlayLaserClip()
    {
        audioSource.PlayOneShot(laserBlasterClip, laserVol);
    }

    public void PlayImpactClip()
    {
        audioSource.PlayOneShot(impactClip, impactVol);
    }

    public void PlayExplosionClip()
    {
        audioSource.PlayOneShot(explosionClip, explosionVol);
    }
}



