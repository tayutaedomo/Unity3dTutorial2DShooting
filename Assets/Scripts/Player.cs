﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 5;

	public GameObject bullet;

	IEnumerator Start () {
		while (true) {
			Instantiate (bullet, transform.position, transform.rotation);
			yield return new WaitForSeconds (0.05f);
		}
	}
	
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
		Debug.Log ("x:y, " + x + " " + y);

		Vector2 direction = new Vector2 (x, y).normalized;
		GetComponent<Rigidbody2D> ().velocity = direction * speed;
	}
}
