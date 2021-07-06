using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
   public AudioSource audioSource;
   public Slider slider;

    public void AjustaVolume(){
        audioSource.volume = slider.value;
    }

}
