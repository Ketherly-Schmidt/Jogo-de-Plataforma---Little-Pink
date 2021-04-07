using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sair : MonoBehaviour {
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E)){
            Application.Quit();
        }

    }



}

