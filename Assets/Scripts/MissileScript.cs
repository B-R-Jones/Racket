using System.Collections;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public float FlightSpeed = 1f;
    public float FuseTimer = 2f;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        StartExplosion(FuseTimer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //CollisionCheck(collision);
        if ((collision.gameObject.CompareTag("Player")) ||
            (collision.gameObject.CompareTag("Enemy")) ||
            (collision.gameObject.CompareTag("Missile")))
        {
            Destroy(collision.gameObject);
            StartExplosion(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += FlightSpeed * Time.deltaTime * transform.up;
    }

    void StartExplosion(float fuseTime)
    {
        StartCoroutine(Explode(fuseTime));
    }
    IEnumerator Explode(float fuseTime)
    {
        yield return new WaitForSeconds(fuseTime);
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
