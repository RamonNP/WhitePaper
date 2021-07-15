using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
   public string[] sentences;
   public string[] sentencesKey;
   public string npcName;

   private Animator animator;

   public NpcSentence nextNpcQuest;
   public int sentenceQuestNextNpc;
   public int currentSentence;

   private void Start() {
      animator = GetComponent<Animator>();
      //Invoke("inicializaDialogo", 2.0f);
      //inicializaDialogo();
   }

   public void changeAnimation(){
      animator.SetBool("spear", !animator.GetBool("spear")); 
   }

   public void inicializaDialogo() {
      Dialogo dialogo = null;
      for (int i = 0; i < sentences.Length; i++)
      {
         //print(IdiomaController.instance.idiomaAtual);
         //print(sentences[i]);
         if(IdiomaController.instance.idiomaAtual == "pt-BR" || IdiomaController.instance.idiomaAtual == "pt") {
            IdiomaController.instance.dialogosBr.TryGetValue(sentencesKey[i], out dialogo);
         } else {
            IdiomaController.instance.dialogosEng.TryGetValue(sentencesKey[i], out dialogo);
         }  
         sentences[i] = dialogo.valor;
      }
   }

}
