using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManagerPapier : MonoBehaviour
{

    public static int score;


    Text textScore;


   // Use this for initialization

   void Start()
    {
        textScore = GetComponent<Text>();

        score = 0;


    }

   // Update is called once per frame
    void Update()
    {
        if (score < 0)
            score = 0;


        textScore.text = "" + score;


    }

    public static void AddPoints(int pointsToAdd)
    {
        score += pointsToAdd;
    }


    public static void Reset()
    {
        score = 0;
        
    }
}
