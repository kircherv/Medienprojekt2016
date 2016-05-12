using UnityEngine;
using System.Collections;

public class FollowXY : MonoBehaviour {
	public float speed = 5f;
    public Vector2 offset;
    public GameObject target; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (target) {
            Vector3 dest = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * speed);
		}
	}
}
