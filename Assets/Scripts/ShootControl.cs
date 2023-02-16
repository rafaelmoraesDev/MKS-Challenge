using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject CannonBall;
    public GameObject ExitPointSingleCannon;
    public GameObject[] TripleCannon;

    public CannonBall CannonBallScript;

    private void Awake()
    {
        CannonBallScript = CannonBall.GetComponent<CannonBall>();
    }

    public void SingleShoot()
    {
        GameObject cannonBall = CannonBall;
        CannonBall cannonBallScript = cannonBall.GetComponent<CannonBall>();

        Instantiate(cannonBall, ExitPointSingleCannon.transform.position, ExitPointSingleCannon.transform.rotation);
    }

    public void TripleShoot()
    {

    }
}
