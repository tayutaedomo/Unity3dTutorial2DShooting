using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

//	public float speed = 5;
//	public GameObject bullet;
	Spaceship spaceship;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();

		while (true) {
//			Instantiate (bullet, transform.position, transform.rotation);
			spaceship.Shot (transform);

//			yield return new WaitForSeconds (0.05f);
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}
	
	void Update () {
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw ("Vertical");
//		Debug.Log ("x:y, " + x + " " + y);

		Vector2 direction = new Vector2 (x, y).normalized;
//		GetComponent<Rigidbody2D> ().velocity = direction * speed;
		spaceship.Move (direction);
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		if (layerName == "Bullet (Enemy)") {
			Destroy (c.gameObject);
		}

		if (layerName == "Bullet (Enemy)" || layerName == "Enemy") {
			spaceship.Explosion ();

			Destroy (gameObject);
		}
	}
}
