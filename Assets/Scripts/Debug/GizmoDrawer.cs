using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    public delegate void GizmoDrawAction();
    private GizmoDrawAction gizmoDrawAction;

    public void SetGizmoDrawAction(GizmoDrawAction action)
    {
        gizmoDrawAction = action;
    }

    private void OnDrawGizmos()
    {
        if (gizmoDrawAction != null)
        {
            gizmoDrawAction();
        }
    }
}
