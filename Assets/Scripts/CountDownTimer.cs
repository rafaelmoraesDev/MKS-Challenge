using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI Timer;
    public GameObject GameData;
    private GameData gameData;
    private float currentTime = Constants.ZERO;
    private float startTime;
    private float timeReference = 60;

    private void Awake()
    {
        
        GameData = GameObject.FindGameObjectWithTag(Tags.GameData);
        gameData = GameData.GetComponent<GameData>();
        startTime = gameData.Minutes * timeReference;
        Time.timeScale = Constants.MINIMUM_VALUE;
    }
    private void Start()
    {
        currentTime = startTime;
    }
    private void Update()
    {
        CurrentTimeStatus();

    }

    private void CurrentTimeStatus()
    {
        currentTime -= Time.deltaTime;
        DisplayTime(currentTime);
        if (currentTime <= Constants.ZERO)
        {
            currentTime = Constants.ZERO;
            SceneManager sceneManager = gameObject.GetComponent<SceneManager>();
            sceneManager.GameOver();
        }
    }

    private void DisplayTime(float time)
    {
        if (time <= Constants.ZERO)
            time = Constants.ZERO;

        float minutes = Mathf.FloorToInt(time / timeReference);
        float seconds = Mathf.FloorToInt(time % timeReference);
        Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


}
