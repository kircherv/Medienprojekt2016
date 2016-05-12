using UnityEngine;
using System.Collections;

public class fallenLeaves : MonoBehaviour {

	// es gibt 2 Möglichkeiten die Blätter zu steuern, einmal durch Angaben einer Zeit (timeToLive) oder durch ein Objekt mit dem Tag Respawn welches die Blätter bei
	// Berührung wieder zu ihrem Startpunkt schickt, der IENumerator BackToTop muss aktiviert werden, wenn man über Zeit einstellen möchte.

	private Rigidbody2D leafRigid;
	private Vector2 position;
	public float fallSpeed=0.1f;
	public float timeToLive = 10.0f;
	public float TimeToChangeDirection=1.0f;

	private bool rightOrLeft = true;
	private float startx,starty;


	// links rechts mit Zeit abfragen
	// Use this for initialization
	void Start () {
		leafRigid = transform.GetComponent<Rigidbody2D> ();
		position.x = transform.position.x;
		position.y = transform.position.y;

		startx=transform.position.x;
		starty=transform.position.y;
//		StartCoroutine(backToTheTop());
		StartCoroutine (ChangeDirection ());
	




	}
	
	// Update is called once per frame
	void Update () {

		position.y -= fallSpeed* Time.deltaTime;
		if (rightOrLeft == true) {
			moveLeft ();
		} else if (rightOrLeft == false) {
			moveRight();
		}
		leafRigid.MovePosition(position);


	}

	void moveRight()
	{

		position.x += 2 * Time.deltaTime;
		leafRigid.MovePosition (position);

	}

	void moveLeft()
	{

		position.x -= 2 * Time.deltaTime;
		leafRigid.MovePosition (position);

	}

//	IEnumerator backToTheTop()
//	{
//
//		yield return new WaitForSeconds (timeToLive);
//		sendBack ();
//		position.x = startx;
//		position.y = starty;
//	}

	IEnumerator ChangeDirection()
	{
		yield return new WaitForSeconds (TimeToChangeDirection);
		if (rightOrLeft == true) {
			rightOrLeft = false;
		} else if (rightOrLeft == false) {
			rightOrLeft = true;
		}

		StartCoroutine (ChangeDirection ());
	}

	void ChangeDirectionCol()
	{
		if (rightOrLeft == true) {
			rightOrLeft = false;
		} else if (rightOrLeft == false) {
			rightOrLeft = true;
		}
	}


	void sendBack()
	{
		leafRigid.MovePosition(new Vector2(startx,starty));
		//StartCoroutine(backToTheTop());
	}




    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            //Debug.Log ("blatt hat berührt!");
            StopCoroutine(ChangeDirection());
            sendBack();
            position.x = startx;
            position.y = starty;
        }
        else if (col.gameObject.tag == "Leaf")
        {
            ChangeDirectionCol();
            //Debug.Log("BlattGetroffen");
        }
    }


	

}
