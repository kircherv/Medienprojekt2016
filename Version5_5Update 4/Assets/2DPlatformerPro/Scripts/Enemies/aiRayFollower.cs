using UnityEngine;
using System.Collections;

public class aiRayFollower : MonoBehaviour {

	public Transform sightStart, sightEnd,target;

	private SpriteRenderer enemyRenderer;

	private float movingSpeed = 2;

	private Vector3 pos;

	public bool erwischt = false;

	public bool playerIsLeft =false;
	public bool playerIsRight = false;

	private bool alreadyHit = false;


	// Use this for initialization
	void Start () {
		enemyRenderer = target.GetComponent<SpriteRenderer> ();
	//	Raycasting ();
	}
	
	// Update is called once per frame
	void Update () {
	//	Raycasting ();
	

	}

	void Raycasting()
	{	
		erwischt = false;

		Rigidbody2D r = (Rigidbody2D)GetComponent (typeof(Rigidbody2D));
		Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);
		erwischt = Physics2D.Linecast (sightStart.position, sightEnd.position, 1<< LayerMask.NameToLayer("Player"));
	
		if (erwischt) {
			follow ();
		} else {
			erwischt=false;


		}

	}




	void follow()
	{

		transform.LookAt (target.position);
		transform.Rotate (new Vector3 (0, -90, 0));

		Vector3 richtung = transform.TransformDirection(Vector3.forward);
		Vector3 delta = target.position - transform.position;


		transform.Translate(new Vector3(movingSpeed * Time.deltaTime,0,0));

	}


	void OnCollisionStay2D(Collision2D col){

		if (col.gameObject.tag == "Player") {
			Debug.Log("aua!");
			if(alreadyHit==false)
			{
				StartCoroutine(damageEffect ());
			}
			alreadyHit=true;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player") {
			Debug.Log("nicht mehr aua!");
			alreadyHit=false;
		}
	}

	void OnCollisionEnter2D(Collision2D col)
	{

		if (col.gameObject.tag == "Player"){
			Debug.Log("GETROFFEN!");
			StartCoroutine(damageEffect ());
			//damage ();
		}
	}

	void damage()
	{
		Debug.Log ("Aua!");
		// hp - damage..
	}

	IEnumerator damageEffect()
	{
		enemyRenderer.enabled = false;
		yield return new WaitForSeconds (0.2F);
		enemyRenderer.enabled = true;
		yield return new WaitForSeconds (0.2F);
		enemyRenderer.enabled = false;
		yield return new WaitForSeconds (0.2F);
		enemyRenderer.enabled = true;
		yield return new WaitForSeconds (0.2F);
		enemyRenderer.enabled = false;
		yield return new WaitForSeconds (0.2F);
		enemyRenderer.enabled = true;
	}

}
