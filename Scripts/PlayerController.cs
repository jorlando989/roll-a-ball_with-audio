using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    private int count;
    public Text countText;
    public Text winText;
    AudioSource RollAudioSource;
    bool onTerrain = false;
    [SerializeField] private AudioClip[] m_RollingSounds;
    [SerializeField] private AudioClip[] m_LandSound;
    public Camera cam;
    float max_y = 0f;
    bool jumped = false;

    void Start() {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
        AudioSource[] audios = GetComponents<AudioSource>();
        RollAudioSource = audios[0];
    }

    void Update(){
        //get max height
        if(transform.position.y > max_y){
            max_y = (float)transform.position.y;
        }
        if(Input.GetButtonDown("Jump")){
            jumped = true;
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal,0,moveVertical);
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 relativeMovement = forward * moveVertical + right * moveHorizontal;

        rb.AddForce(relativeMovement*speed);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Pickup")){
            other.gameObject.SetActive(false);
            count += 1;
            setCountText();
        } else if(other.gameObject.CompareTag("Pickup tier 2")){
            other.gameObject.SetActive(false);
            count += 3;
            setCountText();
        } else if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("platform")){
            //play landing sound
            PlayLandSound();
            //play rolling sound
            RollingCycle();
        }  else if(other.gameObject.CompareTag("Terrain")){
            //play landing sound
            if(jumped){
                PlayLandSound();
                jumped = false;
            }
            //play rolling sound
            RollingCycle();
        } 
    }

    void OnTriggerStay(Collider other){
        if(other.gameObject.CompareTag("ground")|| other.gameObject.CompareTag("platform")){
            //play rolling sound
            RollingCycle();
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.CompareTag("ground")){
            //play rolling sound
            RollAudioSource.Stop();
        }
    }

    void setCountText(){
        countText.text = "Count: " + count.ToString();
        if(count >= 16){
            winText.text = "You Win!!!";
        }
    }

    //rolling sound keeps playing if jump into air from rolling
    private void RollingCycle() {
        if(rb.velocity.magnitude == 0) {    //if not moving
            RollAudioSource.Stop();
            return;
        }
        if(!RollAudioSource.isPlaying){ //&& rb.velocity.magnitude >= 0.05 && 
            // pick & play a random sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_RollingSounds.Length);
            RollAudioSource.clip = m_RollingSounds[n];
            RollAudioSource.Play();
            // move picked sound to index 0 so it's not picked next time
            m_RollingSounds[n] = m_RollingSounds[0];
            m_RollingSounds[0] = RollAudioSource.clip;
        }
    }

    private void PlayLandSound(){
        int n = Random.Range(0, m_LandSound.Length);
        RollAudioSource.volume = (float) max_y + 0.1f; 
        max_y = 0f;
        RollAudioSource.clip = m_LandSound[n];
        RollAudioSource.Play();
    }
}
