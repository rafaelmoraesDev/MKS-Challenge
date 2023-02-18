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
            Alive = !Alive;

            if (gameObject.CompareTag(Tags.Enemy))
                this.gameObject.GetComponent<EnemyControl>().AnimateExplosionAndDestroy();

            if (gameObject.CompareTag(Tags.Player))
                Time.timeScale = Constants.ZERO;

        }
    }
    public void SetDeterioration(SpriteRenderer spriteRenderer, Sprite[] sprites)
    {
        switch (Life)
        {
            case int n when n <= 0:
                spriteRenderer.sprite = sprites[3];
                break;
            case int n when n < 25:
                spriteRenderer.sprite = sprites[2];
                break;
            case int n when n < 60:
                spriteRenderer.sprite = sprites[1];
                break;
            case int n when n < 100:
                spriteRenderer.sprite = sprites[0];
                break;

        }
    }
}
