using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private Boolean canPressKey = true;
    private float delay = 1.5f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && canPressKey)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            
            //this prevents the user from spamming the spacebar
            canPressKey = false;
            Invoke("ResetPressKey", delay);
        }
    }

    void ResetPressKey()
    {
        canPressKey = true;
    }
}
