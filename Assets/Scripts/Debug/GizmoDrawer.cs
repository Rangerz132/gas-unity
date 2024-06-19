using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public delegate void GizmoDrawAction(Vector3 position);
    private GizmoDrawAction gizmoDrawAction;
    private Vector3 gizmoPosition;

    public void SetGizmoDrawAction(GizmoDrawAction action)
    {
        gizmoDrawAction = action;
    }

    public void SetGizmoPosition(Vector3 position)
    {
        gizmoPosition = position;
    }

    private void OnDrawGizmos()
    {
        if (gizmoDrawAction != null)
        {
            gizmoDrawAction(gizmoPosition);
        }
    }
}
