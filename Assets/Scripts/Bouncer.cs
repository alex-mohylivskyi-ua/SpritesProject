using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] Sprite bouncePadUp, bouncePadDown;
    [SerializeField] float bouncePower, stayUpTime = 0f;

    private float stayUpTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stayUpTimer > 0)
        {
            stayUpTimer -= Time.deltaTime;

            if (stayUpTimer <= 0)
            {
                Debug.Log("Time norm");
                gameObject.GetComponent<SpriteRenderer>().sprite = bouncePadDown;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        
        if(other.tag == "Player" && other.GetComponent<Rigidbody2D>().velocity.y < -0.1f)
        {
            if (stayUpTimer <= 0)
            {
                Rigidbody2D theRB = other.GetComponent<Rigidbody2D>();
                theRB.velocity = new Vector2(theRB.velocity.x, bouncePower);
                gameObject.GetComponent<SpriteRenderer>().sprite = bouncePadUp;
                stayUpTimer = stayUpTime;
                AudioManager.instance.PlaySFX(6);
            }
        }
    }
}
