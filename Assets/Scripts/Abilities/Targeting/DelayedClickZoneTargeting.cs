using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DelayClickZoneTargeting", menuName = "Abilities/Targeting/Delay Click Zone", order = 0)]
public class DelayedClickZoneTargeting : TargetingStrategy
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Vector3 offset;
    [SerializeField] private GameObject targetingZone;
    [SerializeField] private ZoneStrategy zoneStrategy;
    private GameObject targetingZoneInstance = null;
    private Vector3 targetPosition;

    public override void StartTargeting(AbilityData data, Action finished)
    {
        PlayerController playerController = data.User.GetComponent<PlayerController>();
        playerController.StartCoroutine(Targeting(data, playerController, finished));

        playerController.GizmoDrawer.SetGizmoDrawAction((Vector3 position) => zoneStrategy.DrawGizmos(targetPosition));
    }

    /// <summary>
    /// Aim until the user has click on something
    /// </summary>
    /// <param name="user"></param>
    /// <param name="playerController"></param>
    /// <param name="finished"></param>
    /// <returns></returns>
    private IEnumerator Targeting(AbilityData data, PlayerController playerController, Action finished)
    {

        if (targetingZoneInstance == null)
        {
            // Instantiate targeting zone
            targetingZoneInstance = Instantiate(targetingZone);

            // Scale targeting zone
            targetingZoneInstance.transform.localScale = new Vector3(zoneStrategy.Size, zoneStrategy.Size, zoneStrategy.Size);
        }
        else
        {
            targetingZoneInstance.SetActive(true);
        }


        while (true)
        {
            // Change cursor while targeting (aiming)
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

            // Raycast data
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            float maxDistance = 1000;

            if (Physics.Raycast(ray, out raycastHit, maxDistance, layerMask))
            {
                // Update targeting zone position
                targetingZoneInstance.transform.position = raycastHit.point + offset;

                targetPosition = targetingZoneInstance.transform.position;
                playerController.GizmoDrawer.SetGizmoPosition(targetPosition);

                if (Input.GetMouseButtonDown(0))
                {
                    // Wait to completly finish the click frame
                    yield return new WaitWhile(() => Input.GetMouseButton(0));

                    //playerController.enabled = true;
                    targetingZoneInstance.SetActive(false);
                    data.targetedPoints = raycastHit.point;
                    data.targets = zoneStrategy.GetGameObjectsInZone(raycastHit.point);
                    finished();

                    // Stop Coroutine
                    yield break;
                }
            }

            // Run every frame
            yield return null;
        }
    }
}
