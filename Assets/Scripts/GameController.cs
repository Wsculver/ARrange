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
    [HideInInspector]
    public bool scattered = false;

    public GameObject startScreen;
    public GameObject winScreen;
    public GameObject crosshairScreen;
    public TMP_Text gameTimeText;
    public TMP_Text winTimeText;

    private bool isTracking = false;
    private bool firstTrack = false;
    private ObserverBehaviour mObserverBehaviour;
    private bool gameWon = false;
    private float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        startScreen.SetActive(false);
        winScreen.SetActive(false);
        crosshairScreen.SetActive(false);

        // Vuforia will track this image target. Register for notifications.
        mObserverBehaviour = GetComponent<ObserverBehaviour>();

        if (mObserverBehaviour != null) {
            mObserverBehaviour.OnTargetStatusChanged += OnStatusChanged;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(scattered) {
            if(!gameWon) {
                TextMeshProUGUI counterText = GameObject.Find("Correct Count Text").GetComponent<TextMeshProUGUI>();
                TextMeshProUGUI outOfText = GameObject.Find("Out Of Text").GetComponent<TextMeshProUGUI>();
                if(isTracking) {
                    counterText.text = correctObjectCounter.ToString(); 
                } else {
                    counterText.text = "0";
                }
                outOfText.text = "/ " + numObjects.ToString();
      
                time += Time.deltaTime;  
                float minutes= Mathf.FloorToInt(time / 60); 
                float seconds = Mathf.FloorToInt(time % 60);
                gameTimeText.text = string.Format("{0,00}:{1:00}",minutes,seconds);

                if(correctObjectCounter != 0 && correctObjectCounter == numObjects && isTracking) {
                    // Win
                    crosshairScreen.SetActive(false);
                    winScreen.SetActive(true);
                    winTimeText.text = string.Format("{0,00}:{1:00}",minutes,seconds);
                    gameWon = true;
                }
            }           
        }
    }

    void OnStatusChanged(ObserverBehaviour b, TargetStatus status) {
        isTracking = (status.Status != Status.NO_POSE);
        if(!firstTrack && isTracking) {
            startScreen.SetActive(true);
            firstTrack = true;
        }
    }
}
