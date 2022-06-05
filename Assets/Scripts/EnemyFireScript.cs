using System.Collections;
using UnityEngine;

public class EnemyFireScript : MonoBehaviour
{
    // Public properties
    public float SetFireRate = 0.0f;
    public float SetEngageDistance = 0.0f;
    public GameObject Missile;

    // Private properties
    private bool PlayerFound = true;
    private bool Firing = false;
    private GameObject Player;
    Vector3 launchPoint;
    Quaternion launchAngle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        if (PlayerFound == true & Firing == false)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) < SetEngageDistance)
            {
                LoadWeapons(SetFireRate);
            }
        }
    }
    void LoadWeapons(float fireRate)
    {
        Firing = true;
        StartCoroutine(PrepareToFire(fireRate));
    }

    IEnumerator PrepareToFire(float fireRate)
    {
        while (PlayerFound)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
        }
        Firing = false;
    }

    void Fire()
    {
        LookForPlayer();
        if (PlayerFound) FireMissile();
    }

    void FireMissile()
    {
        launchPoint = transform.position + (transform.up * 0.75f);
        launchAngle = transform.rotation;
        Instantiate(Missile, launchPoint, launchAngle);
    }

    void LookForPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            PlayerFound = true;
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        else
        {
            PlayerFound = false;
        }
    }
}
