using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public string EnemyKind;
    public GameObject Player;
    public Sprite[] EnemySpritesChaser;
    public Sprite[] EnemySpritesShooter;
    public SpriteRenderer EnemySpriteRenderer;
    public StatusCharacter StatusCharacter;

    [SerializeField] private float speed;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float startTime;


    private ShootControl shootControl;
    private MoveCharacter moveCharacter;
    private float distance;
    private Vector3 direction;


    private void Awake()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        shootControl = GetComponent<ShootControl>();
        EnemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StatusCharacter = GetComponent<StatusCharacter>();
    }
    private void Start()
    {
        SetEnemyKind();
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        timeBetweenShoots = startTime;
    }
    private void Update()
    {
        if (EnemyKind.Equals("Shooter"))
        {
            timeBetweenShoots -= Time.deltaTime;
            StatusCharacter.SetDeterioration(EnemySpriteRenderer, EnemySpritesShooter);
        }
        else
            StatusCharacter.SetDeterioration(EnemySpriteRenderer, EnemySpritesChaser);

        distance = Vector3.Distance(transform.position, Player.transform.position);
    }

    private void FixedUpdate()
    {
        if (!Player.Equals(null))
        {
            direction = Player.transform.position - transform.position;
            moveCharacter.LookToPlayerRotation(direction);

            if (distance < 5)
            {
                if (EnemyKind.Equals("Shooter"))
                {
                    moveCharacter.StopMoving();
                    if (timeBetweenShoots <= Constants.ZERO)
                    {
                        shootControl.SingleShoot(gameObject);
                        timeBetweenShoots = startTime;
                    }
                }
                if (EnemyKind.Equals("Chaser"))
                    moveCharacter.Kamikaze(direction, speed);
            }
            else
                moveCharacter.ChaseMovement(direction, speed);
        }
    }

    private void SetEnemyKind()
    {
        switch (EnemyKind)
        {
            case "Chaser":
                EnemySpriteRenderer.sprite = EnemySpritesChaser[0];
                shootControl.Cannon.SetActive(false);
                StatusCharacter.Life = 100;
                StatusCharacter.Damage = Random.Range(StatusCharacter.Life / 2, StatusCharacter.Life);
                speed = 1f;
                break;
            case "Shooter":
                EnemySpriteRenderer.sprite = EnemySpritesShooter[0];
                shootControl.enabled = !shootControl.enabled;
                StatusCharacter.Life = 100;
                StatusCharacter.Damage = Random.Range(StatusCharacter.Life / 4, StatusCharacter.Life / 2);
                speed = 0.7f;
                break;

            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Player) && EnemyKind.Contains("Chaser"))
        {
            Player.GetComponent<PlayerControl>().StatusCharacter.SetDamage();
            Destroy(gameObject);

        }
    }

}
