using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI ScorePoint;
    public TextMeshProUGUI FinalScore;
    private int points;

    private void Start()
    {
        points = Constants.ZERO;
        ScorePoint.text = points.ToString();
        FinalScore.text = points.ToString();
    }

    public void SetScore()
    {
        points++;
        ScorePoint.text = points.ToString();
        FinalScore.text = points.ToString();
    }
}
