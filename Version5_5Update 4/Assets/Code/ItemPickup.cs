using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour
{


    public int pointsToAdd = 200;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == null)
            return;

        ScoreManagerPapier.AddPoints(pointsToAdd);

        Destroy(gameObject);
    }



}
