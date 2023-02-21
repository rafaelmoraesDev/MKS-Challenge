using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public EnemiesPile EnemiesPile;

    [SerializeField] private float timeGenerateNext;
    [SerializeField] private int radiusArea;
    [SerializeField] private int amountEnemies;

    private GameObject gameData;
    private float counter = Constants.ZERO;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        gameData = GameObject.FindGameObjectWithTag(Tags.GameData);
        GameData gameDataScript = gameData.GetComponent<GameData>();
        timeGenerateNext = gameDataScript.SpawnTime;
    }
    
    private void Update()
    {
        SetupShips();
    }

    private void SetupShips()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        counter += Time.deltaTime;
        float tolerance = 8;

        if (counter >= timeGenerateNext && !Time.timeScale.Equals(Constants.ZERO) && distance > tolerance)
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

            var enemy = EnemiesPile.SetEnemy();
            enemy.transform.position = randomPosition;
        }

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