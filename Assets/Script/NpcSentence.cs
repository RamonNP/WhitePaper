using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcSentence : MonoBehaviour
{
   public string[] sentences;
   public string npcName;

   private Animator animator;

   public NpcSentence nextNpcQuest;
   public int sentenceQuestNextNpc;
   public int currentSentence;

   private void Start() {
      animator = GetComponent<Animator>();
   }

   public void changeAnimation(){
      animator.SetBool("spear", !animator.GetBool("spear")); 
   }

}
