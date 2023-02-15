using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject CannonBall;
    public GameObject ExitPointSingleCannon;
    public GameObject[] ExitPointTripleCannon;

    public StatusCharacter StatusCharacter;

    private Rigidbody2D rb2D;

    private Vector2 direction;

    [SerializeField] private float speed = Constants.MINIMUM_VALUE;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        StatusCharacter = GetComponent<StatusCharacter>();
    }

    private void Update()
    {
        ProcessInputs();
    }
    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void LateUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
            Instantiate(CannonBall, ExitPointSingleCannon.transform.position, ExitPointSingleCannon.transform.rotation);

        if (Input.GetButtonDown("Fire2"))
        {
            foreach (GameObject exitPoint in ExitPointTripleCannon)
            {
                Instantiate(CannonBall, exitPoint.transform.position, exitPoint.transform.rotation);
            }
        }
    }

    private void MovePlayer()
    {
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
    private void ProcessInputs()
    {
        float axysX = Input.GetAxisRaw("Horizontal");
        float axysY = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(Constants.ZERO, axysY, Constants.ZERO).normalized;

        if (axysY > Constants.ZERO)
            transform.Translate(direction * speed * Time.deltaTime);

        if (!axysX.Equals(Constants.ZERO))
            transform.Rotate(new Vector3(Constants.ZERO, Constants.ZERO, -axysX));
    }

}
