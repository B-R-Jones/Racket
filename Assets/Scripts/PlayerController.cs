using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public properties
    public float TurnSpeed = 5f;
    public float MovementSpeed;
    public float MaxSpeed = 10f;
    public float MinSpeed = -10f;
    public float Degrees = 1f;
    public int WeaponsMax = 0;
    public int WeaponCount = 0;
    public List<GameObject> Weapons = new List<GameObject>();
    public SpriteRenderer Renderer;
    public Sprite DefaultSprite;
    //public GameObject WeaponL;
    public bool CommenceFiring = false;

    //Engine properties
    public bool MainThrusterOn = false;
    public bool RetrothrustersOn = false;

    // Private properties
    private float DefaultMovementSpeed = 0;
    private float SquishTime = 1f;
    private bool Squished = false;

    // Start is called before the first frame update
    void Start()
    {
        DefaultMovementSpeed = MovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //WeaponsCheck();
        DetectFire();
        //WeaponsCheck();
    }

    void Move()
    {
        //Engine and movement modifier variables
        //Transform Engine = this.transform.Find("Light Engine").transform;

        Vector2 _forward = transform.up;
        Vector2 _backward = -1*(_forward);
        
        if (Input.GetKey(KeyCode.A)) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                                                                                       transform.rotation.eulerAngles.y,
                                                                                       transform.rotation.eulerAngles.z + TurnSpeed));
        if (Input.GetKey(KeyCode.D)) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
                                                                                       transform.rotation.eulerAngles.y,
                                                                                       transform.rotation.eulerAngles.z - TurnSpeed));

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.LeftShift))
        {
            MainThrusterOn = true;
        }
        else
        {
            MainThrusterOn = false;
        }

        //RetrothrustersOn = Input.GetKey(KeyCode.S);

        if (MovementSpeed > MaxSpeed) MovementSpeed = MaxSpeed;
        if (MovementSpeed < MinSpeed) MovementSpeed = MinSpeed;
    }

    void Jump()
    {
        transform.position += MaxSpeed * 10 * transform.up;
    }

    IEnumerator Squish()
    {
        Squished = true;
        Vector3 _Scale = new Vector3(this.transform.localScale.x,
                                     this.transform.localScale.y,
                                     this.transform.localScale.z);
        float _Time = 0;
        while (_Time < SquishTime)
        {
            _Time += Time.deltaTime;
            float _Squish = Mathf.Lerp(1, 0, _Time);
            this.transform.localScale = new Vector3(_Scale.x, _Squish, _Scale.z);
            Debug.Log($"SQUISH: {_Squish}");
            yield return null;
        }
        this.transform.localScale = new Vector3(_Scale.x, 0f, _Scale.z);
        Jump();
        StartCoroutine(Unsquish());
    }

    IEnumerator Unsquish()
    {
        Vector3 _Scale = new Vector3(this.transform.localScale.x,
                                     this.transform.localScale.y,
                                     this.transform.localScale.z);
        float _Time = 0;
        while (_Time < SquishTime)
        {
            _Time += Time.deltaTime;
            float _Squish = Mathf.Lerp(0, 1, _Time);
            this.transform.localScale = new Vector3(_Scale.x, _Squish, _Scale.z);
            Debug.Log($"SQUISH: {_Squish}");
            yield return null;
        }
        this.transform.localScale = new Vector3(_Scale.x, 1f, _Scale.z);
        Squished = false;
    }

    public void RemoveWeapons()
    {
        foreach (Transform Weapon in this.transform)
        {
            Debug.Log("Weapon Name: " + Weapon.name);
            if (Weapon.CompareTag("Weapon") && Weapon.gameObject.activeSelf == true)
            {
                Destroy(Weapon.gameObject);
            }
            WeaponCount--;
        }
    }

    public void WeaponsCheck()
    {
        WeaponCount = 1;
        foreach (Transform Weapon in this.transform)
        {
            if (Weapon.CompareTag("Weapon") && Weapon.gameObject.activeSelf == false)
            {
                Weapon.gameObject.SetActive(true);
            }
            WeaponCount++;
        }
    }

    void DetectFire()
    {
        if (Input.GetMouseButtonDown(0)) CommenceFiring = true;
        if (Input.GetMouseButtonUp(0)) CommenceFiring = false;
    }
}
