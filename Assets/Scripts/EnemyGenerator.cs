using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float timeGenerateNext;

    private float counter = Constants.ZERO;


    private void Update()
    {
        counter += Time.deltaTime;
        if (counter >= timeGenerateNext && !Time.timeScale.Equals(Constants.ZERO))
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            counter = Constants.ZERO;

        }
    }
}
