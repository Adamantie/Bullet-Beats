using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            other.transform.SendMessage("Miss", null, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            other.transform.parent.SendMessage("Miss", null, SendMessageOptions.DontRequireReceiver);
        }
    }
}
