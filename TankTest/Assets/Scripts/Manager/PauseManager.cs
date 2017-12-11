using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseManager : MonoBehaviour {
	public GameObject pauseMenu;
	public bool pause;

	// Use this for initialization
	void Start () {
		pause = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			pause = !pause;
		}

		if (pause) {
			pauseMenu.SetActive(true);
			Time.timeScale = 0;
		} else {
			pauseMenu.SetActive(false);
			Time.timeScale = 1;
		}
	}

	public void LoadGame () {
		SceneManager.LoadScene(1);
	}

	public void QuitGame () {
		SceneManager.LoadScene(0);
	}

	public void ResumeGame () {
		pause = false;
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}
}
