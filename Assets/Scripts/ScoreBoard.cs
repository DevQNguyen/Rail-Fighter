using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    public void AddScore(int enemyValue)
    {
        Debug.Log($"[AddPoint()] Start Score: {score}");
        score += enemyValue;
        Debug.Log($"Score: {score}");
    }
}


