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
    public Animator Animator;

    [SerializeField] private float speed;
    [SerializeField] private float timeBetweenShoots;
    [SerializeField] private float startTime;

    private ShootControl shootControl;
    private MoveCharacter moveCharacter;
    private float distance;
    private Vector3 direction;
    private PlayerControl playerControl;
    private AudioSource shootSound;

    struct EnemyVar
    {
        public int fullLife;
        public int firstDivisor;
        public int secondDivisor;
    }
    private void Awake()
    {
        moveCharacter = GetComponent<MoveCharacter>();
        shootControl = GetComponent<ShootControl>();
        EnemySpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StatusCharacter = GetComponent<StatusCharacter>();
        Animator = GetComponentInChildren<Animator>();
        shootSound = Camera.main.GetComponent<AudioSource>();
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        timeBetweenShoots = startTime;
        playerControl = Player.GetComponent<PlayerControl>();
    }
    private void Update()
    {
        SetUpdateActions();
    }
    private void FixedUpdate()
    {
        MoveAndShootBehavior();
    }
    private void OnDisable()
    {
        Animator.ResetTrigger("Explode");
    }

    private void SetUpdateActions()
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


    private void MoveAndShootBehavior()
    {
        if (!Player.Equals(null))
        {
            direction = Player.transform.position - transform.position;
            moveCharacter.LookToPlayerRotation(direction);
            float distanceToShoot = 5;

            if (distance < distanceToShoot)
            {
                if (EnemyKind.Equals("Shooter"))
                {
                    moveCharacter.StopMoving();
                    if (timeBetweenShoots <= Constants.ZERO && playerControl.StatusCharacter.Alive)
                    {
                        float pitchValue = 3;
                        shootControl.SingleShoot(gameObject);
                        shootSound.pitch = pitchValue;
                        shootSound.Play();
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

    public void SetEnemyStats()
    {
        EnemyVar enemyVars = new EnemyVar()
        {
            fullLife = 100,
            firstDivisor = 2,
            secondDivisor = 4
        };

        switch (EnemyKind)
        {
            case "Chaser":
                EnemySpriteRenderer.sprite = EnemySpritesChaser[Constants.ZERO];
                shootControl.Cannon.SetActive(false);
                StatusCharacter.Alive = true;
                StatusCharacter.Life = enemyVars.fullLife;
                StatusCharacter.Damage = Random.Range(StatusCharacter.Life / enemyVars.firstDivisor, StatusCharacter.Life);
                speed = 1f;
                break;
            case "Shooter":
                EnemySpriteRenderer.sprite = EnemySpritesShooter[Constants.ZERO];
                shootControl.enabled = !shootControl.enabled;
                StatusCharacter.Alive = true;
                StatusCharacter.Life = enemyVars.fullLife;
                StatusCharacter.Damage = Random.Range(StatusCharacter.Life / enemyVars.secondDivisor,
                    StatusCharacter.Life / enemyVars.firstDivisor);
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
            AnimateExplosionAndDestroy();
            Player.GetComponent<PlayerControl>().StatusCharacter.SetDamage();
        }
    }

    public void AnimateExplosionAndDestroy()
    {
        Animator.SetTrigger("Explode");
        float duration = 0.333f;
        PileObject pileObject = gameObject.GetComponent<PileObject>();
        if (pileObject.isActiveAndEnabled)
            StartCoroutine(SetBackToPile(duration, pileObject));
    }

    private IEnumerator SetBackToPile(float timer, PileObject pileObject)
    {
        yield return new WaitForSecondsRealtime(timer);
        pileObject.SendBackToPile();
    }
}
