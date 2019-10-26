using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioClip : MonoBehaviour {
	AudioSource audio;
	[SerializeField] private AudioClip[] m_Sounds;    

	void Start() {
		audio = GetComponent<AudioSource>();
	}		

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player"){	
			// get position from transform
			Vector3 pickupLocation = new Vector3(transform.position.x, transform.position.y, transform.position.z) ;

			int n = Random.Range(0, m_Sounds.Length);
			AudioSource.PlayClipAtPoint( m_Sounds[n], pickupLocation, 1.0f );
		}
	}
}
