using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer : MonoBehaviour
{
    public float Velocity = 0f;
    void Update()
    {
        float axysY = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(0f, axysY, 0f);

        transform.Translate(direction * Velocity * Time.deltaTime);
        //transform.RotateAround();
    }
}
