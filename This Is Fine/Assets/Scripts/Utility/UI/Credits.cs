using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

	private float speed = 40f;
	private float time = 22f;
	// Use this for initialization
	void Start () {
		// searches for a component within the gameObject
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * speed);
		time -= Time.deltaTime;

		if(time < 0) { SceneManager.LoadScene(0); }
		else if (Input.GetKeyDown(KeyCode.Escape)) { SceneManager.LoadScene(0); }
	}
}