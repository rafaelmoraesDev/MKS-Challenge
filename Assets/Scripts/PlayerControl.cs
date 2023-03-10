using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] ExitPointTripleCannon;
    public StatusCharacter StatusCharacter;
    public Sprite[] PlayerSprites;
    public Animator Animator;
    public GameObject Cannon;
    public Animator[] AnimatorTriple;

    [SerializeField] private float speed = Constants.MINIMUM_VALUE;

    private ShootControl shootControl;
    private Rigidbody2D rb2D;
    private Vector2 direction;
    private SpriteRenderer playerSpriteRenderer;
    private AudioSource audioSource;


    private void Awake()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerSpriteRenderer.sprite = PlayerSprites[Constants.ZERO];
        rb2D = GetComponent<Rigidbody2D>();
        StatusCharacter = GetComponent<StatusCharacter>();
        shootControl = GetComponent<ShootControl>();
        Animator = GetComponentInChildren<Animator>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    private void Update()
    {
        StatusCharacter.SetDeterioration(playerSpriteRenderer, PlayerSprites);
        if (StatusCharacter.Alive)
            ProcessMoveInputs(direction);

        if (!StatusCharacter.Alive)
            Cannon.SetActive(false);

        AnimationCannonShoot();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void LateUpdate()
    {
        Shooting();
    }

    private void Shooting()
    {
        float pitchValue = 2;
        if (StatusCharacter.Alive)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                shootControl.SingleShoot(this.gameObject);
                audioSource.pitch = pitchValue;
                audioSource.Play();
            }

            if (Input.GetButtonDown("Fire2"))
            {
                shootControl.TripleShoot(this.gameObject);
                pitchValue = 1;
                audioSource.pitch = pitchValue;
                audioSource.Play();
            }
        }
    }

    private void AnimationCannonShoot()
    {
        if (Input.GetButtonDown("Fire1"))
            Animator.SetTrigger("Explode");

        if (Input.GetButtonDown("Fire2"))
        {
            foreach (Animator anim in AnimatorTriple)
            {
                anim.SetTrigger("Explode");
            }
        }
    }

    private void MovePlayer()
    {
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
    private void ProcessMoveInputs(Vector2 direction)
    {
        float axysX = Input.GetAxisRaw("Horizontal");
        float axysY = Input.GetAxisRaw("Vertical");

        direction = new Vector2(Constants.ZERO, axysY).normalized;

        if (axysY > Constants.ZERO)
            rb2D.MovePosition(rb2D.position + ((Vector2)rb2D.transform.up * speed * Time.fixedDeltaTime));

        if (!axysX.Equals(Constants.ZERO))
            rb2D.rotation -= axysX;
    }
}
