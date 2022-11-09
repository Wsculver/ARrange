using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TurnOnGravity : MonoBehaviour
{
    private ObserverBehaviour mObserverBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        // Vuforia will track this image target. Register for notifications.
        mObserverBehaviour = GetComponent<ObserverBehaviour>();

        if (mObserverBehaviour != null) {
            mObserverBehaviour.OnTargetStatusChanged += OnStatusChanged;
        }
    }

    void OnStatusChanged(ObserverBehaviour b, TargetStatus status) {
        bool isTracking = (status.Status != Status.NO_POSE);

        Rigidbody[] rigidBodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody r in rigidBodies) {
            r.useGravity = isTracking;
        }
    }
}
