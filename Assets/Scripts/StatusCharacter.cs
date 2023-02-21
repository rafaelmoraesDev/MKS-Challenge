using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCharacter : MonoBehaviour
{
    public int Life;
    public int Damage;
    public bool Alive;
    public GameObject GameOverPanel;

    private GameObject score;
    private Score scoreScript;

    private void Awake()
    {
        Alive = !Alive;
        score = GameObject.FindGameObjectWithTag(Tags.Canvas);
        scoreScript = score.GetComponent<Score>();
    }

    public void SetDamage()
    {
        Life -= Damage;

        if (Life <= Constants.ZERO && Alive)
        {
            Alive = !Alive;

            if (gameObject.CompareTag(Tags.Enemy))
            {
                this.gameObject.GetComponent<EnemyControl>().AnimateExplosionAndDestroy();
                this.gameObject.GetComponent<PileObject>().SendBackToPile();
                
                scoreScript.SetScore();

            }

            if (gameObject.CompareTag(Tags.Player))
            {
                GameOverPanel.SetActive(true);
                Time.timeScale = Constants.ZERO;
            }

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
