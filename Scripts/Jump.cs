using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour {
    [Range(1,10)]
    public float jumpVelocity;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    Rigidbody rb;
    [SerializeField] private AudioClip[] m_JumpSound;
    AudioSource RollAudioSource;
    AudioSource JumpAudioSource;

    void Awake() {
        rb = GetComponent<Rigidbody> ();
        AudioSource[] audios = GetComponents<AudioSource>();
        RollAudioSource = audios[0];
        JumpAudioSource = audios[1];
    }

    // Update is called once per frame
    void Update() {
        if(rb.velocity.y < 0){
            rb.velocity += Vector3.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(Input.GetButtonDown("Jump")){
            PlayJumpSound();
            GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
        }
    }

    private void PlayJumpSound(){
        int n = Random.Range(0, m_JumpSound.Length);
        RollAudioSource.Stop();
        JumpAudioSource.PlayOneShot(m_JumpSound[n]);
    }
}
