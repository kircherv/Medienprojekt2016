using UnityEngine;
using System.Collections;

public class enemyRightCollider : MonoBehaviour {
	
	
	public Transform player;
	public bool hit =false;

 
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag =="Player") 
		{
			Debug.Log("right!");
			transform.parent.Rotate(new Vector3(0,-180,0));
			hit=true;
		}
	}
	
	//void OnTriggerStay2D(Collider2D col){
	//
	//	if (col.gameObject.tag == "Player" ) {
	//		transform.parent.Translate(new Vector3(5 * Time.deltaTime,0,0));
	//		Debug.Log ("InRight!");
	//	}
	//}


	
	void OnTriggerExit2D(Collider2D col)
	{

		if (col.gameObject.tag == "Player") {
			Debug.Log ("Left right!");
			transform.parent.Translate(Vector3.zero);
			hit=false;
		}
	}


}
