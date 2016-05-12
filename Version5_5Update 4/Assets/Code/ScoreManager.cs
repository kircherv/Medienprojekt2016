using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : PlayerLifePointManager
{

    
    public static float actualLife;

    
    Text textLives;

	
	void Start () {
       
        textLives = GetComponent<Text>();
        
        actualLife = lives;
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        textLives.text = "" + actualLife;
        
    }

    

    public static void ExtraLife (float LiveToAdd)
    {
        actualLife += LiveToAdd;
    }
    
}
