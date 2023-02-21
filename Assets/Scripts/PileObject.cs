using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileObject : MonoBehaviour
{
    private EnemiesPile pile;

    public void SendBackToPile()
    {
        pile.GetBackEnemy(gameObject);
    }

    public void SetEnemiesPile(EnemiesPile enemiesPile)
    {
        pile = enemiesPile;
    }
}
