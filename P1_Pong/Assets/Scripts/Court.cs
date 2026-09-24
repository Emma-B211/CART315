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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball == null) return;

    // teleport the ball
        ball.transform.position = TeleportPoint.position;

        BaseEventData eventData = new BaseEventData(EventSystem.current);
        courtTrigger.Invoke(eventData);
    }
}