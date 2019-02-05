using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionScripts : MonoBehaviour {
	public Button okButton;
	public bool clicked;

	void Start () {
		clicked = false;
		okButton.onClick.AddListener (okClicked);
	}
	
	void okClicked() {
		clicked = true;
		SceneManager.LoadScene ("main");
	}
}
