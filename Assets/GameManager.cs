using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public properties
    public int EnemyCount = 1;
    public GameObject Enemy;
    public GameObject[] enemies;
    public GameObject RespawnAs;
    public GameObject Player;
    public Vector2 PlayerPosition;
    public TextMeshProUGUI ScoreText;

    // Private properties
    private int SpawnDistance;
    private int EnemyCounter;
    private bool PlayerFound;
    private bool RespawnMe = true;
    private Vector3 SpawnPoint;
    public List<GameObject> PlayerWeapons = new List<GameObject>();

    // Scorekeeping
    public int EnemyKills;
    public int PlayerKills;



    // Start is called before the first frame update
    void Start()
    {
        LookForPlayer();
        WeaponsCheck();
        LookForEnemies();
        SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        LookForPlayer();
        LookForEnemies();
        PlayerKills += (EnemyCount - enemies.Length);
        if (PlayerFound == false && RespawnMe == true)
        {
            EnemyKills += 1;
            StartCoroutine(Respawn());
        }
        if (PlayerFound) WeaponsCheck();
        SpawnEnemies();
        ScoreText.text = "Player Kills: " + PlayerKills + "\n" + "Enemy Kills: " + EnemyKills;
    }


    List<GameObject> WeaponsCheck()
    {
        return Player.GetComponent<PlayerController>().Weapons;
    }
    IEnumerator Respawn()
    {
        RespawnMe = false;
        yield return new WaitForSeconds(2.0f);
        Instantiate(RespawnAs, new Vector3(0, 0), transform.rotation);
        RespawnMe = true;
    }

    void LookForPlayer()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            PlayerFound = true;
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerPosition = Player.transform.position;
        }
        else
        {
            PlayerFound = false;
        }
    }

    void LookForEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void SpawnEnemies()
    {
        if (enemies.Length == 0)
        {
            SpawnDistance = 0;
            for (EnemyCounter = 0; EnemyCounter < EnemyCount; EnemyCounter++)
            {
                SpawnPoint = new Vector3(transform.position.x + SpawnDistance, transform.position.y);
                Instantiate(Enemy, SpawnPoint, transform.rotation);
                SpawnDistance += 5;
            }
        }

    }
}
