using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float LifeTime = 2f;
    public float Speed = 10f;

    public Rigidbody2D rb2D;

    private void Start()
    {
        rb2D.velocity = transform.right * Speed;
    }
    private void Update()
    {
        StartCoroutine(SelfDestroy());
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

        if (statusCharacter != null)
        {
            statusCharacter.SetDamage();

            if (statusCharacter.Life <= Constants.MINIMUM_VALUE)
                Destroy(collision.gameObject);
        }

    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(LifeTime);
        DestroyCannonBall();
    }
}
