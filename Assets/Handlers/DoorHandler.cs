using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    Animator anim;
    bool open = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        anim.SetTrigger("Open");
        open = true;
    }

    public Animator GetAnim()
    {
        return anim;
    }

    public void CloseDoor()
    {
        anim.SetTrigger("Close");
        open = false;
    }
    public void ToggleDoor()
    {
        open = !open;

        if (open)
            anim.SetTrigger("Open");
        else
            anim.SetTrigger("Close");
    }
}
