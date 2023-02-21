using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public float Minutes;
    public float SpawnTime;
    public bool Music;
    private void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }

    public void SetGameTime(float time)
    {
        Minutes = time;
    }

    public void SetSpawnTime(float time)
    {
        SpawnTime = time;
    }

}
