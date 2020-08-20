using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public string ButtonTrigger = "";
    bool buttonpressed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetButtonDown(ButtonTrigger))
        //     buttonpressed = true;
        // else 
        //     buttonpressed = false;
    }

private void OnTriggerStay(Collider other) {
    //Debug.Log("Ontriggerstay");
    // if(Input.GetButtonDown(ButtonTrigger))
    //if(buttonpressed){
        Debug.Log($"I HIT {ButtonTrigger}");
        Destroy(gameObject);
    //}
}

private void OnTriggerExit(Collider other) {
    Debug.Log(other.name);
    Destroy(gameObject);
}

}
