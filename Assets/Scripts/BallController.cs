using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BallController : MonoBehaviour {
	public int force;
	Rigidbody2D rigid;
	int scoreP1;
	int scoreP2;
	Text ScoreUIP1;
	Text ScoreUIP2;
	GameObject panelSelesai;
	Text txtPemenang;
	AudioSource audio;
	public AudioClip hitSound;
	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		scoreP1 = 0;
		scoreP2 = 0;
		rigid = GetComponent<Rigidbody2D>();
		Vector2 arah = new Vector2(2, 0).normalized;
		rigid.AddForce (arah * force);
		ScoreUIP1 = GameObject.Find ("Score1").GetComponent<Text> ();
		ScoreUIP2 = GameObject.Find ("Score2").GetComponent<Text> ();
		panelSelesai = GameObject.Find ("PanelSelesai");
		panelSelesai.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ResetBall(){
		transform.localPosition = new Vector2 (0, 0);
		rigid.velocity = new Vector2(0, 0);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		audio.PlayOneShot (hitSound);
		if (coll.gameObject.name == "TepiKanan") {
			scoreP1 += 1;
			TampilkanScore ();

			if (scoreP1 == 5) {
				panelSelesai.SetActive (true);
				txtPemenang = GameObject.Find ("Pemenang").GetComponent<Text> ();
				txtPemenang.text = "Player Hijau Menang!";
				Destroy (gameObject);
				return;
			}

			ResetBall();
			Vector2 arah = new Vector2(2, 0).normalized;
			rigid.AddForce(arah * force);
	}
		if (coll.gameObject.name == "TepiKiri") {
			scoreP2 += 1;
			TampilkanScore ();

			if (scoreP2 == 5) {
				panelSelesai.SetActive (true);
				txtPemenang = GameObject.Find ("Pemenang").GetComponent<Text> ();
				txtPemenang.text = "Player Biru Menang!";
				Destroy (gameObject);
				return;
			}

			ResetBall();
			Vector2 arah = new Vector2(-2, 0).normalized;
			rigid.AddForce(arah * force);

		}

		if (coll.gameObject.name == "Pemukul1" || coll.gameObject.name == "Pemukul2") {

			float sudut = (transform.position.y - coll.transform.position.y) * 5f;
			Vector2 arah = new Vector2 (rigid.velocity.x, sudut).normalized;
			rigid.velocity = new Vector2 (0, 0);
			rigid.AddForce (arah * force * 2);
		}
}

	void TampilkanScore() {
		Debug.Log ("Score P1: " + scoreP1 + " Score P2: " + scoreP2);
		ScoreUIP1.text = scoreP1 + "";
		ScoreUIP2.text = scoreP2 + "";
}
}
