using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartGameChecker : MonoBehaviour
{
    public string levelToLoad;
    private int playersInZone;

    public TMP_Text startCountText;
    [SerializeField] private float timeToStartGame = 10;
    private float startGameCounter;
    private bool freezeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null && GameManager.instance.activePlayers.Count > 1 && GameManager.instance.activePlayers.Count == playersInZone)
        {
            if (!startCountText.gameObject.activeInHierarchy)
            {
                StartCoroutine(PlaySoundCoroutine());
            }
            
            startCountText.gameObject.SetActive(true);
            // AudioManager.instance.PlaySFX(3);

            if (!freezeCounter)
            {   
                startGameCounter -= Time.deltaTime;
                startCountText.text = Mathf.CeilToInt(startGameCounter).ToString();
                
            }
            

            if (Mathf.FloorToInt(startGameCounter) < 0)
            {
                Debug.Log(startGameCounter);
                freezeCounter = true;
                // SceneManager.LoadScene(levelToLoad);
                GameManager.instance.StartFirstRound();
            }
            
        } else {
            startCountText.gameObject.SetActive(false);
            // Here we set the start game counter number....we do it from the start
            startGameCounter = timeToStartGame;
            freezeCounter = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playersInZone ++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playersInZone --;
        }
    }

    IEnumerator PlaySoundCoroutine()
    {
        while (true)
        {
            AudioManager.instance.PlaySFX(3); // Change the index as needed
            yield return new WaitForSeconds(1f); // Wait for 1 second
        }
    }
}
