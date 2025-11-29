using UnityEngine;

public class Delivery : MonoBehaviour
{
    bool hasPackage;
    [SerializeField] float destroyDelay = 0.2f;
    void OnTriggerEnter2D(Collider2D collision)       
    {
        // if the tag is packge, print picked up package to console
        if (collision.CompareTag("Package") && !hasPackage)
        {
            Debug.Log("Picked up package");
            hasPackage = true;
            GetComponent<ParticleSystem> ().Play();
            //STop the particle system for "customer"
            //Find component by tag name customer
            GameObject customer = GameObject.FindWithTag("customer");
            if (customer != null)
            {
                customer.GetComponent<ParticleSystem>().Stop();
            }
            Destroy(collision.gameObject, destroyDelay * Time.timeScale);
        }

        // if the tag is packge, print picked up package to console
        if (collision.CompareTag("customer") && hasPackage)
        {
            Debug.Log("Delivered package");
            hasPackage = false;
             GetComponent<ParticleSystem> ().Stop();
             // Play particle system for "customer"
             collision.GetComponent<ParticleSystem> ().Play();
        }
    }
}
