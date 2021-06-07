using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGreat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            other.transform.SendMessage("Great", null, SendMessageOptions.DontRequireReceiver);
        }
        else if (other.transform.parent != null && other.gameObject.name != "noteEmpty")
        {
            other.transform.parent.SendMessage("Great", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
