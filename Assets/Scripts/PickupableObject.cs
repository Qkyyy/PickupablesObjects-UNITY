using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupableObject : MonoBehaviour {

    Transform playerTransform;          //player's Transform component  
    Rigidbody rb;                       //object's rigidbody
        
    float throwPower = 10f;             //amount of throw power
    bool pickedUp = false;              //bools which tells us if the objects was picked up

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleObjectThrow();
        HandleObjectPut();
    }


    //Picks up the object from ground
    private void OnMouseDown()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < 2.2f)      //If player is close enough
        {
            transform.SetParent(Camera.main.transform);                                       //sets the parent of the object to be player
            rb.isKinematic = true;                                                      //turns Kinematic on, so object will not interfere when picked up
            transform.localPosition = new Vector3(0f, 0f, 1.4f);                      //sets local position in front of player, so it seems player is holding it
            transform.localRotation = Quaternion.Euler(0, 0f, 0);                       //rotates the object
            pickedUp = true;                                                            //tell's that object was picked up
        }
    }

    //Handles object throw
    void HandleObjectThrow()
    {
            if (Input.GetMouseButtonDown(1) && pickedUp == true)                        //If player clicked RMB and object was picked up
            {

                transform.SetParent(null);                                              //unleashes object from its parent
                rb.isKinematic = false;                                                 //turns off kinematic, so object can be physical again
                rb.AddForce(new Vector3(Camera.main.transform.forward.x,0.2f, Camera.main.transform.forward.z) * throwPower, ForceMode.Impulse);    //Adds force in direction where camera is looking with 'throwpower' power and Impulse forcemode
                pickedUp = false;                                                       //object is not picked up

            }
    }


    //Handles putting object lightly on the ground
    void HandleObjectPut()
    {
        if (Input.GetKeyDown(KeyCode.E) && pickedUp == true)                            //if player clicked 'E' and picked up object before
        {
            transform.SetParent(null);                                                  //unleashes object from its parent
            rb.isKinematic = false;                                                     //turns kinematic off, so object can lightly fall on the ground
            pickedUp = false;                                                           //object is not picked up
        }
    }
}
