using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLifePointManagement : MonoBehaviour {

	public float fullHealth = 10;
	public float actualHealth = 10;
	public float timeToRespawn=2.0f;

	public Vector2 startPoint;

	private bool isDead=false; 	//damit man nicht mehrmals stirbt
	private bool damageable = true;

	private float damageMade;



	public Image healthGUI;


	// Use this for initialization
	void Start () {

		startPoint.x = transform.position.x;
		startPoint.y = transform.position.y;
	//	actualHealth = PlayerPrefs.GetFloat ("Health");
		UpdateHealth ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "enemy") {
			if (damageable) {
				damageMade = col.GetComponentInChildren<enemyLeftCollider>().getDamage();		//Schaden der bei jedem individuellen Gegner eingestellt ist (bei dem Front child)
				damage (damageMade);
				UpdateHealth ();
			}

		}

		else if (col.gameObject.tag == "death") {
			respawn();
		}

	}


	



	void damage(float damage)
	{
		if (damageable) {
			actualHealth -= damage;
			actualHealth = Mathf.Max (0, actualHealth); 	// wenn Gesundheit kleiner 0 wird 0 gewählt sonst actualhealth
			if (actualHealth == 0 && isDead == false) {
				isDead = true;
				UpdateHealth();
				respawn ();
			}

		}
		UpdateHealth ();

		damageable = false;
		Invoke ("isDamagable", 1); 	//einmal die Sekunde kann schaden hinzugefügt werden

	
	}

	void isDamagable()
	{
		damageable = true;
	}

	void respawn()
	{
		//Restart Level? level neugenerieren und person wieder zum Anfang
		actualHealth = fullHealth;
		StartCoroutine (waitFor());
		transform.parent.position = startPoint;
		isDead = false;
	}

	IEnumerator waitFor()
	{

		yield return new WaitForSeconds(timeToRespawn);
	}



	void UpdateHealth()
	{
		healthGUI.fillAmount = actualHealth / fullHealth;	//gesamtwert in Prozent umrechnen
	}

	//void OnDestroy() //Werte speichern wenn nächste Szene geladen wird
	//{
	//	PlayerPrefs.SetFloat ("Health", actualHealth);
	//}


	//funktion damaging, in der man auch updateHealth aufruft..

}
