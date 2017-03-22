using System;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    bool off;

    void Update() {
        if (Input.GetKeyDown(KeyCode.F)) {  // on and off
            GetComponent<Light>().intensity = (off ? 8 : 0);
            off = !off;
        }
    }

}