using UnityEngine;
using System.Collections;

public class enemyUpCollider : MonoBehaviour {

	private Rigidbody2D parentRigid;
	public bool isUp = false;
	public Transform player;
	public float jumpSpeed = 350.0f;

	// Use this for initialization
	void Start () {
		parentRigid = transform.parent.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){

		Debug.Log ("UP");
		if (col.gameObject.tag == "Player" && isUp==false) {
			isUp=true;
			Jump ();
		}
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.gameObject.tag == "Player" && isUp==false) {

			isUp = true;
		}
	}

	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			isUp = false;

		}
	}

	void Jump()
	{
		parentRigid.AddForce (Vector2.up * jumpSpeed);
	}
}
