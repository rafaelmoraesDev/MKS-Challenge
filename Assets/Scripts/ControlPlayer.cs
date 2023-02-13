using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{

    public GameObject CannonBall;
    public GameObject ExitPointCannon;
    static float ZERO = 0f;

    private StatusCharacter statusCharacter;

    private Rigidbody2D rgdBody2D;

    [SerializeField]
    private float speed = 1f;

    private void Awake()
    {
        rgdBody2D = GetComponent<Rigidbody2D>();
        statusCharacter = GetComponent<StatusCharacter>();
    }
    private void LateUpdate()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(CannonBall, ExitPointCannon.transform.position , ExitPointCannon.transform.rotation);
        }
    }

    private void MovePlayer()
    {
        float axysY = Input.GetAxis("Vertical");
        float axysZ = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(ZERO, axysY, ZERO);

        if (axysY > ZERO)
            transform.Translate(direction * speed * Time.deltaTime);

        if (!axysZ.Equals(0))
            transform.Rotate(new Vector3(ZERO, ZERO, axysZ));
    }

    public void SetDamage()
    {
        statusCharacter.Life -= statusCharacter.Damage;

        if (statusCharacter.Life <= ZERO)
        {
            Debug.Log("Die");
        }
    }

}
