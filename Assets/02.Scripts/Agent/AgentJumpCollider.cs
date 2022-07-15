using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentJumpCollider : MonoBehaviour
{
    public UnityEvent OnEnterFloor;
    public UnityEvent OnLeaveFloor;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            OnEnterFloor?.Invoke();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Floor"))
        {
            OnLeaveFloor?.Invoke();
        }
    }
}
