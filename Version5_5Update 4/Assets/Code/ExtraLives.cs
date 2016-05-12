using UnityEngine;
using System.Collections;

public class ExtraLives : PlayerLifePointManager
{

    public int extraLives=1;
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Zunge")
        {
          // Debug.Log("frisstFliege");
            //if (other.GetComponent<Player>() == null)
            //{
               
            //    return;
            //}
               

            ScoreManager.ExtraLife(extraLives);

            Destroy(gameObject);
        }
            
        
    }
}