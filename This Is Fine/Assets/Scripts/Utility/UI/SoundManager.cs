using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

	[SerializeField] private AudioClip[] levelMusicChangeArray;
	private AudioSource audioSource;
	private AudioClip thisLevelMusic;

	private void Awake()
	{
		// keeps the musicManager gameobject alive for other scenes when they load
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	private void Start () {
		// GetComponent: searches for a audioSource and grabs it
		audioSource = GetComponent<AudioSource> ();
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		// Sets the music to the current level variable
		if (scene.buildIndex != 0)
		{
			thisLevelMusic = levelMusicChangeArray[scene.buildIndex];
		}

		// If there is music for the current level
		if (thisLevelMusic)
		{
			// set the audio to play and loop the current  level music
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play();
		}
	//	else if (scene.buildIndex == 0 || scene.buildIndex == 1 || scene.buildIndex == 2 || scene.buildIndex == 3)
	//	{
	//		audioSource.loop = true;
	//	}
		else if (scene.buildIndex != 0)
		{
			audioSource.Stop();
		}
	}
}