using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    CircleCollider2D myCircleCollider;
    GameSession2 gameSession;

    [SerializeField] int pointsForCoin = 100;

    bool wasCollected = false;

    // public static int savedScore;
    // public int GetSavedScore() {return savedScore;}

    [SerializeField] AudioClip coinPickupSFX;
    void Start()
    {
        myCircleCollider = GetComponent<CircleCollider2D>();
        gameSession = FindObjectOfType<GameSession2>();
    }

    void Update()
    {
        if (myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && !wasCollected)
        {
            wasCollected = true;
            // If stil get a error picking up coin twice, set below line
            //gameObject.SetActive(false);
            gameSession.IncreasingScore(pointsForCoin);
            // if (gameObject.tag == "Coin")
            // {
            //     gameSession.IncreasingScore(100);
            // }
            // else if(gameObject.tag == "Coin200")
            // {
            //     gameSession.IncreasingScore(200);
            // }
            // savedScore += gameSession.GetScore();
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }
    }
}
