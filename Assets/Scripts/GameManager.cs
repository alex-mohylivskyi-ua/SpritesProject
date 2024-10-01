using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("Game Config")]
    public int maxPlayers;
    public List<PlayerController> activePlayers = new List<PlayerController>();
    public GameObject PlayerSpawnEffect;
    public bool canFight = false;
    public string[] allLevels;
    public int pointsToWin = 5;
    public List<int> roundWins = new List<int>();
    private bool gameWon;
    [SerializeField] private string gameWonLevel;
    [HideInInspector] public int lastPlayerNumber;
    // private string[] sortedLevels;
    private List<string> sortedLevels = new List<string>();

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //TODO
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            GoToNextArena();
        }
    }

    public void AddPlayer(PlayerController newPlayer)
    {
        if(activePlayers.Count < maxPlayers)
        {
            activePlayers.Add(newPlayer);
            // to do fix error
            Instantiate(PlayerSpawnEffect, newPlayer.transform.position, newPlayer.transform.rotation);
        } else 
        {
            Destroy(newPlayer.gameObject);
        }
    }

    public void ActivatePlayers()
    {
        foreach(PlayerController player in activePlayers)
        {
            player.gameObject.SetActive(true);
            player.GetComponent<PlayerHealthController>().FillHealth();
        }
    }

    public int CheckActivePlayers()
    {
        int playerAliveCount = 0;

        for (int i = 0; i < activePlayers.Count; i++)
        {
            if (activePlayers[i].gameObject.activeInHierarchy)
            {
                playerAliveCount++;
                lastPlayerNumber = i;
            }
        }

        return playerAliveCount;
    }

    public void GoToNextArena()
    {   
        // Simple logic
        // SceneManager.LoadScene(allLevels[Random.Range(0, allLevels.Length)]);


        // Hard logic

        if (gameWon)
        {
            foreach(PlayerController player in activePlayers)
            {
                player.GetComponent<PlayerController>().PlayerReset();
                player.gameObject.SetActive(false);
                player.GetComponent<PlayerHealthController>().FillHealth();
            }

            SceneManager.LoadScene(gameWonLevel);
        } else
        {
            if(sortedLevels.Count == 0)
            {
                List<string> allLevelListCopy = new List<string>();
                allLevelListCopy.AddRange(allLevels);

                for(int i = 0; i < allLevels.Length; i++)
                {
                    int selected = Random.Range(0, allLevelListCopy.Count);

                    sortedLevels.Add(allLevelListCopy[selected]);

                    allLevelListCopy.RemoveAt(selected);
                }
            }

            string levelToLoad = sortedLevels[0];
            sortedLevels.RemoveAt(0);

            foreach(PlayerController player in activePlayers)
            {
                player.GetComponent<PlayerController>().PlayerReset();
                player.GetComponent<PlayerHealthController>().FillHealth();
                player.gameObject.SetActive(true);
            }

            SceneManager.LoadScene(levelToLoad);
        }
    }

    public void StartFirstRound()
    {

        roundWins.Clear();

        foreach (PlayerController player in activePlayers)
        {
            roundWins.Add(0);
        }

        gameWon = false;

        GoToNextArena();

        // SceneManager.LoadScene(levelToLoad);
    }

    public void AddRoundWin()
    {
        
        if (CheckActivePlayers() == 1)
        {
            roundWins[lastPlayerNumber] = roundWins[lastPlayerNumber] + 1;

            if (roundWins[lastPlayerNumber] >= pointsToWin)
            {
                gameWon = true;
            }
        }
    }
}
