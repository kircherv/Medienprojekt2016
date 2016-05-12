using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Controller2D))] 
public class Player : MonoBehaviour {



	// epi 3
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float accelerationTimeAirborne = .2f;
	public float accelerationTimeGrounded = .1f;
    public bool wjActivated = true;
    public bool stickToWall = true;
    


    //__anim
    private Vector2 scale;
    private bool facingRight= true;  //player is looking to the right by default
    private bool facingLeft;

	public float moveSpeed = 6;

	float gravity;

	float jumpVelocity;
	//Vector3 wallJumpVelocity = new Vector3 (6, 5, 0);    //second way of wj implementation, this one jumps by a fixed amount and neglects the entry angle

	Vector3 velocity;   // vector describing the velocity per deltatime
	float velocityXSmoothing;

	Controller2D controller;
    Animator animator;
	// Use this for initialization
	void Start () {
		controller = GetComponent<Controller2D> ();
        animator = GetComponent<Animator>();

        //__anim
        scale = transform.localScale;

        gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity =Mathf.Abs ( gravity) * timeToJumpApex;
		//print ("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity);

		controller.collisions.wJReset ();
	}
	
	void Update () 
	{
        animator.SetBool("tongueAttack", false);


        //epi 3 , so we do not accumulate gravity
        if (controller.collisions.above || controller.collisions.below )
		{
			velocity.y = 0;
		}



		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetKeyDown (KeyCode.Space) && controller.collisions.below) {
			velocity.y = jumpVelocity;
		} else 

			//print (" walLeft:" + controller.collisions.wallLeft + "wallRight:" + controller.collisions.wallRight);
		//walljump
		if (wjActivated &&Input.GetKeyDown (KeyCode.Space) && (controller.collisions.wallLeft || controller.collisions.wallRight)&& !controller.collisions.below) {
			velocity = Vector3.zero;

			//print ("velX:" + velocity.x + "velY:" + velocity.y);

			velocity = controller.collisions.velocityWj;    // entry and exit angle govern jump direction



			print ("velX:" + velocity.x + "velY:" + velocity.y);
			//velocity.y = jumpVelocity;

			controller.collisions.wJReset();
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)? accelerationTimeGrounded: accelerationTimeAirborne);

        // added * stickyness to  velocity.y += gravity * Time.deltaTime; to simulate wall friction

        if (stickToWall &&(controller.collisions.wallLeft || controller.collisions.wallRight)&&velocity.y<0)
        {
            velocity.y += gravity * Time.deltaTime * controller.stickyness;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }


        
        //idle , walking, jumping anim__
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            animator.SetBool("walking", true);
        }
        else
        {
            
            animator.SetBool("walking", false);
        }
       


        if ((Input.GetKey(KeyCode.Space)))
        {

            animator.SetBool("walking", false);
            animator.SetBool("jumping", true);
            
        }
        else
        {

            animator.SetBool("jumping", false);
        }
        //end__


        if ((Input.GetKeyDown(KeyCode.K)))
        {
            animator.SetBool("tongueAttack", true);

           

        }else
        {
            animator.SetBool("tongueAttack", false);
        }




        // Flipping animation
        Flip();

        controller.Move (velocity * Time.deltaTime);
        
    }


    //__anim
    void Flip()
    {

        

        if ((!facingLeft)&&velocity.x<0)
        {
            scale.x *= -1;
            transform.localScale = scale;
            facingLeft = true;
            facingRight = false;
        }
        else if((!facingRight)&&velocity.x>0)
        {
            scale.x *= -1;
            transform.localScale = scale;
            facingRight = true;
            facingLeft = false;
        }
    }

    public bool isFacingLeft()
    {
        return facingLeft;
    }
    public bool isFacingRight()
    {
        return facingRight;
    }
}
