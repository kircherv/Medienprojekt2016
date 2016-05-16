using UnityEngine;
using System.Collections;

public class TurtleEnemy : MonoBehaviour {

	public bool facingLeft = false;
	public int fieldOfView = 15;
	public float movingSpeed=1.0f;
	public float jumpSpeed = 5.0f;
    public float rageWalkingSpeed = 15.0f;

	public bool playerSeen = false;
	public bool isRaging = false;

    private bool jumped = false;
    private bool turtleIsDying = false;
	private Vector2 enemyViewDistance;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
    }
	
	// Update is called once per frame
	void Update () {
		enemyViewDistance.x = transform.position.x;
		enemyViewDistance.y = transform.position.y;
        if (turtleIsDying == false)
        {
            Raycasting();
        }
       
	}

 

	void Raycasting()
	{
		playerSeen = false;


		if (facingLeft) {


			enemyViewDistance.x -= fieldOfView;
			Debug.DrawLine(transform.position, enemyViewDistance, Color.green); 
			playerSeen = Physics2D.Linecast(transform.position, enemyViewDistance, 1<<LayerMask.NameToLayer("Character"));
			if(playerSeen)
			{

				RageOnPlayer();
				
			}
			else {

				if(isRaging==false){
				playerSeen=false;
				Walk ();
				}
                else
                {
                    RageOnPlayer();
                }
			}
		}

		if (facingLeft==false) {
			
			enemyViewDistance.x += fieldOfView;
			Debug.DrawLine(transform.position, enemyViewDistance, Color.green); 
			playerSeen = Physics2D.Linecast(transform.position, enemyViewDistance, 1<<LayerMask.NameToLayer("Character"));
			if(playerSeen)
			{

				RageOnPlayer();
				
			}
			else {
                if (isRaging == false)
                {
                    playerSeen = false;
                    Walk();
                }
                else {
                    RageOnPlayer();
                }
			}
			
		}



	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (isRaging== false) {
			if (col.gameObject.tag == "Hindernis") {
				Turn ();
			}

            if (turtleIsDying == true && col.gameObject.tag == "Zunge")
            {
                die();
            }
        }
		else {
            if (col.gameObject.tag == "Hindernis")      // animation fehlt , hier ist der Fall dass der Gegner auf eine Wand oder den Player trifft
            {

                turtleIsDying = true;
                StartCoroutine(fallOnBack());
                isRaging = false;
                jumped = false;

            }
            else if (col.gameObject.tag == "Player")
            {
                turtleIsDying = true;
                StartCoroutine(theMomentAfterWeHitThePlayer());
                isRaging = false;
                jumped = false;

            }
         
		}
  
	}




	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {

			isRaging = false;
		}

	}


	void Walk()
	{
		transform.Translate(new Vector3(movingSpeed * Time.deltaTime, 0,0));
	}

	void Turn()
	{
		transform.Rotate(new Vector3(0,-180,0));
		if (facingLeft) {
			facingLeft = false;
		} 
		else {
			facingLeft=true;
		}
	}




	void RageOnPlayer()
	{
		isRaging=true;
        if (jumped == false) 
        {
            turtleIsDying = true;
            jump();
            
            StartCoroutine(playerHit());

        }
        transform.Translate(new Vector3(rageWalkingSpeed * Time.deltaTime, 0, 0));
    }
	

	IEnumerator playerHit()
	{
		
        
        yield return new WaitForSeconds(1f);
        turtleIsDying = false;
        StopCoroutine (playerHit ());
	}

    IEnumerator waitAndTurn()
    {
        jump();
        yield return new WaitForSeconds(1f);
        Turn();
        turtleIsDying = false;
        StopCoroutine(waitAndTurn());
    }


    void jump()
    {
        turtleIsDying = true;
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        jumped = true;
    }

    IEnumerator fallOnBack()
    {
        gameObject.GetComponent<Animator>().SetBool("gotHit", true);
        jump();
        if (facingLeft)
        {
            transform.Translate(new Vector3(-1.5f, 0, 0));
        }
        else
        {
            transform.Translate(new Vector3(1.5f, 0, 0));
        }
        transform.Rotate(new Vector3(0, 0, 180));
        yield return new WaitForSeconds(5.0f);
        jumped = false;
        jump();
        transform.Rotate(new Vector3(0, 0, 180));
        Turn();
        turtleIsDying = false;
        jumped = false;
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        StopCoroutine(fallOnBack());

    }

    IEnumerator theMomentAfterWeHitThePlayer()
    {
        gameObject.GetComponent<Animator>().SetBool("gotHit", true);
        jump();
        if (facingLeft)
        {
            transform.Translate(new Vector3(-1.5f, 0, 0));
        }
        else
        {
            transform.Translate(new Vector3(1.5f, 0, 0));
        }
        
        yield return new WaitForSeconds(2.5f);    
        turtleIsDying = false;
        jumped = false;
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        StopCoroutine(fallOnBack());
    }

    void die() {

        Destroy(gameObject);

    }




}
