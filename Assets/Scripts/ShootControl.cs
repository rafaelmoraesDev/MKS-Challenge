using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootControl : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject CannonBall;
    public GameObject ExitPointSingleCannon;
    public GameObject[] ExitPointTripleCannon;
    public CannonBall CannonBallScript;

    public void SingleShoot(GameObject shooter)
    {
        SetPrefab(ExitPointSingleCannon, shooter);
    }

    public void TripleShoot(GameObject shooter)
    {
        foreach (GameObject exitPoint in ExitPointTripleCannon)
        {
            SetPrefab(exitPoint, shooter);
        }
    }

    private void SetPrefab(GameObject exitPoint, GameObject owner)
    {
        GameObject prefab = CannonBall;
        GameObject cannonBall = Instantiate(prefab, exitPoint.transform.position, exitPoint.transform.rotation);
        CannonBall cannonBallScript = cannonBall.GetComponent<CannonBall>();
        cannonBallScript.OriginalShooter = owner;
        CannonBallScript = cannonBallScript;
    }
}
