using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingHandler : MonoBehaviour
{
    [SerializeField] int healAmount;

    bool inContact;



    //collider code
    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.layer == 3)
        {
            inContact = true;
            StartCoroutine(Heal(collider.gameObject.GetComponent<HealthHandler>()));
        }
    }
    private void OnCollisionExit(Collision collider)
    {
        inContact = false;
    }

    //trigger code
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            inContact = true;
            StartCoroutine(Heal(other.gameObject.GetComponent<HealthHandler>()));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        inContact = false;
    }

    IEnumerator Heal(HealthHandler charHealth)
    {
        while (inContact)
        {
            Debug.Log("Healed");
            charHealth.Heal(healAmount);
            yield return new WaitForSeconds(1f);
        }
    }
}
