using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSite : MonoBehaviour
{
    public GameObject uiPainel;

    private void Start() {
        uiPainel.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        print(other.gameObject.tag);
        if(other.gameObject.tag == "Player") {
            uiPainel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        uiPainel.SetActive(false);
    }
}
