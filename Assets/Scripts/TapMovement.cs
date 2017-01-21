﻿using Academy.HoloToolkit.Unity;
using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class TapMovement : MonoBehaviour
{
    [Tooltip("GameObject player.")]
    public GameObject player;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // Do a raycast into the world that will only hit the plane's mesh.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {

            player.transform.position = Vector3.MoveTowards(player.transform.position, hitInfo.point, 5);

        }
    }

    void Update()
    {
        
    }
}