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

			GetComponent<AudioSource> ().Play ();

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

//		spaceship.Move (direction);

		//Clamp ();

		Move (direction);
	}

//	void Clamp() {
//		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
//		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
//		Vector2 pos = transform.position;
//
//		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
//		pos.y = Mathf.Clamp (pos.y, min.y, max.y);
//
//		transform.position = pos;
//	}

	void Move(Vector2 direction) {
		Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
		Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
		Vector2 pos = transform.position;

		pos += direction * spaceship.speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		transform.position = pos;
	}

	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

		if (layerName == "Bullet (Enemy)") {
			Destroy (c.gameObject);
		}

		if (layerName == "Bullet (Enemy)" || layerName == "Enemy") {
			FindObjectOfType<Manager> ().GameOver ();

			spaceship.Explosion ();

			Destroy (gameObject);
		}
	}
}
