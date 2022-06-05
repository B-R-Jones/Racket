using System.Collections;
using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    public float FlightSpeed = 1f;
    public float EndTimer = 2f;
    public bool BeamAmmo = false;
    public AnimationClip FlightAnimation;
    public GameObject EndEffect;

    private Transform Shot;
    private Camera Cam;

    // Start is called before the first frame update
    void Start()
    {
        if (FlightAnimation) this.GetComponent<Animator>().Play(FlightAnimation.name);
        Shot = this.transform;
        if (BeamAmmo)
        {
            Transform Parent = this.transform.parent;
            Shot.SetParent(Parent);
            Cam = Camera.main;
            float PositionY = Cam.orthographicSize;
            float ScaleY = (2 * PositionY) + 4;
            Shot.localPosition = new Vector3(0f, 10f, 0f);
            Shot.localScale = new Vector3(1.0f, ScaleY, 1.0f);
        }
        else
        {
            Shot.localPosition = new Vector3(0f, 0f, 0f);
            Shot.parent = null;
            StartExplosion(EndTimer);
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
                StartExplosion(0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(BeamAmmo == false) Move();
        if (BeamAmmo)
        {
            Shot = this.transform;
            if (Shot.GetComponentInParent<FireScript>().CommenceFire == false)
            {
                this.gameObject.GetComponentInParent<FireScript>().BeamOn = false;
                Destroy(this.gameObject);
            }
        }
        else
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += FlightSpeed * Time.deltaTime * transform.up;
    }

    void StartExplosion(float EndTimer)
    {
        StartCoroutine(Explode(EndTimer));
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
