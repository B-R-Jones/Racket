using System.Collections.Generic;
using UnityEngine;

public class PickupTest : MonoBehaviour
{
    public List<GameObject> UpgradeWeaponsList = new List<GameObject>();
    public AnimationClip UpgradeAnimation;

    private GameObject Receiver;
    private int PlayerWeapons;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        Receiver = collision.gameObject;
        if (Receiver.transform.CompareTag("Player"))
        {
            PlayerWeapons = Receiver.GetComponent<PlayerController>().WeaponCount;
            Receiver.GetComponent<PlayerController>().RemoveWeapons();
            UpgradeWeapons(collision.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    void UpgradeWeapons(GameObject Player)
    {
        int i = 1;
        foreach (GameObject weaponSlot in UpgradeWeaponsList)
        {
            if (weaponSlot)
            {
                GameObject NewWeapon = Instantiate(weaponSlot, Player.transform);
                Vector3 NewPos = NewWeapon.GetComponent<WeaponOffsets>().Offset;
                Vector3 NewScale = NewWeapon.transform.localScale;
                Debug.Log($"Instantiated Transform:{NewWeapon.transform.localPosition}");
                if (i % 2 < 1)
                {
                    NewScale.x = -1 * NewScale.x;
                    NewPos.x = Mathf.Abs(NewPos.x);
                }
                NewWeapon.transform.localPosition = NewPos;
                NewWeapon.transform.localScale = NewScale;
            }
            i++;
        }
    }
}
