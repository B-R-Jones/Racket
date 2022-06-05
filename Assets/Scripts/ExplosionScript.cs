using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float DestroyTimer = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, DestroyTimer);
    }
}
