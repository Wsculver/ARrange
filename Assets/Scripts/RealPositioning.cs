using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealPositioning : MonoBehaviour
{
    // Winning position
    public Vector3 correctPosition;
    public GameController controller;
    public GameObject relativeObject;
    private float distanceThreshold;
    private bool placedCorrectly;

    // Start is called before the first frame update
    void Start()
    {
        distanceThreshold = 2.5f;
        placedCorrectly = false;

        controller.numObjects++;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current position is close enough to the desired
        Vector3 currentPosition = this.transform.position - relativeObject.transform.position;
        float distance = Vector3.Distance(currentPosition, correctPosition);
        if(!placedCorrectly && distance <= distanceThreshold) {
            controller.correctObjectCounter++;
            //print("correct " + this.gameObject.name + " " + distance);
            placedCorrectly = true;
        } else if(placedCorrectly && distance > distanceThreshold) {
            controller.correctObjectCounter--;
            //print("moved " + this.gameObject.name + " " + distance);
            placedCorrectly = false;
        }
    }
}
