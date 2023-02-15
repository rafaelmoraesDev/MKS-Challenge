using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    private const float degrees = 90f;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    public void ChaseMovement(Vector3 direction, float speed)
    {
        rb2D.velocity = new Vector2(direction.x * speed, direction.y * speed).normalized;
        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Constants.ZERO, Constants.ZERO, rot_z - degrees);
    }
}
