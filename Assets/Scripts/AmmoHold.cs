using UnityEngine;

public class AmmoHold : MonoBehaviour
{
    public GameObject ItemHeld;
    // Start is called before the first frame update
    void Start()
    {
        if (ItemHeld)
        {
            //Debug.Log($"Ammo Name: { AmmoHeld.name}");
        }
        else
        {
            Debug.Log($"No item held in {this.name}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
