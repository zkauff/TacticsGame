using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEntity : MonoBehaviour {
	public Vector2 gridPosition = Vector2.zero;
	public Transform unitTransform;
	public AudioSource audioSource;
	public AudioClip hurtSoundEffect;
	//inventory for gameTiles is simply "contents"
	public List<Item> inventory;

	public void Awake(){
		unitTransform = this.transform;
		audioSource = AudioManager.instance.defaultAudio;
		inventory = new List<Item>();
	}

	public Vector2 getPosition() {
		return gridPosition;
	}

	public void hurt() {
		try{
			audioSource.clip = hurtSoundEffect;
			audioSource.Play();
		} catch(MissingComponentException e){

		}
		StartCoroutine(hurtShake());
	}

	public void heal() {
		try{
			audioSource.clip = hurtSoundEffect;
			audioSource.Play();
		} catch(MissingComponentException e){

		}
		StartCoroutine(healShake());
	}

	IEnumerator hurtShake(){
		int count = 0;
		Vector3 originalPosition = unitTransform.localPosition;
		Color originalColor = unitTransform.GetComponent<Renderer>().material.color;
		unitTransform.GetComponent<Renderer>().material.color = Color.red;
		while(count < 5){
			this.randomPosition(originalPosition);
			yield return new WaitForSeconds(0.15f);
			count++;
		}
		unitTransform.localPosition = originalPosition;
		unitTransform.GetComponent<Renderer>().material.color = originalColor;
		StopCoroutine(hurtShake());
	}

	IEnumerator healShake(){
		int count = 0;
		Vector3 originalPosition = unitTransform.localPosition;
		Color originalColor = unitTransform.GetComponent<Renderer>().material.color;
		unitTransform.GetComponent<Renderer>().material.color = Color.green;
		while(count < 3){
			this.randomPosition(originalPosition);
			yield return new WaitForSeconds(0.15f);
			count++;
		}
		unitTransform.localPosition = originalPosition;
		unitTransform.GetComponent<Renderer>().material.color = originalColor;
		StopCoroutine(healShake());
	}

	void randomPosition(Vector3 originalPosition){
		float shakeAmount = 0.1f;
		unitTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
	}
}
