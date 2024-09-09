using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] int damageAmount;

    HealthHandler charHealth;
    bool inContact = false;

    private void Update()
    {
        if (charHealth != null)
        {
            charHealth.TakeDamage(damageAmount);
        }
    }

    //collider code
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            inContact = true;
            StartCoroutine(Damage(collision.gameObject.GetComponent<HealthHandler>()));
            collision.rigidbody.velocity -= collision.relativeVelocity * .5f;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        inContact = false;
    }

    //trigger code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            inContact = true;
            StartCoroutine(Damage(other.gameObject.GetComponent<HealthHandler>()));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inContact = false;
    }

    IEnumerator Damage(HealthHandler charHealth)
    {
        while (inContact)
        {
            Debug.Log("Damaged");
            charHealth.TakeDamage(damageAmount);
            yield return new WaitForSeconds(1f);
        }
    }
}
