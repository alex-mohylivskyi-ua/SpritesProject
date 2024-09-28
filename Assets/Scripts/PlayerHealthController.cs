using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthController : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    public SpriteRenderer[] heartDisplay;
    public Sprite healthFull, herathEmpty;

    public float invincibilityTime, heartFlashTime = .2f;
    private float invincCounter, flashCounter;

    public Transform heartHolder;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            // updateHealthDisplay();
            DamagePlayer(1);
        }

        if (invincCounter > 0) {
            invincCounter -= Time.deltaTime;
            flashCounter -= Time.deltaTime;

            if (flashCounter < 0)
            {   
                flashCounter = heartFlashTime;
                // check if gameobject is active
                heartHolder.gameObject.SetActive(!heartHolder.gameObject.activeInHierarchy);
            }

            if (invincCounter <= 0)
            {
                heartHolder.gameObject.SetActive(true);
            }
        }
    }

    //We don't know in which file the update method will work firstly, 
    // so using LateUpdate unity method to be sure it will be at the end
    private void LateUpdate()
    {
        heartHolder.localScale = transform.localScale;
    }

    public void updateHealthDisplay()
    {
        switch(currentHealth)
        {   
            case 0:
                heartDisplay[0].sprite = herathEmpty;
                heartDisplay[1].sprite = herathEmpty;
                heartDisplay[2].sprite = herathEmpty;
                break;
            case 1:
                heartDisplay[0].sprite = healthFull;
                heartDisplay[1].sprite = herathEmpty;
                heartDisplay[2].sprite = herathEmpty;
                break;
            case 2:
                heartDisplay[0].sprite = healthFull;
                heartDisplay[1].sprite = healthFull;
                heartDisplay[2].sprite = herathEmpty;
                break;
            case 3:
                heartDisplay[0].sprite = healthFull;
                heartDisplay[1].sprite = healthFull;
                heartDisplay[2].sprite = healthFull;
                break;
        }
    }

    public void DamagePlayer(int damageToTake)
    {

        if (invincCounter <= 0 && GameManager.instance.canFight)
        {
            currentHealth -= damageToTake;

            Debug.Log($"Current Health is {currentHealth}");

            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            updateHealthDisplay();
            
            // Disable player if 0 hp
            if (currentHealth == 0)
            {
                AudioManager.instance.PlaySFX(4);
                gameObject.SetActive(false);
            }

            invincCounter = invincibilityTime;
            flashCounter = heartFlashTime;
        }
        
    }

    public void KillPlayer()
    {
        currentHealth = 0;

        AudioManager.instance.PlaySFX(4);
        gameObject.SetActive(false);

    }

    public void FillHealth()
    {
        currentHealth = maxHealth;
        updateHealthDisplay();
        flashCounter = 0;
        invincCounter = 0;

        heartHolder.gameObject.SetActive(true);
    }

    public void MakeInvincible(float invicLength)
    {
        invincCounter = invicLength;
    }
}
