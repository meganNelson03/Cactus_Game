using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
	public float speed;
	public GameObject gameManager;
	public Button exitButton;

	private Manager manager;
	private Rigidbody2D rigidBody;
	private bool waddle, leftWaddle, rightWaddle, grounded, keyEnabled, inShop, talking, reachedAudition;
	private int lastWaddle, direction, turn, messageNum;
	private float playerYPos, xV, yV, zV;
	private List<string> keanu, elvis;

	// Use this for initialization
	void Start () {
		manager = gameManager.GetComponent<Manager> ();
		rigidBody = GetComponent<Rigidbody2D> ();
		waddle = false;
		leftWaddle = true;
		rightWaddle = false;
		grounded = true;
		lastWaddle = 0;
		direction = 0;
		keyEnabled = true;
		reachedAudition = false;
		playerYPos = transform.position.y;
		talking = false;
		turn = 0;
		messageNum = 0;
		elvis = new List<string> ();
		elvis.Add ("...Oh yeah, how’s your soil doing?");
		elvis.Add ("Yeah - ceramic is sooo in! Really breathable.");
		elvis.Add ("Oh, this? I just started drinking Fiji water and they really spruced up");
		elvis.Add ("Grapevines is such a liar. Do you hear what she did to Gloriana?");
		keanu = new List<string> ();
		keanu.Add ("It’s good, just got repotted. Do you like it?");
		keanu.Add ("Uhm, anyways, I really like what you did with your leaves - I’m green with envy!");
		keanu.Add ("Really? I’ve been hearing through the grapevine that Fiji water is bad for you...");
		keanu.Add ("Hmm, now that I think of it, she was clingy...");
		InvokeRepeating ("startConversation", 0.0f, 3.0f);
	}

	void Update () {
		bool leftDown = Input.GetKeyDown ("a") || Input.GetKeyDown ("left");
		bool rightDown = Input.GetKeyDown ("d") || Input.GetKeyDown ("right");
		bool leftUp = Input.GetKeyUp ("a") || Input.GetKeyUp ("left");
		bool rightUp = Input.GetKeyUp ("d") || Input.GetKeyUp ("right");
		inShop = Input.GetKey ("e");

		if (!grounded) {
			if (transform.position.y > playerYPos + 4.0f) {
				grounded = false;
				yV = -speed * 10;
			}
		}

		if (inShop) {
			if (reachedAudition) {
				manager.victory ();
			} else {
				manager.openShop ();
				keyEnabled = false;
			}
		}

		keyEnabled = true;
			
		if (keyEnabled) {
			if (leftDown || rightDown) {
				if (leftDown) {
					direction = 180;
				} else {
					direction = 0;
				}
				waddle = true;
			} else if (leftUp || rightUp) {
				waddle = false;
				lastWaddle = 0;
			} else if (waddle) {
				if (rightWaddle) {
					lastWaddle -= 4;
					if (lastWaddle == -16) {
						leftWaddle = true;
						rightWaddle = false;
					}
				} else if (leftWaddle) {
					lastWaddle += 4;
					if (lastWaddle == 16) {
						leftWaddle = false;
						rightWaddle = true;
					}
				}
			}
			transform.rotation = Quaternion.Euler (0, direction, lastWaddle);
		}
	}
		
	void FixedUpdate () {
		if (keyEnabled) {
			float xV = 0f;
			float zV = 0f;

			bool left = Input.GetKey ("a") || Input.GetKey ("left");
			bool right = Input.GetKey ("d") || Input.GetKey ("right");
			bool space = Input.GetKey ("space");

			if (left) {
				xV = -speed;
				yV = jumpVelocity (space);
			} else if (right) {
				xV = speed;
				yV = jumpVelocity (space);
			} else if (space) {
				if (grounded) {
					yV = jumpVelocity (space);
				}
			}
			rigidBody.velocity = new Vector3 (xV, yV, zV);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		string triggeredObject = other.gameObject.tag;
		switch (triggeredObject) {
		case ("Fertilizer"):
			manager.updateSlider ("fertilizer", 1.0f);
			other.gameObject.SetActive (false);
			break;	
		case ("Toxic Waste"):
			manager.updateSlider ("sunlight", -1.0f);
			other.gameObject.SetActive (false);
			break;
		case ("Ground"):
		case ("Platform"):
			grounded = true;
			break;
		case ("Shop"):
			manager.enteredShop ();
			break;
		case ("A"):
			manager.enteredBubble ("aloe");
			manager.setBubbleText ("aloe", "", manager.checkTime (), false, false);
			break;
		case ("E&K"):
			talking = true;
			break;
		case ("Audition"):
			manager.enteredAudition ();
			reachedAudition = true;
			break;
		case ("Sky"):
			yV = jumpVelocity (false);
			break;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		string triggeredObject = other.gameObject.tag;
		switch (triggeredObject) {
		case ("Shop"):
			manager.exitedShop ();
			break;
		case ("A"):
			manager.leftBubble ("aloe");
			break;
		case ("E&K"):
			talking = false;
			break;
		}
	}

	private void startConversation() {
		if (!talking) {
			manager.leftBubble ("elvis");
			manager.leftBubble ("keanu");
			if (messageNum == 4) {
				messageNum = 0;
			}
			if (turn == 0) {
				manager.enteredBubble ("elvis");
				manager.setBubbleText ("elvis", elvis [messageNum], false, false, true);
				turn++;
			} else {
				manager.enteredBubble ("keanu");
				manager.setBubbleText ("keanu", keanu [messageNum], false, false, true);
				turn--;
				messageNum++;
			}
		} else {
			manager.leftBubble ("elvis");
			manager.leftBubble ("keanu");
			if (turn == 0) {
				manager.enteredBubble ("elvis");
				manager.setBubbleText ("elvis", "", false, manager.checkFame(), false);
				turn++;
			} else {
				manager.enteredBubble ("keanu");
				manager.setBubbleText ("keanu", "", false, manager.checkFame(), false);
				turn--;
			}
		}
	}

	private float jumpVelocity(bool space) {
		float new_yV = 0f;
		if (space) {
			new_yV = speed;
			grounded = false;
		} else {
			new_yV = -speed;
		}
		return new_yV;
	}

}

