using UnityEngine;

public class EnemyFollowScript : MonoBehaviour
{
    // Public properties
    public float DampTime = 5f;


    // Private properties
    private bool PlayerFound = false;
    private float RotationAngle;
    private GameObject Player;
    private Transform PlayerLocation;
    private Vector3 Velocity = Vector3.zero;
    private Vector3 PlayerPosition;
    private Vector3 CurrentPosition;
    private Vector3 DestinationPath;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Player != null)
        if (PlayerFound)
        {
            //PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
            //PlayerPosition = PlayerLocation.position;
            //CurrentPosition = transform.position;
            //PlayerPosition.x -= CurrentPosition.x;
            //PlayerPosition.y -= CurrentPosition.y;

            //float angle = Mathf.Atan2(PlayerPosition.y, PlayerPosition.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            //DestinationPath = transform.position + (PlayerLocation.position - transform.position);
            //transform.position = Vector3.SmoothDamp(transform.position, DestinationPath, ref Velocity, DampTime);
        }
    }
}
