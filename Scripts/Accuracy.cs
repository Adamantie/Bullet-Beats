using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accuracy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == null)
        {
            other.transform.SendMessage("Perfect", null, SendMessageOptions.DontRequireReceiver);
        }
        else if (other.transform.parent != null && other.gameObject.name != "noteEmpty")
        {
            other.transform.parent.SendMessage("Perfect", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void OnTriggerExit(Collider other)
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
