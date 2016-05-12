using UnityEngine;
using System.Collections;

public class Emitter : MonoBehaviour {

    public GameObject waterParticle;
    public Transform Container;
    public float particlesPerSecond = 10;
    public float particlesForce = 3.0f;
    public int maxParticles = 10000;

    int created = 0;

	// Use this for initialization
	void Start () {
        //StartCoroutine(spawn());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable() {
        StartCoroutine("spawn");
    }

    void OnDisable() {
        StopCoroutine("spawn");
    }

    IEnumerator spawn() {
        while (created < maxParticles) {
            GameObject go = Instantiate(waterParticle, this.transform.position, this.transform.rotation) as GameObject;
            if (Container) {
                go.transform.parent = Container;
            }
            Rigidbody r = go.GetComponent<Rigidbody>();
            //r.mass += Random.Range(1,100);
            //go.GetComponent<SphereCollider>().radius += Random.Range(-0.2f, 0.2f);
            r.AddForce(particlesForce * Random.insideUnitSphere, ForceMode.Impulse);
            created++;
            yield return new WaitForSeconds(1.0f / particlesPerSecond);
        }
    }
}
