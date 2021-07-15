using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CenasCOntroller : MonoBehaviour
{
    public void MudarCena(string cena) {
        SceneManager.LoadScene(cena);
    }
}
