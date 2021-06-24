using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescricao;
    public Sprite image;
    public string[] sentences;
    public int currentSentence;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(sentences.Length > 0)
       txtDescricao.text = sentences[currentSentence];
    }

    public void initializeDialog(string[] sentences, string name){
        txtName.text = name;
        this.sentences = sentences;
        currentSentence = 0;
    }

    public void nextSentence(){
        if(sentences.Length <= currentSentence){
            currentSentence++;
        } else {
            print("FIM DO DIALOGO IMPLEMENTAR");
        }
    }
}
