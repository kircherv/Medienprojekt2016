using UnityEngine;
using System.Collections;

public class MovementAnim : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
        anim.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        anim.enabled = true;
        
	}
}
