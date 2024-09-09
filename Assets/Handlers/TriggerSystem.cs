using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSystem : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("enter");
            OnEnter?.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("exit");
            OnExit?.Invoke();
        }
    }
}
