using UnityEngine;
using System.Collections;

public class enemyLeftCollider : MonoBehaviour {

	public bool left=false;
	public Transform player;
	public bool hit =false;
	public float damageAmount=3;

	public float movingSpeed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
	//	Debug.Log ("left!");
	}

	void OnTriggerStay2D(Collider2D col){
	//	Debug.Log ("InLeft!");


		if (col.gameObject.tag == "Player" ) {
			transform.parent.Translate(new Vector3(movingSpeed * Time.deltaTime,0,0));
		}
	}

	public float getDamage()
	{
		return damageAmount;
	}



	void OnTriggerExit2D(Collider2D col)
	{
	//	Debug.Log ("Left left !");
		if (col.gameObject.tag == "Player") {
			transform.parent.Translate(Vector3.zero);
			hit=false;
		}
	}
}
