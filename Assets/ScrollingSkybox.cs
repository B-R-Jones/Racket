using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    public float scrollSpeed;

    void Start()
    {
        //renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        //renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
        this.GetComponent<SpriteRenderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
