using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComboScript : MonoBehaviour
{
    private GameObject Ammo1;
    private GameObject Ammo2;
    public GameObject ComboAmmo;

    // Start is called before the first frame update
    void Start()
    {
        Ammo1 = this.transform.Find("AmmoHold1").gameObject.GetComponent<AmmoHold>().ItemHeld;
        Ammo2 = this.transform.Find("AmmoHold2").gameObject.GetComponent<AmmoHold>().ItemHeld;
        ComboAmmo = Ammo1;
        Texture2D ComboTex = new Texture2D(Ammo2.GetComponent<SpriteRenderer>().sprite.texture.width,
                                           Ammo2.GetComponent<SpriteRenderer>().sprite.texture.height,
                                           TextureFormat.RGBA32, false, false);
        ComboTex.filterMode = FilterMode.Point;
        ComboTex = Ammo2.GetComponent<SpriteRenderer>().sprite.texture;
        ComboTex.SetPixels(Ammo2.GetComponent<SpriteRenderer>().sprite.texture.GetPixels());
        ComboAmmo.GetComponent<PaletteGenerator>().COPalette = Ammo1.GetComponent<PaletteGenerator>().COPalette;
        ComboAmmo.GetComponent<PaletteGenerator>().OverrideSprite = ComboTex;
        //ComboAmmo.GetComponent<PaletteGenerator>().UpdatePalettes();
        SetComboTransform();
    }

    void SetComboTransform()
    {
        GameObject _ComboAmmo = Instantiate(ComboAmmo, this.transform.Find("ComboItem").transform);
        _ComboAmmo.GetComponent<AmmoScript>().enabled = false;
        _ComboAmmo.GetComponent<SpriteRenderer>().sortingOrder = 1;
        _ComboAmmo.transform.localPosition = new Vector3(0f, 0f, 0f);
        _ComboAmmo.transform.localScale = new Vector3(4f, 4f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
