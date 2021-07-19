using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogController : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtDescricao;
    public string keyName;
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

    public void initializeDialog(string[] sentences, string npcKeyName, int currentSentence){
        keyName = npcKeyName;
        ChangeNameNpc();
        this.sentences = sentences;
        this.currentSentence = currentSentence;
    }
    public void ChangeNameNpc(){
        Dialogo dialogo = null;
        if(IdiomaController.instance.idiomaAtual == "pt-BR" || IdiomaController.instance.idiomaAtual == "pt") {
            IdiomaController.instance.dialogosBr.TryGetValue(keyName, out dialogo);
        } else {
            IdiomaController.instance.dialogosEng.TryGetValue(keyName, out dialogo);
        } 
        if(dialogo!=null)
        txtName.text = dialogo.valor;
    }

    public void nextSentence(){
        if(sentences.Length <= currentSentence){
            currentSentence++;
        } else {
            //print("FIM DO DIALOGO IMPLEMENTAR");
        }
    }
}
