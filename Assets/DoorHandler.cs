using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] Animator door;

    private void OpenDoor()
    {
        door.SetTrigger("Open");
    }
    private void CloseDoor()
    {
        door.SetTrigger("Close");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("open");
            OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            Debug.Log("close");
            CloseDoor();
        }
    }
}
