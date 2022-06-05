using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMatcher : MonoBehaviour
{
    private GameObject ParentShip;
    private Texture2D MyPalette;
    private Texture2D ShipPalette;

    // Start is called before the first frame update
    void Start()
    {
        ParentShip = this.transform.parent.gameObject;
        ShipPalette = (Texture2D)ParentShip.GetComponent<SpriteRenderer>().material.GetTexture("_Palette");
        MyPalette = (Texture2D)this.GetComponent<SpriteRenderer>().material.GetTexture("_Palette");
        MyPalette.SetPixel(0, 1, ShipPalette.GetPixel(0, 1));
        MyPalette.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
