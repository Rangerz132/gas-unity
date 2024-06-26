using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DelayClickDirectionalTargeting", menuName = "Abilities/Targeting/Delay Click Directional", order = 0)]
public class DirectionalTargeting : TargetingStrategy
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 cursorHotspot;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float offset;

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
                if (Input.GetMouseButtonDown(0))
                {
                    // Wait to completly finish the click frame
                    yield return new WaitWhile(() => Input.GetMouseButton(0));

                    data.targetedPoints = raycastHit.point + ray.direction * offset / ray.direction.y;
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

