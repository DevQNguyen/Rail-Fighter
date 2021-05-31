using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text scoreText;
    int score;

    private void Start()
    {
        // Get TMP_text component attached to this gameObject
        scoreText = GetComponent<TMP_Text>();
        if (scoreText == null)
        {
            Debug.Log("TextMeshPro Component Not Found.");
        }
        scoreText.text = "Start";
    }

    public void AddScore(int enemyValue)
    {
        score += enemyValue;
        scoreText.text = $"Score: {score}";
    }
}


