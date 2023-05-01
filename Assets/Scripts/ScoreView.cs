using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScore;

    private void OnEnable()
    {
        GameManager.OnAddingScore += UpdatePlayerScore;
    }

    private void OnDisable()
    {
        GameManager.OnAddingScore -= UpdatePlayerScore;
    }
    private void UpdatePlayerScore(int score)
    {
        playerScore.text = score.ToString();
    }
}
