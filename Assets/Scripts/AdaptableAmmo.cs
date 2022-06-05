using System.Collections;
using UnityEngine;

public class AdaptableAmmo : MonoBehaviour
{

    public bool BeamAmmo;
    public bool ProjectileAmmo;
    public bool MissileAmmo;
    public bool DestructionTimer;
    public float EndTimer;
    public GameObject EndEffect;
    public float FlightSpeed;
    public AnimationClip FlightAnimation;

    protected Transform Ammo;
    public Transform Parent;
    protected Camera Cam;
    protected Animator Anim;

    // Start is called before the first frame update
    void Start()
    {
        Ammo = this.transform;
        Anim = this.GetComponent<Animator>();
        Cam = Camera.main;
        AnimationCheck();
        SetParent();
        SetScaleAndPosition();
        DestructionCheck();
    }

    // Update is called once per frame
    void Update()
    {
        if (BeamAmmo)
        {
            BeamOnCheck();
        }
        else
        {
            Move();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.CompareTag("Player")) ||
            (collision.gameObject.CompareTag("Enemy")) ||
            (collision.gameObject.CompareTag("Missile")))
        {
            //Destroy(collision.gameObject);
            if (BeamAmmo == false)
            {
                CancelCountdown();
                DestructionCountdown(0);
            }
        }
    }

    void AnimationCheck()
    {
        if (FlightAnimation) Anim.Play(FlightAnimation.name);
    }
    void SetParent()
    {
        if (BeamAmmo)
        {
            Parent = this.transform.parent;
        }
        else
        {
            Parent = null;
        }
    }
    void SetScaleAndPosition()
    {
        float PositionY = 0f;
        float ScaleY = 0f;
        if (BeamAmmo)
        {
            PositionY = Cam.orthographicSize + 2;
            ScaleY = (2 * PositionY);
        }
        Ammo.localPosition = new Vector3(0f, PositionY, 0f);
        Ammo.localScale = new Vector3(1f, ScaleY, 1f);
    }
    void DestructionCheck()
    {
        if (!BeamAmmo && (ProjectileAmmo || MissileAmmo))
        {
            DestructionCountdown(EndTimer);
        }
    }
    void BeamOnCheck()
    {
        if (Parent.GetComponentInParent<FireScript>().CommenceFire == false)
        {
            Parent.GetComponentInParent<FireScript>().BeamOn = false;
            Destroy(this.gameObject);
        }
    }
    void Move()
    {
        transform.position += FlightSpeed * Time.deltaTime * transform.up;
    }
    void DestructionCountdown(float EndTimer)
    {
        StartCoroutine(Explode(EndTimer));
    }
    void CancelCountdown()
    {
        StopCoroutine(Explode(EndTimer));
    }
    IEnumerator Explode(float EndTimer)
    {
        yield return new WaitForSeconds(EndTimer);
        if (EndEffect)
        {
            Instantiate(EndEffect, transform.position, transform.rotation);
        }
        Destroy(this.gameObject);
    }
}
