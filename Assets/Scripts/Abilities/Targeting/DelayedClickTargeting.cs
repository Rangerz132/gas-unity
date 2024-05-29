using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DelayClickTargeting", menuName = "Abilities/Targeting/Delay Click", order = 0)]
public class DelayedClickTargeting : TargetingStrategy
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float areaRadius;
    [SerializeField] private GameObject targetingZone;

    private GameObject targetingZoneInstance = null;

    public override void StartTargeting(AbilityData data, Action finished)
    {
        PlayerController playerController = data.User.GetComponent<PlayerController>();
        playerController.StartCoroutine(Targeting(data, playerController, finished));
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
        //playerController.enabled = false;

        if (targetingZoneInstance == null)
        {
            // Instantiate targeting zone
            targetingZoneInstance = Instantiate(targetingZone);

            // Scale targeting zone
            targetingZoneInstance.transform.localScale = new Vector3(areaRadius, areaRadius, areaRadius);
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
                targetingZoneInstance.transform.position = raycastHit.point;

                if (Input.GetMouseButtonDown(0))
                {
                    // Wait to completly finish the click frame
                    yield return new WaitWhile(() => Input.GetMouseButton(0));

                    //playerController.enabled = true;

                    data.targets = GetGameObjectsInRadius(raycastHit.point);
                    finished();

                    targetingZoneInstance.SetActive(false);

                    // Stop Coroutine
                    yield break;
                }
            }

            // Run every frame
            yield return null;
        }
    }

    /// <summary>
    /// Get list of gameObjects in a certain radius
    /// </summary>
    /// <returns></returns>
    private IEnumerable<GameObject> GetGameObjectsInRadius(Vector3 point)
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(point, areaRadius, Vector3.up, 0);

        foreach (var hit in raycastHits)
        {
            yield return hit.collider.gameObject;
        }
    }
}
