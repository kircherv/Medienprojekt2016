using UnityEngine;
using System.Collections;

public class TurtleBehaviour : MonoBehaviour {

    // benötigt EnemyMovement

    public float jumpSpeed = 200.0f;
    public float rageWalkingSpeed = 15.0f;

    public bool playerSeen = false;
    public bool isRaging = false;

    public bool jumped = false;
    private bool turtleIsDying = false;
    private bool facingLeft = false;
    private bool stop = false;
    
    


    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        

    }

    // Update is called once per frame
    void Update()
    {
        facingLeft = GetComponent<EnemyMovement>().returnDirection();
        playerSeen = GetComponent<EnemyMovement>().playerInFieldOfView();

        if (stop == false) { 
        if (playerSeen)
        {
            GetComponent<EnemyMovement>().stopMoving();
            RageOnPlayer();
        }

        else
        {
            if (turtleIsDying == true)
            {
                GetComponent<EnemyMovement>().stopMoving();
            }
            else
            {
                if (isRaging == false)
                {
                    GetComponent<EnemyMovement>().startMoving();
                }
                else
                {
                    GetComponent<EnemyMovement>().stopMoving();

                    RageOnPlayer();
                }
            }
        }
    }
 }
    void RageOnPlayer()
    {
        isRaging = true;
        if (jumped == false)
        {
            GetComponent<EnemyMovement>().stopRaycasting();
            jump();
            StartCoroutine(playerHit());

        }
        transform.Translate(new Vector3(rageWalkingSpeed * Time.deltaTime, 0, 0));
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (isRaging == false)
        {
           
            if (turtleIsDying == true && col.gameObject.tag == "Zunge")
            {
                die();
            }
        }
        else
        {
            if (col.gameObject.tag == "Hindernis")      // animation fehlt , hier ist der Fall dass der Gegner auf eine Wand oder den Player trifft
            {

                  turtleIsDying = true;
                GetComponent<EnemyMovement>().stopRaycasting();
                StartCoroutine(fallOnBack());
                isRaging = false;
                jumped = false;

            }
            else if (col.gameObject.tag == "Player")
            {
                
                GetComponent<EnemyMovement>().stopRaycasting();
                StartCoroutine(theMomentAfterWeHitThePlayer());
                isRaging = false;
                jumped = false;

            }

        }

    }




    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {

            isRaging = false;
        }

    }



    IEnumerator playerHit()
    {
        stop = true;
        yield return new WaitForSeconds(1f);
        stop = false;
        GetComponent<EnemyMovement>().startRaycasting();
        turtleIsDying = false;
        
        StopCoroutine(playerHit());
    }

    void jump()
    {
      
        transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpSpeed);
        jumped = true;
    }

    IEnumerator fallOnBack()
    {
        stop = true;
        gameObject.GetComponent<Animator>().SetBool("gotHit", true);
        jump();
        GetComponent<EnemyMovement>().stopRaycasting();

         turtleIsDying = true;
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
        //  GetComponent<EnemyMovement>().Turn();
        GetComponent<EnemyMovement>().startRaycasting();
        turtleIsDying = false;
        jumped = false;
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        stop = false;
        StopCoroutine(fallOnBack());

    }

    IEnumerator theMomentAfterWeHitThePlayer()
    {
        stop = true;
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

        yield return new WaitForSeconds(5f);
        GetComponent<EnemyMovement>().startRaycasting();
        turtleIsDying = false;
        jumped = false;
        gameObject.GetComponent<Animator>().SetBool("gotHit", false);
        stop = false;
        StopCoroutine(fallOnBack());
    }

    void die()
    {

        Destroy(gameObject);

    }

}
