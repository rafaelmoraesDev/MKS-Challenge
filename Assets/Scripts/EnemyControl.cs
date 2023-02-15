using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject Player;
    public Sprite[] Sprites;

    private enum EnemyKind { Chaser, Shooter };
    private EnemyKind enemyKind;

    [SerializeField] private float speed = 0.7f;

    private ShootControl controlShoot;
    private MoveCharacter moveCharacter;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        enemyKind = EnemyKind.Chaser;
        moveCharacter = GetComponent<MoveCharacter>();
        controlShoot = GetComponent<ShootControl>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (enemyKind == EnemyKind.Shooter)
        {
            controlShoot.enabled = !controlShoot.enabled;
            spriteRenderer.sprite = Sprites[1];

        }
        else
        {
            spriteRenderer.sprite = Sprites[0];
        }
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
    }

    private void FixedUpdate()
    {
        if (!Player.Equals(null))
        {
            Vector3 direction = Player.transform.position - transform.position;
            moveCharacter.ChaseMovement(direction, speed);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player))
            Player.GetComponent<PlayerControl>().StatusCharacter.SetDamage();
    }

}
