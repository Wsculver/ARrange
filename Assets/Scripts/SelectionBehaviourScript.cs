using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class SelectionBehaviourScript : MonoBehaviour
{
    private GameObject selectedObject;
    private Rigidbody selectedRigidbody;
    private Color selectedObjectOriginalMaterialColor;
    private Transform selectedObjectOriginalParentTransform;
    private bool isAnObjectSelected;

    private float previousXKnobRotation;
    private float previousYKnobRotation;
    private float previousZKnobRotation;
    public ImageTargetBehaviour xKnob;
    public ImageTargetBehaviour yKnob;
    public ImageTargetBehaviour zKnob;

    // Start is called before the first frame update
    void Start()
    {
        isAnObjectSelected = false;
        previousXKnobRotation = xKnob.transform.eulerAngles.y;
        previousYKnobRotation = yKnob.transform.eulerAngles.y;
        previousZKnobRotation = zKnob.transform.eulerAngles.y;
        InvokeRepeating("CheckKnobMovement", 0.5f, 0.5f); // every half second
    }

    // Update is called once per frame
    void Update()
    {
        GameObject mainCamera = Camera.main.gameObject;
        Vector3 origin = mainCamera.transform.position;
        Vector3 direction = mainCamera.transform.forward;
        Ray ray = new Ray(origin, direction);
        RaycastHit hit;
        bool isThereAHit = Physics.Raycast(ray, out hit);


        if (isAnObjectSelected) {
            selectedRigidbody.useGravity = false;
            if (getUserTap()) {
                selectedObject.GetComponent<Renderer>().material.color = selectedObjectOriginalMaterialColor;
                isAnObjectSelected = false;
                selectedRigidbody.useGravity = true;
                selectedObject.transform.parent = selectedObjectOriginalParentTransform;
            } else { // Still selected.
                selectedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                selectedObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                selectedObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0,0,0);

                
            }
        } else {
            if (isThereAHit && hit.collider.gameObject.tag != "NonSelectable" && hit.collider.gameObject.tag != "Real") {
                //print("Got a hit: " + hit.collider.gameObject.name);
                if (getUserTap()) {
                    //print("Got a tap");
                    selectedObject = hit.collider.gameObject;

                    selectedRigidbody = hit.rigidbody;
                    selectedObjectOriginalMaterialColor = selectedObject.GetComponent<Renderer>().material.color;
                    Color selectedColor = selectedObjectOriginalMaterialColor;
                    selectedColor.a = selectedObjectOriginalMaterialColor.a - 0.4f;
                    selectedObject.GetComponent<Renderer>().material.color = selectedColor;
                    isAnObjectSelected = true;
                    selectedRigidbody.useGravity = false;
                    selectedObjectOriginalParentTransform = selectedObject.transform.parent;
                    selectedObject.transform.parent = mainCamera.transform;
                }
            }
        }
    }

    private void CheckKnobMovement() {
        float currentXKnobRotation = xKnob.transform.eulerAngles.y;
        float currentYKnobRotation = yKnob.transform.eulerAngles.y;
        float currentZKnobRotation = zKnob.transform.eulerAngles.y;

        if (isAnObjectSelected) {
            selectedObject.transform.Rotate(amountToRotate(previousXKnobRotation, currentXKnobRotation), 0.0f, 0.0f, Space.Self);
            selectedObject.transform.Rotate(0.0f, amountToRotate(previousYKnobRotation, currentYKnobRotation), 0.0f, Space.Self);
            selectedObject.transform.Rotate(0.0f, 0.0f, amountToRotate(previousZKnobRotation, currentZKnobRotation), Space.Self);
        }

        print("x: " + currentXKnobRotation + " " + previousXKnobRotation);
        print("y: " + currentYKnobRotation + " " + previousYKnobRotation);
        print("z: " + currentZKnobRotation + " " + previousZKnobRotation);

        previousXKnobRotation = currentXKnobRotation;
        previousYKnobRotation = currentYKnobRotation;
        previousZKnobRotation = currentZKnobRotation;
    }

    private float amountToRotate(float prev, float current) {
        // if rotation number has changed by 5 in the last half second, rotate by 10
        if (current-prev > 5.0f /*|| (current+360)-prev > 5.0f*/) {
            return 10.0f;
        } else if (prev-current > 5.0f /*|| (prev+360)-current > 5.0f*/) {
            return -10.0f;
        } else {
            return 0.0f;
        }
    }

    private bool getUserTap() {
        bool isTap = false;
        // Check for a touch (if we have smart phone).
        if (Input.touchCount > 0) {
            // We have a tap on the screen.
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began) {
                Vector2 p = touch.position;
                // Check if touch position is not too close to the edge of the screen.
                float fractScreenBorder = 0.1f;
                if (p.x > fractScreenBorder * Screen.width && p.x < (1 - fractScreenBorder) * Screen.width &&
                    p.y > fractScreenBorder * Screen.height && p.y < (1 - fractScreenBorder) * Screen.height)
                {
                    isTap = true;
                }
            }
        }
        else {
            // Check for keypress.
            isTap = Input.anyKeyDown && Input.GetKey(KeyCode.Space);
        }
        return isTap;
    }
}
