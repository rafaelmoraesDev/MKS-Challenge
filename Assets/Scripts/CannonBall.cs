using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float LifeTime = 0.1f;
    public float Speed = 10f;
    public GameObject OriginalShooter;

    public Rigidbody2D rb2D;

    private void Start()
    {
        Invoke("DestroyCannonBall", LifeTime);
        rb2D.velocity = transform.right * Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckHitTarget(collision);
        DestroyCannonBall();
    }
 
    private void DestroyCannonBall()
    {
        Destroy(this.gameObject);
    }

    private void CheckHitTarget(Collider2D collision)
    {
        StatusCharacter statusCharacter = collision.gameObject.GetComponent<StatusCharacter>();
        EnemyControl enemyControl = collision.gameObject.GetComponent<EnemyControl>();
        if (statusCharacter != null)
        {
            if (!collision.CompareTag(OriginalShooter.tag))
                statusCharacter.SetDamage();

            if (statusCharacter.Life <= Constants.MINIMUM_VALUE && !collision.CompareTag(Tags.Player))
                enemyControl.AnimateExplosionAndDestroy();
        }

    }
}
