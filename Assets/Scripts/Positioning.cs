using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positioning : MonoBehaviour
{
    // Winning position
    public Vector3 correctPosition;
    public Quaternion correctRotation;
    public Vector3 startingPosition;
    public Quaternion startingRotation;
    public GameController controller;
    public bool realClone;
    private float distanceThreshold;
    private bool placedCorrectly;
    private bool scattered = false;

    // Start is called before the first frame update
    void Start()
    {
        distanceThreshold = 2.0f;
        placedCorrectly = false;

        if(!realClone) {
            controller.numObjects++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scattered) {
            // Check if the current position is close enough to the desired
            Vector3 currentPosition = transform.localPosition;
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

    public void Scatter() {
        if(realClone) {
            gameObject.SetActive(false);
        } else {
            transform.localPosition = startingPosition;
            transform.localRotation = startingRotation;
            scattered = true;
        }
    }

    public void ShowSolution() {
        transform.localPosition = correctPosition;
        transform.localRotation = correctRotation;
    }
}
