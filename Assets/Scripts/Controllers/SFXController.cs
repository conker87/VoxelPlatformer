using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXController : MonoBehaviour {

	#region Singleton

	public static SFXController current = null;

	void Awake() {

		if (current == null) {
            current = this;
		} else if (current != this) {
			Destroy (gameObject);    
		}

		DontDestroyOnLoad(gameObject);

	}

	#endregion

	[SerializeField]
	AudioSource MasterAudioSource, MusicAudioSource, SFXAudioSource, UIAudioSource;

	[SerializeField]
	List<AudioClip> coinCollection = new List<AudioClip>();

	// Use this for initialization
	void Start () {
	


	}

	public void PlayRandomCoinClip() {

		int random = Random.Range (0, coinCollection.Count);

		SFXAudioSource.PlayOneShot(coinCollection [random]);

	}

}
