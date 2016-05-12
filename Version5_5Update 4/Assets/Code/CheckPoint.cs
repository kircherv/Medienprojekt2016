using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	private Vector2 koordinaten;
	//private Collider2D collider;


	// Use this for initialization
	void Start () {

		//collider = GetComponent<BoxCollider2D> ();
		koordinaten.x = transform.position.x;
		koordinaten.y = transform.position.y;

	}

	public Vector2 getCoordinates()
	{
		return koordinaten;
	}



//	void enterCheckPoint (Collider2D col)
//	{
//		if (collider.IsTouching (col.gameObject.tag == "Player")) {
//			Debug.Log("checkpoiiiint");
//		}
//
//	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag =="Player") {
			Debug.Log("checkPOINT");
			col.gameObject.GetComponent<PlayerLifePointManager>().changeRespawnPoint(koordinaten);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
