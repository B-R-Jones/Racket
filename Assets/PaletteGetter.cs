using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using System.IO;

public class PaletteGetter : EditorWindow
{
    [MenuItem("Assets/Create/Palette", true)]
    private static bool PrepareToGenerateValidation()
    {
        return Selection.activeObject.GetType() == typeof(Texture2D);
    }

    [MenuItem("Assets/Create/Palette", false, 349)]
    public static void GeneratePalette()
    {
        Debug.Log($"PAL_GEN: {AssetDatabase.GetAssetPath(Selection.activeObject)};");
        Texture2DArray ColorPalette = GeneratePaletteArray();
        string OutputLocation = BuildPath(AssetDatabase.GetAssetPath(Selection.activeObject).ToString());
        AssetDatabase.CreateAsset(ColorPalette, OutputLocation);
        Debug.Log($"PAL_GEN: {OutputLocation}");
    }



    private static Texture2DArray GeneratePaletteArray()
    {
        Texture2D SpriteToPalette = (Texture2D)Selection.activeObject;
        Color[] SpriteColors = SpriteToPalette.GetPixels();
        List<Color> _SpriteColors = new List<Color>();

        for (int i = 0; i < SpriteColors.Length; i++)
        {
            Color CurrentColor = SpriteColors[i];

            if(!(CurrentColor.a == 0))
            {
                if (!(_SpriteColors.IndexOf(CurrentColor) > -1)) _SpriteColors.Add(CurrentColor);
            }
        }

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

        for (int i = 0; i < _SpriteColors.Count; i ++)
        {
            PixelColor.SetPixel(1, 1, _SpriteColors[i]);
            ColorPalette.SetPixels(PixelColor.GetPixels(), i);
        }
        ColorPalette.Apply();

        return ColorPalette;
    }

    private static string BuildPath(string SpritePath)
    {
        if (SpritePath == "")
        {
            SpritePath = "Assets";
        }
        else if (Path.GetExtension(SpritePath) != "")
        {
            SpritePath = SpritePath.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        int FileCount = 0;
        string SpriteName = Selection.activeObject.name + " Palette_0";
        string FileExtension = ".asset";

        if (File.Exists(SpritePath + SpriteName + FileExtension))
        {
            do
            {
                SpriteName = Selection.activeObject.name + " Palette_" + FileCount.ToString();
                FileCount++;
            } while (File.Exists(SpritePath + SpriteName + FileExtension));
        }
        SpritePath += SpriteName + FileExtension;
        return SpritePath;
    }
}