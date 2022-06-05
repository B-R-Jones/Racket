using System.Collections;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    // Public properties
    public GameObject Ammo;
    public float FireRate;
    public float EngageDistance = 0;
    public bool TargetLockRequired = false;
    public bool CommenceFire = false;
    public AnimationClip FiringAnimation;
    public AnimationClip UpgradeAnimation;
    public bool BeamWeapon = false;

    // Private properties
    private bool AllowFire = true;
    private bool TargetFound = true;
    private bool WeaponsDeployed = false;

    public bool BeamOn = false;
    //private GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        if (EngageDistance > 0)
        {
            LookForTarget("Enemy");
        }
        //GetFireCommand();
    }

    // Update is called once per frame
    void Update()
    {
        GetFireCommand();
        if (EngageDistance > 0)
        {
            if (BeamWeapon)
            {
                //BeamTargetedFire();
            }
            else
            {
                AmmunitionTargetedFire();
            }
        }
        else
        {
            if (BeamWeapon)
            {
                BeamFire();
            }
            else
            {
                AmmunitionFire();
            }
        }
    }

    void BeamFire()
    {
        if (BeamOn == false)
        {
            AmmunitionFire();
        }
        if (CommenceFire == false)
        {
            ResetAnim();
        }
    }

    void AmmunitionFire()
    {
        if (CommenceFire)
        {
            LoadWeapons(FireRate);
        }
        if (CommenceFire == false)
        {
            ResetAnim();
        }
    }

    void AmmunitionTargetedFire()
    {
        if (CommenceFire && TargetFound)
        {
            LoadWeapons(FireRate);
        }
        if (CommenceFire == false)
        {
            ResetAnim();
        }
    }

    void ResetAnim()
    {
        if (WeaponsDeployed == true)
        {
            this.GetComponent<Animator>().StopPlayback();
            string thisWeapon = this.name;
            int SliceIndex = thisWeapon.Length - thisWeapon.IndexOf("(Clone)");
            string weaponAnim = thisWeapon.Substring(0, thisWeapon.Length - SliceIndex);
            this.GetComponent<Animator>().Play(weaponAnim + " Idle");
        }
    }
        void DeployWeapons()
    {
        WeaponsDeployed = true;
        EnableFire();
    }

    void GetFireCommand()
    {
        Debug.Log($"GFC {this.name} 000: {AllowFire}");
        Debug.Log($"GFC {this.name} 001: {this.GetComponentInParent<PlayerController>().name}");
        Debug.Log($"GFC {this.name} 002: {this.GetComponentInParent<PlayerController>().CommenceFiring}");
        if (AllowFire == true)
        {
            CommenceFire = this.GetComponentInParent<PlayerController>().CommenceFiring;
        }
    }

    void EnableFire()
    {
        AllowFire = true;
    }

    void DisableFire()
    {
        AllowFire = false;
    }
    void LoadWeapons(float fireRate)
    {
        if (BeamWeapon)
        {
            BeamLoad(fireRate);
        }
        else
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<Animator>().Play(FiringAnimation.name);
            StartCoroutine(PrepareToFire(fireRate));
        }
    }

    void BeamLoad(float fireRate)
    {
        if (BeamOn == false)
        {
            this.GetComponent<Animator>().Play(FiringAnimation.name);
            StartCoroutine(PrepareToFire(fireRate));
            BeamOn = true;
        }
    }

    IEnumerator PrepareToFire(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        Fire();
    }

    void Fire()
    {
        Transform[] Parts = gameObject.GetComponentsInChildren<Transform>();
        foreach (Transform LaunchPoint in Parts)
        {
            if (LaunchPoint.CompareTag("LaunchPoint"))
            {
                FireAmmo(LaunchPoint.gameObject, LaunchPoint.transform.rotation);
            }
        }
    }

    void FireAmmo(GameObject launchPoint, Quaternion launchAngle)
    {
        Instantiate(Ammo, launchPoint.transform);
    }

    void LookForTarget(string TargetName)
    {
        if (GameObject.FindGameObjectWithTag(TargetName) != null)
        {
            TargetFound = true;
        }
        else
        {
            TargetFound = false;
        }
    }
}
