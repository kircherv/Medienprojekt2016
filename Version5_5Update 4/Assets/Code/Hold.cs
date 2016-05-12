using UnityEngine;
using System.Collections;

public class Hold : MonoBehaviour {

    
    public bool hit = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D col)
    {

        Debug.Log("enterColliderHold");
        if (col.gameObject.tag == "Zunge")
        {
            hit = true;
            
            
            Debug.Log("triggeredTransformHold");
            col.gameObject.transform.parent = transform;
           
        }
            

        }

    void OnTriggerExit2D(Collider2D col)
    {
        if (GetComponentInChildren<ArmRotation>().holder==true)
        {
            hit = false;
            

            Debug.Log("triggeredHoldExit");
            col.gameObject.transform.parent = null;
            
            GetComponentInChildren<ArmRotation>().holder = false;


        }
    }
}



