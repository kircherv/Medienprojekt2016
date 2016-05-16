using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

    public bool facingLeft = false;
    public bool playerSeen = false;
    public float movingSpeed = 1.0f;
    public int fieldOfView = 15;
    public bool standStill = false;

    private bool stopRaycast = false;
    private Vector2 enemyViewDistance;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        enemyViewDistance.x = transform.position.x;
        enemyViewDistance.y = transform.position.y;

        if (stopRaycast == false)
        {
            Raycasting();

        }

    }

    void Raycasting()
    {
        playerSeen = false;


        if (facingLeft)
        {


            enemyViewDistance.x -= fieldOfView;
            Debug.DrawLine(transform.position, enemyViewDistance, Color.green);
            playerSeen = Physics2D.Linecast(transform.position, enemyViewDistance, 1 << LayerMask.NameToLayer("Character"));


            if (standStill == false)
            {
                Walk();
            }
               
            
        }

        if (facingLeft == false)
        {

            enemyViewDistance.x += fieldOfView;
            Debug.DrawLine(transform.position, enemyViewDistance, Color.green);
            playerSeen = Physics2D.Linecast(transform.position, enemyViewDistance, 1 << LayerMask.NameToLayer("Character"));



            if (standStill == false)
            {
                Walk();
            }


        }   
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        
            if (col.gameObject.tag == "Hindernis")
            {
            Debug.Log("turn");
                Turn();
            }
        
    }

            void Walk()
    {
        transform.Translate(new Vector3(movingSpeed * Time.deltaTime, 0, 0));
    }

   public void Turn()
    {
        transform.Rotate(new Vector3(0, -180, 0));
        if (facingLeft)
        {
            facingLeft = false;
        }
        else
        {
            facingLeft = true;
        }
    }

    public void stopMoving()
    {
        standStill = true;
    }

    public void startMoving()
    {
        standStill = false;
    }

    public bool playerInFieldOfView()
    {
        return playerSeen;
    }

    public bool returnDirection()
    {
        return facingLeft;
    }

    public void stopRaycasting()
    {
        stopRaycast = true;
    }

    public void startRaycasting()
    {
        stopRaycast = false;
    }

}
