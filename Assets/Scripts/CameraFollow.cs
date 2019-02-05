using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject Player;

	private float cameraX, cameraY, cameraZ;


	// Use this for initialization
	void Start () {
		cameraY = transform.position.y;
		cameraX = transform.position.x;
		cameraZ = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.transform.position.x >= -4) {
			Vector3 fromPosition = new Vector3 (cameraX, cameraY, cameraZ);
			Vector3 toPosition = new Vector3 (Player.transform.position.x, cameraY, cameraZ);
			transform.position = Vector3.Lerp (fromPosition, toPosition, 1.0f);
		}
	}
}
