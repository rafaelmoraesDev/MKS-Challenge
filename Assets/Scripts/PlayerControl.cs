using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] ExitPointTripleCannon;
    public StatusCharacter StatusCharacter;
    public Sprite[] PlayerSprites;

    private ShootControl shootControl;
    private Rigidbody2D rb2D;
    private Vector2 direction;
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField] private float speed = Constants.MINIMUM_VALUE;

    private void Awake()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        playerSpriteRenderer.sprite = PlayerSprites[Constants.ZERO];
        rb2D = GetComponent<Rigidbody2D>();
        StatusCharacter = GetComponent<StatusCharacter>();
        shootControl = GetComponent<ShootControl>();
    }

    private void Update()
    {
        StatusCharacter.SetDeterioration(playerSpriteRenderer, PlayerSprites);
        if (StatusCharacter.Alive)
            ProcessMoveInputs(direction);
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void LateUpdate()
    {
        if (StatusCharacter.Alive)
        {
           
            if (Input.GetButtonDown("Fire1"))
                shootControl.SingleShoot(this.gameObject);

            if (Input.GetButtonDown("Fire2"))
                shootControl.TripleShoot(this.gameObject);
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
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, 3);
    //}

}
