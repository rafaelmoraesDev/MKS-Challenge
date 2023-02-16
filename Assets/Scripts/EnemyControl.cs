using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public GameObject Player;
    public Sprite[] Sprites;
    public string EnemyKind;
    public SpriteRenderer EnemySpriteRenderer;
    public bool FriendlyFire;

    [SerializeField] private float speed = 0.7f;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float startTime;


    private ShootControl shootControl;
    private MoveCharacter moveCharacter;


    private void Awake()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        shootControl = GetComponent<ShootControl>();
        EnemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();

    }
    private void Start()
    {
        SetEnemyKind();
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        timeBetweenShoots = startTime;
    }
    private void Update()
    {
        timeBetweenShoots -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (!Player.Equals(null))
        {
            Vector3 direction = Player.transform.position - transform.position;
            float distance = Vector3.Distance(transform.position, Player.transform.position);
            moveCharacter.LookToPlayerRotation(direction);
            if (distance < 10 && EnemyKind.Equals("Shooter"))
            {
                if (timeBetweenShoots <= Constants.ZERO)
                {
                    shootControl.SingleShoot();
                    shootControl.CannonBallScript.SetOriginShoot(gameObject);
                    timeBetweenShoots = startTime;
                }
            }
            else if (distance < 5 && EnemyKind.Equals("Shooter"))
            {
                moveCharacter.StopMoving();

            }
            else
            {
                moveCharacter.ChaseMovement(direction, speed);
            }
        }

    }

    private void SetEnemyKind()
    {
        switch (EnemyKind)
        {
            case "Chaser":
                EnemySpriteRenderer.sprite = Sprites[0];
                shootControl.Cannon.SetActive(false);
                break;
            case "Shooter":
                EnemySpriteRenderer.sprite = Sprites[1];
                shootControl.enabled = !shootControl.enabled;
                break;

            default:
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag(Tags.Player))
        //    Player.GetComponent<PlayerControl>().StatusCharacter.SetDamage();
    }


}
