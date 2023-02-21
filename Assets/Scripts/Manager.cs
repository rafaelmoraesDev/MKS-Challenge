using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Manager : MonoBehaviour
{
    public GameData GameData;
    public Slider SliderTime;
    public Slider SliderTimeSpawn;
    public TextMeshProUGUI TextMeshTime;
    public TextMeshProUGUI TextMeshTimeSpawn;

    private void Awake()
    {
        TextMeshTime.text = SliderTime.minValue.ToString();
        TextMeshTimeSpawn.text = SliderTimeSpawn.minValue.ToString();
        SetData();
    }
    private void Update()
    {
        TextMeshTime.text = SliderTime.value.ToString();
        TextMeshTimeSpawn.text = SliderTimeSpawn.value.ToString();
    }

    private void SetData()
    {
        if (GameData.Minutes <= 0 && GameData.SpawnTime <= 0)
        {
            GameData.Minutes = SliderTime.minValue;
            GameData.SpawnTime = SliderTimeSpawn.minValue;
        }
    }
}
