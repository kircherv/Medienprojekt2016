using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerLifePointManager : MonoBehaviour {

	public float fullHealth = 10;
	public float actualHealth = 10;
	public float timeToRespawn=2.0f;
    public float lives=3;
    public int pointPenaltyOnDeathP;

    private AudioSource audio;
	public AudioClip youDiedClip;


	public Image youDied;
    public Image GameOverImage;
	
	public Vector2 startPoint;
	
	private bool isDead=false; 	//damit man nicht mehrmals stirbt
	private bool damageable = true;
	
	private float damageMade;
	
	
	
	public Image healthGUI;
	
	
	// Use this for initialization
	void Start () {


		audio = GetComponent<AudioSource> ();
		youDied.enabled = false;
        GameOverImage.enabled = false;
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
		if (col.gameObject.tag == "enemy" ) {
			if (damageable) {
				damageMade = col.GetComponentInChildren<EnemyDamage>().getDamage();		//Schaden der bei jedem individuellen Gegner eingestellt ist (bei dem Front child)
				damage (damageMade);
				UpdateHealth ();
			}
			
		}
		
		else if (col.gameObject.tag == "death") {
            if (lives <= 0)
            {
                GameOver();
            } else
			respawn();
		}

        else if (col.gameObject.tag == "Lives")
        {
            lives += 1;
        }		
	}

	public void changeRespawnPoint(Vector2 a)		//Respawnort verändern
	{

			startPoint.x = a.x;
			startPoint.y = a.y;
		
	}
	
	
	
	
	
	void damage(float damage)
	{
		if (damageable) {
			actualHealth -= damage;
			actualHealth = Mathf.Max (0, actualHealth); 	// wenn Gesundheit kleiner 0 wird 0 gewählt sonst actualhealth
			if (actualHealth == 0 && isDead == false) {
				isDead = true;
                if (lives <= 0)
                {
                    GameOver();
                }
                else
                {
                    respawn();
                    UpdateHealth();
                }
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
        if (lives <= 0)
        {
            GameOver();
        }
        else if(lives>0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Player>().enabled = false;

            audio.PlayOneShot(youDiedClip, 1f);


            youDied.enabled = true;

            ScoreManager.ExtraLife(-1);
            ScoreManagerPapier.AddPoints(-pointPenaltyOnDeathP);
            lives--;
            StartCoroutine(waitFor());
        }

    }
	
	IEnumerator waitFor()
	{
		
		yield return new WaitForSeconds(timeToRespawn);
		actualHealth = fullHealth;
		UpdateHealth();
		transform.position = startPoint;
		GetComponent<SpriteRenderer> ().enabled = true;
		GetComponent<Player> ().enabled = true;
		youDied.enabled = false;
		//youDiedClip.enabled = false;
		isDead = false;
	}
	
	
	
	void UpdateHealth()
	{
		healthGUI.fillAmount = actualHealth / fullHealth;	//gesamtwert in Prozent umrechnen
	}

    //void UpdateLives()
    //{
    //    if (GetComponentInChildren<ExtraLives>().onTrigger == true)
    //    {
    //        lives += 1;
    //    }
    //}
    void GameOver()
    {
        
        
            
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Player>().enabled = false;
            audio.PlayOneShot(youDiedClip, 1f);
            GameOverImage.enabled = true;
            isDead = true;
        
        
    }
	
	//void OnDestroy() //Werte speichern wenn nächste Szene geladen wird
	//{
	//	PlayerPrefs.SetFloat ("Health", actualHealth);
	//}
	
	
	//funktion damaging, in der man auch updateHealth aufruft..
	
}

