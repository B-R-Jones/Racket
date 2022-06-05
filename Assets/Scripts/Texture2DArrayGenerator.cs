using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texture2DArrayGenerator : MonoBehaviour
{

    public Texture2D[] ordinaryTextures;
    //public GameObject objectToAddTextureTo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void CreateTextureArray()
    {
        // Create Texture2DArray
        Texture2DArray texture2DArray = new
            Texture2DArray(ordinaryTextures[0].width,
            ordinaryTextures[0].height, ordinaryTextures.Length,
            TextureFormat.RGBA32, true, false);

        // Apply settings
        texture2DArray.filterMode = FilterMode.Point;
        texture2DArray.wrapMode = TextureWrapMode.Clamp;

        // Loop through ordinary textures and copy pixels to the
        // Texture2DArray
        for (int i = 0; i < ordinaryTextures.Length; i++)
        {
            texture2DArray.SetPixels(ordinaryTextures[i].GetPixels(0), i, 0);
        }
        // Apply our changes
        texture2DArray.Apply();
        //AssetDatabase.CreateAsset(texture2DArray, "Assets/TextureArray.tarr");
        // Set the texture to a material
        //objectToAddTextureTo.GetComponent<Renderer>()
        //    .sharedMaterial.SetTexture("_MainTex", texture2DArray);
    }
}
