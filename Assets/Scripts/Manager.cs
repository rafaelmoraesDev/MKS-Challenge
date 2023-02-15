using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void GameOver()
    {
        if (Player.GetComponent<StatusCharacter>().Alive)
            Time.timeScale = Constants.ZERO;
    }
}
