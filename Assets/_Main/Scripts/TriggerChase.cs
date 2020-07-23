using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChase : MonoBehaviour {
    private BatBehaviour bat;
   

    // Start is called before the first frame update
    void Start() {
        bat = GetComponentInParent<BatBehaviour>();
    }

   
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player cerca");
            bat.MovementOn();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log("Player lejos");
            bat.MovementOff();
        }
    }
    

}
