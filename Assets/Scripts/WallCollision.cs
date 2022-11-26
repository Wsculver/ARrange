using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private bool colliding = false;
    private Color initialColor;
    private int numCollisions = 0;

    // Start is called before the first frame update
    void Start()
    {
        Renderer rendColor = GetComponent<Renderer>();
        initialColor = rendColor.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Renderer rendColor = GetComponent<Renderer>();
        if(colliding) {
            rendColor.material.color = Color.red;
        } else {
            rendColor.material.color = initialColor;
        }
    }

    // Check for collisions with real objects
    void OnTriggerEnter(Collider collider) {        
        if(collider.gameObject.tag == "Real")
        {
            colliding = true;
            numCollisions++;
        }
    }

    void OnTriggerExit(Collider collider) {
        if(collider.gameObject.tag == "Real")
        {
            numCollisions--;
            if(numCollisions == 0) {
                colliding = false;
            }
        }
    }
}
