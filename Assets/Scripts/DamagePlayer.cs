using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damageToDeal = 1;
    [SerializeField] bool killPlayer = false;
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
            if (killPlayer)
            {
                other.GetComponent<PlayerHealthController>().KillPlayer();
            } else
            {
                other.GetComponent<PlayerHealthController>().DamagePlayer(damageToDeal);
            }
            AudioManager.instance.PlaySFX(5);
        }
    }
}
