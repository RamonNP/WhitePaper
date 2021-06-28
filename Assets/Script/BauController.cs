using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauController : MonoBehaviour
{
    public GameObject bauAberto;
    public GameObject bauFechado;
    public GameObject lightPaper;
    private bool aberto;
    // Start is called before the first frame update
    void Start()
    {
        aberto = false;
        bauAberto.SetActive(false);
        lightPaper.SetActive(false);
        bauFechado.SetActive(true);
    }
    private void Update() {
        if(aberto) {
            lightPaper.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        bauAberto.SetActive(true);
        lightPaper.SetActive(true);
        bauFechado.SetActive(false);
        print("Opa"+lightPaper.active);
        aberto = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        print("Opa SIDA"+lightPaper.active);
        aberto = false;
        lightPaper.SetActive(false);
    }

    public void AbrirWhitPaper() {
         Application.ExternalEval("window.open(\"https://www.twitter.com/GilesDMiddleton\",\"_blank\")");
        //Application.ExternalEval("https://drive.google.com/file/d/1eZ0FEsL9lFPud_i-2axQtATpWMpTzIiP/view");
         //Application.ExternalEval("window.open(\"https://drive.google.com/file/d/1eZ0FEsL9lFPud_i-2axQtATpWMpTzIiP/view\",\"_blank\")");
    }
}
