using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sunlightClick : MonoBehaviour {
	public Slider sunlightSlider;
	public Slider fertilizerSlider;
	public Button sunlightButton1;
	public Button sunlightButton2;
	public Button sunlightButton3;
	public Button sunlightButton4;
	public Button sunlightButton5;
	public float amount;

	void Start() {
		sunlightButton1.onClick.AddListener (sunlightClicked);
		sunlightButton2.onClick.AddListener (sunlightClicked);
		sunlightButton3.onClick.AddListener (sunlightClicked);
		sunlightButton4.onClick.AddListener (sunlightClicked);
		sunlightButton5.onClick.AddListener (sunlightClicked);
	}

	void sunlightClicked() {
		if (fertilizerSlider.value == 0) {

		} else {
			amount = Random.Range (-4.0f, 4.0f);
			sunlightSlider.value += amount;
			fertilizerSlider.value -= 1.0f;
		}
	}
		
}
