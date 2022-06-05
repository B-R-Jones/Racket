using UnityEngine;

public class ShipOutfitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpgradeWeapons(string outfitString)
    {
        char splice = ';';
        string[] outfitSettings = outfitString.Split(splice);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");

    }
}
