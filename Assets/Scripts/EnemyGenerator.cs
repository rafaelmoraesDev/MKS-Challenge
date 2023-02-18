using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{

    public EnemiesPile EnemiesPile;
    //[SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeGenerateNext;
    [SerializeField] private int radiusArea;
    [SerializeField] private int amountEnemies;



    private float counter = Constants.ZERO;

    private string[] enemies = new string[] { "Chaser", "Shooter" };

    private GameObject player;

    private GameObject enemy;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        counter += Time.deltaTime;
        if (counter >= timeGenerateNext && !Time.timeScale.Equals(Constants.ZERO) && distance > 10)
        {
            StartCoroutine(GenerateEnemiesShips());
            counter = Constants.ZERO;
        }
    }

    private IEnumerator GenerateEnemiesShips()
    {
        if (EnemiesPile.IsEmptyPile())
        {
            Vector3 randomPosition = SetRandonPosition();
            Collider[] colliders = Physics.OverlapSphere(randomPosition, Constants.MINIMUM_VALUE);
            while (colliders.Length > Constants.ZERO)
            {
                randomPosition = SetRandonPosition();
                yield return null;
            }
            
            enemy = EnemiesPile.SetEnemy();
            enemy.SetActive(true);
            enemy.transform.position = randomPosition;
            SetRandomCharacter();
        }

    }

    private void SetRandomCharacter()
    {
        int min = 0;
        int max = enemies.Length;
        int sort = Random.Range(min, max);
        EnemyControl enemyControl = enemy.GetComponent<EnemyControl>();
        enemyControl.EnemyKind = enemies[sort];
    }
    private Vector3 SetRandonPosition()
    {
        Vector3 position = Random.insideUnitSphere * radiusArea;
        position += transform.position;
        position.z = transform.position.z;

        return position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusArea);
    }
}
