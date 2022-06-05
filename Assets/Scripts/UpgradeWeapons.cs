using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeapons : MonoBehaviour
{

    // Upgrade variables
    public List<GameObject> SetUpgradeWeapons = new List<GameObject>();
    public AnimationClip UpgradeAnimation;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpgradeShip()
    {
        GameObject Receiver = GameObject.FindGameObjectWithTag("Player");
        GameObject NewWeapon = Instantiate(SetUpgradeWeapons[0]);
        NewWeapon.transform.SetParent(Receiver.transform);
        NewWeapon.transform.SetPositionAndRotation(Receiver.transform.Find("Weapon1").transform.position,
                                                   Receiver.transform.rotation);
        NewWeapon.transform.localScale = Receiver.transform.Find("Weapon1").transform.localScale;
        NewWeapon = Instantiate(SetUpgradeWeapons[0]);
        NewWeapon.transform.SetParent(Receiver.transform);
        NewWeapon.transform.SetPositionAndRotation(Receiver.transform.Find("Weapon2").transform.position,
                                                   Receiver.transform.rotation);
        NewWeapon.transform.localScale = Receiver.transform.Find("Weapon2").transform.localScale;
        Debug.Log("Applied successfully");
    }
}
