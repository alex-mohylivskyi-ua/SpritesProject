using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public TMP_Text playerWinText;
    public GameObject winBarOverlay, roundComplete;
    public GameObject pauseScreen, loadScreen;
    [SerializeField] string mainMenuScene;
    [SerializeField] GameObject firstUIButton;

    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // || Gamepad.current.startButton.wasPressedThisFrame
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            PauseResumeGame();
            AudioManager.instance.PlaySFX(8);
        }
    }

    public void PauseResumeGame()
    {
        

        if (pauseScreen.gameObject.activeInHierarchy) {
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        } else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;

            // Set the first preselected UI button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstUIButton);
        }

        


        
    }

    public void MainMenu()
    {
        // Clear the game start
        foreach(PlayerController player in GameManager.instance.activePlayers)
        {
            Destroy(player.gameObject);
        }

        Destroy(GameManager.instance.gameObject);
        GameManager.instance = null;
        // Clear the game end

        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
