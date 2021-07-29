using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
   public AudioSource audioSource;
   public Slider slider;
    public GameObject btnOff;
    public GameObject btnOn;


   private void Start() {
        PlayAudio();
       //PauseAudio();
       //audioSource.Pause();
       //Debug.Log("AudioController PAUSE Audio");
       //Application.ExternalCall("playAudio");
   }
private void Update() {

}
    public void AjustaVolume(){
        if(!audioSource.isPlaying) {
            PlayAudio();
        }
        audioSource.volume = slider.value;
    }

    public void PlayAudio() {
        audioSource.Play();
        btnOn.SetActive(false);
        btnOff.SetActive(true);
    }
    public void PauseAudio() {
        audioSource.Pause();
        btnOn.SetActive(true);
        btnOff.SetActive(false);
    }

}
