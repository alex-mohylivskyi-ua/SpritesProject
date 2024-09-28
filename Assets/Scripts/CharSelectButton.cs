using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharSelectButton : MonoBehaviour
{
    public SpriteRenderer theSR;
    public Sprite buttonUp, buttonDown;

    public bool isPressed;
    public float waitToPopUp = .5f;
    private float popCounter;
    public AnimatorOverrideController animController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed == true)
        {
            popCounter -= Time.deltaTime;

            if (popCounter <=0)
            {
                isPressed = false;
                theSR.sprite = buttonUp;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController thePlayer = other.GetComponent<PlayerController>();

        if (other.tag == "Player" && !isPressed)
        {
            if (thePlayer.playerRigidbody.velocity.y < -0.1f)
            {
                //change skin, change animatorController
                thePlayer.anim.runtimeAnimatorController = animController;
                isPressed = true;
                theSR.sprite = buttonDown;
                popCounter = waitToPopUp;
                AudioManager.instance.PlaySFX(2);
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player" && isPressed)
        {
            isPressed = false;
            theSR.sprite = buttonUp;
        }
    }
}
