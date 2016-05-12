using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

    public float damageAmount = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public float getDamage()
    {
        return damageAmount;
    }
}
