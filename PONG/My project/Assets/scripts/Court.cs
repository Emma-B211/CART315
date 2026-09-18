using.system;
using UnityEngine;
using UnityEngine.EventSystems;

public class Court : MonoBehaviour
{
    public EventTrigger.TriggerEvent courtTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnCollosionEnter2D(Collision2D other)
    {
       BaseEventData eventData =  new BaseEventData(EventSystems.current);
       courtTrigger.Invoke(eventData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
