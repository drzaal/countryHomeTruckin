using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public bool isShowCredits = false;
	public bool isCreditsLoading = false;
	private bool isStarting = false;
	private bool isQuitting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quitGame () {
		Application.Quit();
	}

	public void startGame() {
		if (isStarting) return;
		SceneManager.LoadScene("Game");
		isStarting = true;
	}

	public void showCredits() {
		if (isCreditsLoading) return;
		isShowCredits = !isShowCredits;
		isCreditsLoading = true;
	}
}
