using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour {

	#region Singleton
	public static SFXManager instance = null;

	void Awake() {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
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
