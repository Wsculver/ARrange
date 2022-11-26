using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using TMPro;

public class GameController : MonoBehaviour
{
    [HideInInspector] 
    public int correctObjectCounter = 0;
    [HideInInspector]
    public int numObjects = 0;

    public GameObject winScreen;
    public GameObject crosshairScreen;

    private bool isTracking = false;
    private ObserverBehaviour mObserverBehaviour;
    private bool gameWon = false;

    // Start is called before the first frame update
    void Start()
    {
        winScreen.SetActive(false);
        crosshairScreen.SetActive(true);

        // Vuforia will track this image target. Register for notifications.
        mObserverBehaviour = GetComponent<ObserverBehaviour>();

        if (mObserverBehaviour != null) {
            mObserverBehaviour.OnTargetStatusChanged += OnStatusChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Remove
        numObjects = 5;
        if(!gameWon) {
            TextMeshProUGUI counterText = GameObject.Find("Correct Count Text").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI outOfText = GameObject.Find("Out Of Text").GetComponent<TextMeshProUGUI>();
            if(isTracking) {
                counterText.text = correctObjectCounter.ToString(); 
            } else {
                counterText.text = "0";
            }
            outOfText.text = "/ " + numObjects.ToString();
        }

        if(correctObjectCounter != 0 && correctObjectCounter == numObjects && isTracking) {
            // Win
            crosshairScreen.SetActive(false);
            winScreen.SetActive(true);
        }
    }

    void OnStatusChanged(ObserverBehaviour b, TargetStatus status) {
        isTracking = (status.Status != Status.NO_POSE);
    }
}
