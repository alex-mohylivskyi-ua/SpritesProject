using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTrap : MonoBehaviour
{
    [SerializeField] Vector3 rotateAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(rotateAmount * Time.deltaTime);
    }


}
