using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StompBox : MonoBehaviour
{
    public int stompDamage = 1;
    public float bounceForce = 12f;
    public PlayerController thePlayer;
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
        if (other.tag == "Player" && thePlayer.playerRigidbody.velocity.y < 0)
        {
            other.GetComponent<PlayerHealthController>().DamagePlayer(stompDamage);
            thePlayer.playerRigidbody.velocity = new Vector2(thePlayer.playerRigidbody.velocity.x, bounceForce);
            AudioManager.instance.PlaySFX(1);
        }
    }
}
