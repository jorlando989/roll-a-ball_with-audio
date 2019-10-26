using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rolling : MonoBehaviour {
    AudioSource audio;
    [SerializeField] private AudioClip[] m_RollingSounds;

    // Start is called before the first frame update
    void Start(){
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update(){
        
    }

    // private void RollingCycle() {
    //     if(rb.velocity.y != 0 || rb.velocity.magnitude == 0) {    //if jumping
    //         audio.Stop();
    //         return;
    //     }
    //     if(rb.velocity.magnitude >= 0.05 && !audio.isPlaying){
    //         // pick & play a random footstep sound from the array,
    //         // excluding sound at index 0
    //         int n = Random.Range(1, m_RollingSounds.Length);
    //         audio.clip = m_RollingSounds[n];
    //         audio.Play();
    //         // move picked sound to index 0 so it's not picked next time
    //         m_RollingSounds[n] = m_RollingSounds[0];
    //         m_RollingSounds[0] = audio.clip;
    //     }
    // }
}
