using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSkyboxScroll : MonoBehaviour
{
    public float ParallaxMultiplier = 0.5f;
    private Transform _Camera;

    private Vector3 PreviousCameraPosition;
    private float UnitSizeW;
    private float UnitSizeH;
    // Start is called before the first frame update
    void Start()
    {
        _Camera = Camera.main.transform;
        PreviousCameraPosition = _Camera.position;

        Sprite _Sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D _Texture = _Sprite.texture;
        UnitSizeW = _Texture.width / _Sprite.pixelsPerUnit;
        UnitSizeH = _Texture.height / _Sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 DeltaMovement = _Camera.position - PreviousCameraPosition;
        //float ParallaxMultiplier = 0.5f;
        transform.position += DeltaMovement * ParallaxMultiplier;
        PreviousCameraPosition = _Camera.position;

        if (Mathf.Abs(_Camera.position.x - transform.position.x) >= UnitSizeW)
        {
            float OffsetX = (_Camera.position.x - transform.position.x) % UnitSizeW;
            transform.position = new Vector3(_Camera.position.x + OffsetX, transform.position.y);
        }

        if (Mathf.Abs(_Camera.position.y - transform.position.y) >= UnitSizeH)
        {
            float OffsetY = (_Camera.position.y - transform.position.y) % UnitSizeH;
            transform.position = new Vector3(transform.position.x, _Camera.position.y + OffsetY);
        }
    }
}
