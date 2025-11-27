using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
       void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger detected with " + collision.gameObject.name);
    }
}
