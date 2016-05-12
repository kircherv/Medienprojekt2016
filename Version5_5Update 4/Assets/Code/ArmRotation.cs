
using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour
{

    private float rotationOffset = 0;
    public GameObject crosshair;
    public float distanceCrosshair = 100;

    public GameObject projectile;
    public float projectileDelay = 0.03f;
    private bool isFired = false;
    private bool facingRight = true;  //player is looking to the right by default
    private bool facingLeft;
    private bool hit;
    public bool holder;
    private bool onCollision;
    


    private Vector2 scale;
    Vector3 velocity;
    
    

    void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("tongueReady", true);
    }

    

    // Update is called once per frame
    void Update()
    {

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition)
         - transform.position;

        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (this.transform.parent.localScale.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);


        }
        else
        {
            transform.rotation = (Quaternion.Euler(0f, 0f, (-1) * (rotZ + rotationOffset)));
        }

        crosshair.transform.position = transform.position + difference * distanceCrosshair;

       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

            
           
                
                //projectile.transform.position = Vector3.Lerp(transform.position, crosshair.transform.position, 1.0f);
                StartCoroutine(onMouseDown());
                

               

            } 
           
    
        


        if (isFacingLeft())
        {
            rotationOffset = 180;
        }

        if (isFacingRight())
        {
            rotationOffset = 0;
        }

    }

    IEnumerator onMouseDown()
    {

        Debug.Log("OnMouseDown");
        if (isFired == false)
        {
            Debug.Log("isFired=false");
            
            

            gameObject.GetComponent<Animator>().SetBool("tongueReady", false);
            gameObject.GetComponent<Animator>().SetBool("tongueShot", true);
            yield return new WaitForSeconds(2f);

            hitOrNot();
            //projectile.transform.position = transform.position;

            //projectile.transform.Translate(crosshair.transform.position);

        }else if (isFired == true)
        {
            Debug.Log("isFired=true");
            //if(isFired ==false)
            //{
            
            
            
            gameObject.GetComponent<Animator>().SetBool("tongueReady", true);
            

            yield return new WaitForSeconds(0.3f);
        }



    }

   

    void hitOrNot()
    {
        Debug.Log("in hitOrNot");
        if (onCollision == true)
        {
            if (hit == true)
            {
                Debug.Log(" hit=true");

                gameObject.GetComponent<Animator>().SetBool("tongueShot", false);
                gameObject.GetComponent<Animator>().SetBool("tongueHold", true);
                isFired = true;

            }
            else if (hit == false)
            {

                Debug.Log("hit=false");

                gameObject.GetComponent<Animator>().SetBool("tongueShot", false);
                gameObject.GetComponent<Animator>().SetBool("tongueHold", false);
                gameObject.GetComponent<Animator>().SetBool("tongueReady", true);
                isFired = false;


            }
        }else
        {
            gameObject.GetComponent<Animator>().SetBool("tongueShot", false);
            gameObject.GetComponent<Animator>().SetBool("tongueReady", true);
        }

    }

    

    void onTriggerEnter2D(Collider2D col)
    {
        onCollision = true;
        if (col.gameObject.tag == "Enemy")
        {

            hit = false;
        }
        if (col.gameObject.tag == "MainGeometry")
        {

            hit = true;
        }
        
        
        


    }
    void onTriggerExit2D(Collider2D col)
    {
        //Debug.Log("onTriggerEnter armRotation coBool=false");
        //col.gameObject.transform.parent = GetComponent<Player>().transform;
        //colBool = false;
        //hit = false;
        onCollision = false;
    }

    void Flip()
    {



        if ((!facingLeft) && velocity.x < 0)
        {
            scale.x *= -1;
            transform.localScale = scale;
            facingLeft = true;
            facingRight = false;
        }
        else if ((!facingRight) && velocity.x > 0)
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

    
    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    Debug.Log("enterColliderPlayer");
        
            
    //        if (isFired == true)
    //        {
    //            Debug.Log("triggeredisFiredPlayer");
    //            if (col.gameObject.gameObject.tag == "MainGeometry")
    //            {
    //                Debug.Log("triggeredTransformPlayer");
    //                col.gameObject.transform.parent = transform;
    //            }
    //        }

        
    //}
    //void OnTriggerExit2D(Collider2D col)
    //{
    //    Debug.Log("ExitCollider");
    //        if (isFired == false)
    //        {
    //            Debug.Log("ExitColliderisFired");
    //            if (col.gameObject.gameObject.tag == "MainGeometry")
    //                {
    //                    Debug.Log("triggeredExitPlayer");
    //                    col.gameObject.transform.parent = null;
    //                }
    //        }

        
    //}

}