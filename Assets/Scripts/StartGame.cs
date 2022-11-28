using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Transform gameObjects;
    public GameController controller;
    public GameObject countdown;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in gameObjects)
        {
            child.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButton() {
        controller.startScreen.SetActive(false);
        foreach (Transform child in gameObjects)
        {
            child.gameObject.SetActive(true);
            child.gameObject.GetComponent<Positioning>().ShowSolution();
            child.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
        countdown.gameObject.GetComponent<Countdown>().BeginCountdown();
        Invoke("ScatterObjects", 15f);
    }

    void ScatterObjects() {
        controller.crosshairScreen.SetActive(true);
        foreach (Transform child in gameObjects)
        {
            child.gameObject.GetComponent<Positioning>().Scatter();
        }
        controller.scattered = true;
    }
}
