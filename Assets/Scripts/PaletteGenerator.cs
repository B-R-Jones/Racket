using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaletteGenerator : MonoBehaviour
{
    private Texture2D ColorTexture;
    public Texture2DArray COPalette;
    public Texture2DArray GSPalette;

    public Texture2D OverrideSprite;
    public bool OverrideBool;

    // Start is called before the first frame update
    void Start()
    {
        ColorTexture = new Texture2D(255, 1, TextureFormat.RGBA32, false, false);
        ColorTexture.filterMode = FilterMode.Point;
        UpdatePalettes();
        OverrideBool = false;
    }

    // Update is called once per frame.
    void Update()
    {
        
    }

    public void UpdatePalettes()
    {
        Color colorFromCOPalette;
        Color greyFromGSPalette;
        int greyscaleRedValue;



        if (OverrideSprite) QuickPalette();

        for(int i = 0; i < ColorTexture.width; i++)
        {
            ColorTexture.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
        }

        for (int i = 0; i < GSPalette.depth; i++)
        {
            colorFromCOPalette = COPalette.GetPixels(i)[0];
            greyFromGSPalette = GSPalette.GetPixels(i)[0];
            Color32 g = greyFromGSPalette;
            greyscaleRedValue = g.r;
            ColorTexture.SetPixel(greyscaleRedValue, 0, colorFromCOPalette);
        }

        ColorTexture.Apply();
        this.GetComponent<SpriteRenderer>().material.SetTexture("_Palette", ColorTexture);
    }

    private void QuickPalette()
    {
        Texture2D SpriteToPalette = OverrideSprite;
        Color[] SpriteColors = SpriteToPalette.GetPixels();
        List<Color> _SpriteColors = new List<Color>();

        Color CurrentColor = new Color(0f, 0f, 0f, 0f);

        //This will build our list of colors for us - should all be good
        for (int i = 0; i < SpriteColors.Length; i++)
        {
            CurrentColor = SpriteColors[i];

            if (!(CurrentColor.a == 0))
            {
                if (!(_SpriteColors.IndexOf(CurrentColor) > -1)) _SpriteColors.Add(CurrentColor);
            }
        }

        //This just sets up our color and array textures - again, should be all good here
        Texture2D PixelColor = new Texture2D(1, 1, TextureFormat.RGBA32, false, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };
        Texture2DArray ColorPalette = new Texture2DArray(1, 1, _SpriteColors.Count, TextureFormat.RGBA32, false, false)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        //This is where the work is
        //We're cycling through our list of colors here
        //We can use another list to keep track of which colors we've used so we don't repeat
        List<Color> ColorsUsed = new List<Color>();
        Color CheckColor = new Color(1f, 1f, 1f, 1f);
        ColorsUsed.Add(CheckColor);
        for (int i = 0; i < _SpriteColors.Count; i++)
        {
            int _SpriteColorsMax = _SpriteColors.Count;
            int _SpriteColorsRandomColor = Random.Range(0, _SpriteColorsMax);
            //We can use a random number between 0 and the Ubound of _SpriteColors to pull a random color
            //We set our color texture to the color from the array
            PixelColor.SetPixel(1, 1, _SpriteColors[_SpriteColorsRandomColor]);
            while ((ColorsUsed.IndexOf(PixelColor.GetPixel(1, 1)) > -0))
            {
                _SpriteColorsRandomColor = Random.Range(0, _SpriteColorsMax);
                PixelColor.SetPixel(1, 1, _SpriteColors[_SpriteColorsRandomColor]);
            }
            ColorsUsed.Add(PixelColor.GetPixel(1, 1));

            int ColorPaletteMax = ColorPalette.depth;
            int ColorPaletteRandomColor = Random.Range(0, ColorPaletteMax);
            //We can use a random number between 0 and the Ubound of ColorPalette to set a random color
            //Here we set our color palette
            ColorPalette.SetPixels(PixelColor.GetPixels(), ColorPaletteRandomColor);
        }
        ColorsUsed.Remove(CheckColor);
        for (int i = 0; i < ColorsUsed.Count; i++)
        {
            //Debug.Log($"COL000: {ColorsUsed[i]}");
        }
        ColorPalette.Apply();
        COPalette = ColorPalette;
        OverrideBool = true;
        //UpdatePalettes();
    }

    public void ResetOverride()
    {
        if (OverrideBool == true) OverrideBool = false; 
    }
}
