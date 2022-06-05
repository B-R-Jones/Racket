using UnityEngine;

public class ItemSprite : MonoBehaviour
{
    public GameObject ItemHeld;
    protected SpriteRenderer Render;
    private bool SpritePresent;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject ItemImage = this.transform.Find("ItemSprite").gameObject;

        //GameObject _ItemHeld = ItemHeld;


        //float Offset = _ItemHeld.GetComponent<WeaponOffsets>().Offset.x;

        //if (Offset < 0) Offset = Mathf.Abs(Offset); else Offset = Offset * -1;

        //this.transform.SetPositionAndRotation(new Vector3(Offset, 0, 1), this.transform.rotation);

        Render = this.transform.GetComponent<SpriteRenderer>();
        SpritePresent = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (ItemHeld) SpritePresent = true; else SpritePresent = false;

        if (SpritePresent && !Render.sprite)
        {
            if (!ItemHeld)
            {
                Render.sprite = this.GetComponentInParent<AmmoHold>().ItemHeld.GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                Render.sprite = ItemHeld.GetComponent<SpriteRenderer>().sprite;
                //Debug.Log($"IS UPD 000 {Render.sprite.name}");
            }
        }
    }

    private void FixedUpdate()
    {
        if (SpritePresent && Render.sprite)
        {
            this.transform.Rotate(new Vector3(0f, 5f, 0f));
        }
    }
}
