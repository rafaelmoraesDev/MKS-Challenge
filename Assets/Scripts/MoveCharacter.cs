using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private const float degrees = 90f;
    private Rigidbody2D rb2D;
    private string enemyKind;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        enemyKind = GetComponent<EnemyControl>().EnemyKind;
    }
    public void ChaseMovement(Vector3 direction, float speed)
    {
        //TODO: checar tipo de inimigo, se shooter parar numa distancia x do player e atirar
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed).normalized;
        
    }
    public void LookToPlayerRotation(Vector3 direction)
    {
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rot_z - degrees);
    }

    public void StopMoving()
    {
        rb2D.velocity = Vector3.zero;
    }
}
