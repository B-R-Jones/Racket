using UnityEngine;

public class FollowScript : MonoBehaviour
{
    public Transform Player;
    public float DampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            Vector3 point = Camera.main.WorldToViewportPoint(Player.position);
            Vector3 delta = Player.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, DampTime);
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                Player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
    }
}
