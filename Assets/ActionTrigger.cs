using System;
using System.Collections;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public float ActionCoolDown = 0.5f;
    private bool _isCooling = false;
    private void OnTriggerEnter(Collider other)
    {
        if (SubscribedAction!= null && !_isCooling)
        {
            SubscribedAction.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isCooling = true;
        StartCoroutine(CoolDown());
    }

    public Action SubscribedAction { get; set; }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(ActionCoolDown);
        _isCooling = false;
    }
}