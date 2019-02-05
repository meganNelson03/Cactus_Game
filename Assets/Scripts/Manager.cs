using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	public Slider sunlightSlider, fameSlider, fertilizerSlider;
	public Image clock;
	public GameObject fertilizer, waste, shopPanel, aloeBubble, elvisBubble, keanuBubble, shopEntrance, auditionEntrance;
	public Text aloe, elvis, keanu;
	public Button exitButton;
	private bool inShop;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 10; i++) {
			fertilizer.transform.GetChild (Random.Range(0, 24)).gameObject.SetActive(true);
		}
		for (int j = 0; j < 5; j++) {
			waste.transform.GetChild (Random.Range(0, 23)).gameObject.SetActive(true);
		}
		shopPanel.gameObject.SetActive (false);
		aloeBubble.gameObject.SetActive (false);
		elvisBubble.gameObject.SetActive (false);
		keanuBubble.gameObject.SetActive (false);
		shopEntrance.gameObject.SetActive (false);
		auditionEntrance.gameObject.SetActive (false);
		exitButton.onClick.AddListener (exitClicked);
		inShop = false;
	}
	
	// Update is called once per frame
	void Update () {
		decreaseSunlight ();
		decreaseTime ();
	}

	void decreaseSunlight() {
		if (sunlightSlider.value == 0) {
			gameOver ();
		}
		sunlightSlider.value -= 0.0004f;
	}

	void decreaseTime() {
		clock.fillAmount += 0.0001f;
	}
		

	public void updateSlider(string s, float f) {
		switch (s) {
		case "sunlight":
			sunlightSlider.value += f;
			break;
		case "fame":
			fameSlider.value += f;
			break;
		case "fertilizer":
			fertilizerSlider.value += f;
			break;
		}

	}

	void exitClicked () {
		shopPanel.gameObject.SetActive (false);
	}

	public void exitedShop() {
		inShop = false;
		shopEntrance.gameObject.SetActive (false);
	}

	public void enteredShop() {
		inShop = true;
		shopEntrance.gameObject.SetActive (true);
	}

	public void enteredAudition() {
		auditionEntrance.gameObject.SetActive (true);
	}

	public void enteredBubble(string person) {
		switch (person) {
		case "elvis":
			elvisBubble.gameObject.SetActive (true);
			break;
		case "keanu":
			keanuBubble.SetActive (true);
			break;
		case "aloe":
			aloeBubble.gameObject.SetActive (true);
			break;
		}
	}

	public void leftBubble(string person) {
		switch (person) {
		case "elvis":
			elvisBubble.gameObject.SetActive (false);
			break;
		case "keanu":
			keanuBubble.SetActive (false);
			break;
		case "aloe":
			aloeBubble.gameObject.SetActive (false);
			break;
		}
	}

	public void setBubbleText(string person, string message, bool late, bool famous, bool conversation) {
		switch (person) {
		case "elvis":
			if (famous) {
				elvis.text = "Is that who I think it is?!";
			} else if (conversation) {
				elvis.text = message;
			} else {
				elvis.text = "Ugh... who's that?";
			}
			break;
		case "keanu":
			if (famous) {
				keanu.text = "Omg... sign my leaf!";
			} else if (conversation) {
				keanu.text = message;
			} else {
				keanu.text = "Go back to the desert where you belong!";
			}
			break;
		case "aloe":
			if (late) {
				aloe.text = "I can’t beleaf you didn’t make it... you are such a pothead, you must not want to be an actor...";
			} else {
				aloe.text = "Wow! I can't beleaf you are so early... you must really want this acting job!";
			}
			break;
		}
	}

	public bool checkFame() {
		return fameSlider.value >= 5;
	}

	public bool checkTime() {
		return clock.fillAmount == 1;
	}

	public void openShop() {
		if (inShop) {
			shopPanel.gameObject.SetActive (true);
		}
	}

	public void gameOver() {
		SceneManager.LoadScene ("Loser");
	}

	public void victory() {
		if (clock.fillAmount == 1) {
			SceneManager.LoadScene ("Loser");
		} else {
			SceneManager.LoadScene ("Winner");
		}
	}

}
