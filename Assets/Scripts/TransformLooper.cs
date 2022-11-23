using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Purpose:
 * Looping the object transform across a rectangle area.
 */

[AddComponentMenu("Scripts/ESI/Transform Looper")]
public class TransformLooper : MonoBehaviour {

    //[field: SerializeField] public Rect area { get; private set; } // The area to loop the object transform.
    [field: SerializeField] public GameArea gameArea { get; private set; }

    [SerializeField] private Vector2 areaSpacePosition = Vector2.zero; // The area to loop the object transform.


    // Update is called once per frame
    void Update() {
        //position = transform.position;  // Get the current position.

        // InverseTransformPoint: Transform a point from world space into local space.
        areaSpacePosition = gameArea.transform.InverseTransformPoint(transform.position); // Get the current position.

        if (gameArea._area.Contains(areaSpacePosition)) // If the current position (player or asset) is inside the area.
            return;  // Do nothing.

        if (areaSpacePosition.x < gameArea._area.xMin)       // If the current position is less than the minimum x value of the area.
            areaSpacePosition.x = gameArea._area.xMax;       // Set the current position to the maximum x value of the area.
        else if (areaSpacePosition.x > gameArea._area.xMax)  // If the current position is greater than the maximum x value of the area.
            areaSpacePosition.x = gameArea._area.xMin;       // Set the current position to the minimum x value of the area.

        if (areaSpacePosition.y < gameArea._area.yMin)       // If the current position is less than the minimum y value of the area.
            areaSpacePosition.y = gameArea._area.yMax;       // Set the current position to the maximum y value of the area.
        else if (areaSpacePosition.y > gameArea._area.yMax)  // If the current position is greater than the maximum y value of the area.
            areaSpacePosition.y = gameArea._area.yMin;       // Set the current position to the minimum y value of the area.

        //transform.position = position;    // Set the current position.
        transform.position = gameArea.transform.TransformPoint(areaSpacePosition);    // Set the current position.

    }
}
