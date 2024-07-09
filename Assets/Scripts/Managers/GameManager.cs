using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameObject playerPrefab;
    public Transform spawnPoint;
    public Transform[] path;
    public List<EnemyOnPlatform> platforms;
    
    private GameObject player;

    public GameObject wayPointsParent;

    private bool isStarted;
    
    public TextMeshProUGUI startText;

    public GameObject Player
    {
        get => player;
        set => player = value;
    }

    private void Awake()
    {
        GetPath();
        SpawnPlayer();
    }

    private void GetPath()
    {
        path = wayPointsParent.GetComponentsInChildren<Transform>().Skip(1).ToArray();;
    }
    

    private void SpawnPlayer()
    {
        player = Instantiate(playerPrefab);
        SetResPos();
        Camera.main.transform.SetParent(player.transform);
        player.GetComponent<PlayerMove>().waypoints = path;
        
    }

    private void StartLvl()
    {
        EnableStartText(false);
        player.GetComponent<PlayerMove>().StartWalk();
    }

    private void SetResPos()
    {
        player.transform.position = spawnPoint.position;
        player.transform.rotation = spawnPoint.rotation;
    }
    
    public void FinishLevel()
    {
        ResetLvl();
    }

    private void ResetLvl()
    {
        foreach (var platform in platforms)
        {
            platform.ResetEnemies();
        }

        isStarted = false;
        
        player.gameObject.SetActive(false);
        SetResPos();
        player.gameObject.SetActive(true);
        
        EnableStartText(true);
    }

    private void EnableStartText(bool isEnabled)
    {
        startText.enabled = isEnabled;
    }

    private void Update()
    {
        if (!isStarted && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            StartLvl();
            isStarted = true;
        }
    }
}
