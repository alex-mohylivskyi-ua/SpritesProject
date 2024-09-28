using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] Transform exitPoint;
    [SerializeField] GameObject teleportEffect;


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
        if(other.tag == "Player" && exitPoint)
        {
            other.transform.position = exitPoint.position;
            Instantiate(teleportEffect, transform.position, transform.rotation);
            Instantiate(teleportEffect, exitPoint.position, exitPoint.rotation);
            AudioManager.instance.PlaySFX(7);
        }

    }

}
