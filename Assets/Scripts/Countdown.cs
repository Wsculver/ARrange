using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text countdownText;
    public GameObject countdownScreen;

    private bool counting = false;
    private float time = 15f;

    // Start is called before the first frame update
    void Start()
    {
        countdownScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(counting) {
            if(time > 0)     
            {         
                time -= Time.deltaTime;     
            }     
            float seconds = Mathf.FloorToInt(time % 60);
            countdownText.text = seconds.ToString();
            if(time < 0) {
                countdownScreen.SetActive(false);
                counting = false;
                time = 15f;
            }
        }
    }

    public void BeginCountdown() {
        countdownScreen.SetActive(true);
        counting = true;
    }
}
