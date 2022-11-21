using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioning : MonoBehaviour
{
    // Winning position
    public Vector3 correctPosition;
    private float distanceThreshold;
    private bool placedCorrectly;

    // Start is called before the first frame update
    void Start()
    {
        distanceThreshold = 1;
        placedCorrectly = false;
        // TODO: Show correct position for set amount of time. Maybe here maybe in a controller script

        // TODO: Randomize starting position next to board
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current position is close enough to the desired
        Vector3 currentPosition = this.transform.localPosition;
        float distance = Vector3.Distance(currentPosition, correctPosition);
        if(!placedCorrectly && distance <= distanceThreshold) {
            // TODO: Increment correct object counter in controller script
            //print("correct " + distance);
            placedCorrectly = true;
        } else if(placedCorrectly && distance > distanceThreshold) {
            // TODO: Decrement correct object counter in controller script
            //print("moved " + distance);
            placedCorrectly = false;
        }
    }
}
