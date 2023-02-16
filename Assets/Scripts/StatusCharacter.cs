using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCharacter : MonoBehaviour
{
    public int Life;
    public int Damage;
    public bool Alive;
    private void Awake()
    {
        Alive = !Alive;
    }

    public void SetDamage()
    {
        Life -= Damage;

        if (Life <= Constants.ZERO && Alive)
        {
            Debug.Log("Die");
            Alive = !Alive;
            if (gameObject.CompareTag(Tags.Player) && !Alive)
                Time.timeScale = Constants.ZERO;
            else
                Destroy(gameObject);
        }
    }



}
