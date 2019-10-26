using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underwater : MonoBehaviour {
    AudioSource audio;
    public AudioClip underwater_sound;
    public AudioClip splash_in_sound;
    public AudioClip splash_out_sound;

    // Start is called before the first frame update
    void Start() {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            audio.PlayOneShot(splash_in_sound);
        }
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("Player")){
            audio.clip = underwater_sound;
            if(!audio.isPlaying){
                audio.Play();
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("Player")){
            audio.Stop();
            audio.PlayOneShot(splash_out_sound);
        }
    }
}
