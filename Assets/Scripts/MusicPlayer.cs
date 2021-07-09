using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
        // If more than 1 music players, destroy this instance
        if (numMusicPlayers > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Prevent music player from being reload each time player dies
            DontDestroyOnLoad(this.gameObject);
        }
    }
}


