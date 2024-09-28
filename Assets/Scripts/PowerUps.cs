using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] bool isHealth, isInvincible, isSpeedUp, isGravity;
    [SerializeField] float powerupLength = 5f, powerupPower = 1f;
    [SerializeField] GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealthController>().FillHealth();
                AudioManager.instance.PlaySFX(8);
            }

            if (isInvincible)
            {
                other.GetComponent<PlayerHealthController>().MakeInvincible(powerupLength);
                AudioManager.instance.PlaySFX(9);
            }

            if (isSpeedUp) {
                PlayerController thePlayer = other.GetComponent<PlayerController>();
                thePlayer.powerupCounter = powerupLength;
                thePlayer.moveSpeed = powerupPower;
                AudioManager.instance.PlaySFX(10);
            }

            if (isGravity)
            {
                PlayerController thePlayer = other.GetComponent<PlayerController>();
                thePlayer.powerupCounter = powerupLength;
                thePlayer.GetComponent<Rigidbody2D>().gravityScale = powerupPower;
                AudioManager.instance.PlaySFX(11);
            }

            Instantiate(pickupEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }
}
