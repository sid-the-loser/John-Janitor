using System.Threading;
using Sid.Scripts.Player;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class PickUpWeapon : MonoBehaviour
    {
        public GameObject player;
        public GameObject arm;
        
        public Transform holdPos;
        public Transform holdPosHeavy;

        public float throwForce = 500f; //force at which the object is thrown at
        public float pickUpRange = 5f; //how far the player can pick up the object from
        private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
        private GameObject heldObj; //object which the player has picked up
        private Rigidbody heldObjRb; //rigidbody of object which the player has picked up
        private bool canDrop = true; //used to prevent throwing/droping object when rotating
        private int layerNumber; //layer index
        
        //Reference to script that controls mouse movement of player to disable the player looking around when rotating the object
        private PlayerMovement mouseLookScript;
        void Start()
        {
            layerNumber =
                LayerMask.NameToLayer("holdLayer");

            mouseLookScript = player.GetComponent<PlayerMovement>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (heldObj == null) //if currently not holding anything
                {
                    //perform raycast to check if player is looking at object within pickup range
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                    {
                        //make sure pickup tag is attached
                        if (hit.transform.gameObject.CompareTag("canPickUpThrowable")) //Checks if it is a Light object like cup, ball, etc
                        {
                            
                        }else if (hit.transform.gameObject.CompareTag("canPickUpWeapon")) //checks if it is a weapon
                        {
                            
                        }else if (hit.transform.gameObject.CompareTag("canLiftHeavy")) //checks if it is a heavy object like chair, table, etc
                        {
                            //pass in object hit into the PickUpObject function
                            PickUpObjectHeavy(hit.transform.gameObject);
                        }
                    }
                }
                else
                {
                    if (canDrop == true)
                    {
                        StopClipping(); //prevents object from clipping through walls
                        DropObject();
                    }
                }
            }

            if (heldObj != null) //if player is already holding object
            {
                MoveObject(); //constantly moves object position at holdPos
                RotateObject();
                if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true) //left click is used to throw
                {
                    StopClipping();
                    ThrowObject();
                }
            }
        }

        void PickUpObjectHeavy(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObj.transform.rotation = new Quaternion(0, 0, 0, 0);
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //Gets and stores Rigidbody
                heldObjRb.isKinematic = true; //sets rigidbody to kinematic
                heldObjRb.transform.parent = holdPosHeavy.transform; //parent object to holdposition
                heldObj.layer = layerNumber; //change the object layer to the holdLayer
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
                arm.SetActive(false);
            }
        }

        void DropObject()
        {
            //re-enable collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0; //object assigned back to default layer
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null; //un parent object
            heldObj = null; //undefine game object
            arm.SetActive(true);
        }

        void MoveObject()
        {
            //keep object position the same as the holdPosition position
            heldObj.transform.position = holdPosHeavy.transform.position;
        }

        void RotateObject()
        {
            if (Input.GetKey(KeyCode.R)) //hold R key to rotate, change this to whatever key you want
            {
                canDrop = false; //make sure throwing can't occur during rotating
                
                //disable player being able to look around
                mouseLookScript.enabled = false;
                
                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
                float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
                //rotate the object depending on mouse X-Y Axis
                heldObj.transform.Rotate(Vector3.down, XaxisRotation);
                heldObj.transform.Rotate(Vector3.right, YaxisRotation);
            }
            else
            {
                //re-enable player being able to look around
                mouseLookScript.enabled = true;
                canDrop = true;
            }
        }

        void ThrowObject()
        {
            //same as drop function, but add force to object before undefining it
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            heldObj.layer = 0;
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null;
            heldObjRb.velocity = transform.forward * (throwForce * Time.deltaTime);
                //.AddForce(transform.forward * throwForce);
            heldObj = null;
            arm.SetActive(true);
        }

        void StopClipping() //function only called when dropping/throwing
        {
            var clipRange =
                Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
            //have to use RaycastAll as object blocks raycast in center screen
            //RaycastAll returns array of all colliders hit within the cliprange
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
            //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
            if (hits.Length > 1)
            {
                //change object position to camera position 
                heldObj.transform.position =
                    transform.position +
                    new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
                //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
            }
        }
    }
}