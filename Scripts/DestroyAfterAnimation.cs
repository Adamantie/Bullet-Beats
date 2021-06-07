using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimation : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (animator.gameObject.transform.parent != null)
        {
            //Destroy(animator.gameObject.transform.parent.gameObject, stateInfo.length);
            //animator.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            //Destroy(animator.gameObject, stateInfo.length);
            //animator.gameObject.SetActive(false);
        }
    }
}
