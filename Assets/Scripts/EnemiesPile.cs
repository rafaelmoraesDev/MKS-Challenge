using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPile : MonoBehaviour
{
    public GameObject prefab;
    public int Amount;
    private Stack<GameObject> enemiesPile;
    private string[] enemies = new string[] { "Chaser", "Shooter" };
    private void Start()
    {
        enemiesPile = new Stack<GameObject>();
        GenerateEnemies();
    }

    private void GenerateEnemies()
    {
        for (int i = Constants.ZERO; i < Amount; i++)
        {
            var enemy = GameObject.Instantiate(prefab, this.transform);
            var pile = enemy.GetComponent<PileObject>();
            pile.SetEnemiesPile(this);
            EnemyControl enemyControl = enemy.GetComponent<EnemyControl>();
            enemy.SetActive(false);
            SetRandomCharacter();
            enemyControl.SetEnemyStats();
            enemiesPile.Push(enemy);
        }
    }

    public GameObject SetEnemy()
    {
        var enemy = enemiesPile.Pop();
        EnemyControl enemyControl = enemy.GetComponent<EnemyControl>();
        enemy.SetActive(true);
        enemyControl.SetEnemyStats();
        return enemy;
    }

    public void GetBackEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemiesPile.Push(enemy);
    }
    private void SetRandomCharacter()
    {
        EnemyControl enemyControl = prefab.GetComponent<EnemyControl>();
        int min = Constants.ZERO;
        int max = enemies.Length;
        int sort = Random.Range(min, max);
        enemyControl.EnemyKind = enemies[sort];
    }
    public bool IsEmptyPile()
    {
        return enemiesPile.Count > Constants.ZERO;
    }
}
