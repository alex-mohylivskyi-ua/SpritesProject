using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoinPlayerKeyboard2 : MonoBehaviour
{

    public GameObject playerToLoad;
    private bool hasLoadedPlayer;

    public GameObject spawnPortal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (GameManager.instance != null)  FIX BUG 
        if (GameManager.instance != null && !hasLoadedPlayer && GameManager.instance.activePlayers.Count < GameManager.instance.maxPlayers) {
            if (Keyboard.current.jKey.wasPressedThisFrame || Keyboard.current.lKey.wasPressedThisFrame || Keyboard.current.rightShiftKey.wasPressedThisFrame) {
                if (spawnPortal.activeInHierarchy)
                {
                    Instantiate(playerToLoad, spawnPortal.transform.position, spawnPortal.transform.rotation);
                } else
                {
                    Instantiate(playerToLoad, transform.position, transform.rotation);
                }
                
                hasLoadedPlayer = true;
            }
        }
    }
}
