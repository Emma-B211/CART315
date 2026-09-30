using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class Court : MonoBehaviour
{
    public GameManager gameManager;
    public int courtId = 0;


    public EventTrigger.TriggerEvent courtTrigger;

// where the ball will teleport
    public Transform TeleportPoint;

    // ball prefav used for the multiplier
    public GameObject ballPrefab;

    public Transform ballSpawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;
        
    

    // teleport the ball
        ball.transform.position = TeleportPoint.position;

        Instantiate (
            ballPrefab,
            ballSpawnPoint.position,
            Quaternion.identity
        );

        BaseEventData eventData = new BaseEventData(EventSystem.current);
        courtTrigger.Invoke(eventData);
    }
}