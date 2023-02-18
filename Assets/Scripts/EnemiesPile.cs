using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPile : MonoBehaviour
{
    public GameObject prefab;
    public int Amount;
    private Stack<GameObject> enemies;
    private void Start()
    {
        enemies = new Stack<GameObject>();
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = 0; i < Amount; i++)
        {
            var enemy = GameObject.Instantiate(prefab, this.transform);
            enemy.SetActive(false);
            enemies.Push(enemy);
        }
    }

    public GameObject SetEnemy()
    {
        var enemy = enemies.Pop();
        enemy.SetActive(true);
        return enemy;
    }

    public void GetBackEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemies.Push(enemy);
    }

    public bool IsEmptyPile()
    {
        return enemies.Count > Constants.ZERO;
    }
}
