using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();
    private bool roundOver = false;

    // [SerializeField] TMP_Text playerWinText;
    // [SerializeField] GameObject winBarOverlay, roundComplete;
    [SerializeField] GameObject[] powerUps;
    [SerializeField] float powerUpsSpawnTime;
    private float powerUpsSpawnCounter;
    // Start is called before the first frame update
    void Start()
    {
        powerUpsSpawnCounter = powerUpsSpawnTime * Random.Range(0.75f, 1.25f);
        foreach (PlayerController player in GameManager.instance.activePlayers)
        {
            // when we use int number it mean that range wont return max number 8 but return 7
            // if we use float number then it can return max number
            int randomPoint = Random.Range(0, spawnPoints.Count);
            player.transform.position = spawnPoints[randomPoint].position;

            if (spawnPoints.Count >= GameManager.instance.activePlayers.Count)
            {
                spawnPoints.RemoveAt(randomPoint);
            }
            
        }

        GameManager.instance.canFight = true;
        GameManager.instance.ActivatePlayers();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.CheckActivePlayers() == 1 && !roundOver)
        {
            roundOver = true;

            StartCoroutine(EndRoundCo());
        }

        if (powerUpsSpawnCounter > 0 && powerUps.Length > 0)
        {
            powerUpsSpawnCounter -= Time.deltaTime;

            if (powerUpsSpawnCounter <= 0)
            {
                SpawnRandomPowerUp();
                powerUpsSpawnCounter = powerUpsSpawnTime * Random.Range(0.75f, 1.25f);
            }
        }
    }

    IEnumerator EndRoundCo()
    {   
        UIController.instance.playerWinText.text = "Player " + (GameManager.instance.lastPlayerNumber + 1) + " Wins!";
        UIController.instance.playerWinText.gameObject.SetActive(true);
        UIController.instance.winBarOverlay.SetActive(true);
        UIController.instance.roundComplete.SetActive(true);

        // GameManager.instance.roundWins[GameManager.instance.lastPlayerNumber];
        GameManager.instance.AddRoundWin();

        yield return new WaitForSeconds(3f);

        UIController.instance.loadScreen.SetActive(true);

        GameManager.instance.GoToNextArena();
    }

    private void SpawnRandomPowerUp()
    {   
        int randomPowerUp = Random.Range(0, powerUps.Length);

        int randomSpawnPos = Random.Range(0, spawnPoints.Count);

        Instantiate(powerUps[randomPowerUp], spawnPoints[randomSpawnPos].position, spawnPoints[randomSpawnPos].rotation);
    }
}
