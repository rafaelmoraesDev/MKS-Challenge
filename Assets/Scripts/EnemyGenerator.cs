using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeGenerateNext;
    [SerializeField] private int radiusArea;



    //TODO:Colocar todos os inimigos numa lista para ativar e desativar ao inves de destroy
    //private List<GameObject> Enemies = new List<GameObject>();

    private float counter = Constants.ZERO;

    private string[] enemies = new string[] { "Chaser", "Shooter" };


    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= timeGenerateNext && !Time.timeScale.Equals(Constants.ZERO))
        {
            StartCoroutine(GenerateEnemiesShips());
            counter = Constants.ZERO;
        }
    }

    private IEnumerator GenerateEnemiesShips()
    {
        Vector3 randomPosition = SetRandonPosition();
        Collider[] colliders = Physics.OverlapSphere(randomPosition, Constants.MINIMUM_VALUE);
        while (colliders.Length > Constants.ZERO)
        {
            randomPosition = SetRandonPosition();
            yield return null;
        }
        SetRandomCharacter();
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
    }

    private void SetRandomCharacter()
    {
        int min = 0;
        int max = enemies.Length;
        int sort = Random.Range(min, max);
        EnemyControl enemyControl = enemyPrefab.GetComponent<EnemyControl>();
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
