using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    MovementLogic logicmovement;
    private void OnTriggerEnter(Collider other)
    {
        logicmovement.groundedchanger();
    }
    // Start is called before the first frame update
    void Start()
    {
        logicmovement = this.GetComponentInParent<MovementLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
