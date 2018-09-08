using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	public static AudioManager instance;
	public AudioSource defaultAudio;
	public AudioClip UISoundEffect;

	void Awake() {
		instance = this;
	}
}
