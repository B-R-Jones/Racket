using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public properties
    public GameManager GameManager;
    public Vector3 PlayerPosition = new Vector2(0f, 0f);
    public float PlayerDetectionDistance = 0f;
    public bool PlayerDetected = false;
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
        DetectPlayer();
        Move();
        //WeaponsCheck();
        //DetectFire();
        //WeaponsCheck();
    }


    void Move()
    {
        //Engine and movement modifier variables
        //Transform Engine = this.transform.Find("Light Engine").transform;
        Vector2 CurrentPosition = transform.position;
        Vector2 _forward = transform.up;
        Vector2 _backward = -1 * (_forward);

        PlayerPosition = GameManager.PlayerPosition;

        //if (Input.GetKey(KeyCode.A)) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
        //                                                                               transform.rotation.eulerAngles.y,
        //                                                                               transform.rotation.eulerAngles.z + TurnSpeed));
        //if (Input.GetKey(KeyCode.D)) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
        //                                                                               transform.rotation.eulerAngles.y,
        //                                                                               transform.rotation.eulerAngles.z - TurnSpeed));



        if (PlayerDetected)
        {
            //MainThrusterOn = true;
        }
        else
        {
            //MainThrusterOn = false;
        }

        //RetrothrustersOn = Input.GetKey(KeyCode.S);

        if (MovementSpeed > MaxSpeed) MovementSpeed = MaxSpeed;
        if (MovementSpeed < MinSpeed) MovementSpeed = MinSpeed;
    }

    void DetectPlayer()
    {
        PlayerDetected = Vector2.Distance(transform.position, PlayerPosition) < PlayerDetectionDistance;
        if (PlayerDetected)
        {
            Vector2 Direction = PlayerPosition - transform.position;
            //Direction.Normalize();
            float Offset = -90f;
            float Angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

            //transform.up = Time.deltaTime * TurnSpeed * Direction;

            //transform.rotation = Quaternion.Euler(Vector3.forward * (Angle + Offset));
            var LookToward = Quaternion.LookRotation(PlayerPosition - transform.position);
            Quaternion rotation = Quaternion.AngleAxis(Angle + Offset, transform.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, TurnSpeed * Time.deltaTime);

            //transform.rotation = Quaternion.AngleAxis(Angle, Vector2.up);
            //if (Angle < -1)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
            //                                                      transform.rotation.eulerAngles.y,
            //                                                      transform.rotation.eulerAngles.z - TurnSpeed));
            //}
            //if (Angle > 1)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
            //                                                      transform.rotation.eulerAngles.y,
            //                                                      transform.rotation.eulerAngles.z + TurnSpeed));
            //}
            //if (-1 < Angle && Angle < 1)
            //{
            //    transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
            //                                                      transform.rotation.eulerAngles.y,
            //                                                      transform.rotation.eulerAngles.z));
            //}
        }
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
