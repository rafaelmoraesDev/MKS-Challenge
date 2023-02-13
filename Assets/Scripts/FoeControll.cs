using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeControll : MonoBehaviour
{
    public GameObject Player;
    private enum EnemyKind { Chaser, Shooter };
    private Rigidbody2D rgdBody2D;
    [SerializeField]
    private float speed = 1f;
    [SerializeField]
    private float speedRotation = 1f;
    private EnemyKind enemyKind;
    private ControlShoot controlShoot;

    public float rotationModifier;

    private void Awake()
    {
        enemyKind = EnemyKind.Shooter;
        controlShoot = GetComponent<ControlShoot>();
        Player = GameObject.FindGameObjectWithTag(Tags.Player);
        rgdBody2D = GetComponent<Rigidbody2D>();

        if (enemyKind.Equals(EnemyKind.Shooter))
            controlShoot.enabled = !controlShoot.enabled;
    }

    private void FixedUpdate()
    {

        Vector3 direction = Player.transform.position - transform.position;
        rgdBody2D.MovePosition(rgdBody2D.position + (Vector2)direction * speed * Time.deltaTime);
        LookToPlayer();
    }

    private void LookToPlayer()
    {
        Vector3 direction = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - rotationModifier;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speedRotation);
    }

}
