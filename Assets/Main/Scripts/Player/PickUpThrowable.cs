using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class PickUpThrowable : MonoBehaviour
    {
        public GameObject player;
        public Transform holdPos;
        public float throwForce = 10f; //force at which the object is thrown at
        public float pickUpRange = 5f; //how far the player can pick up the object from
        private GameObject heldObj; //object which we pick up
        private Rigidbody heldObjRb; //rigidbody of object we pick up
        private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
        private int layerNumber; //layer index
        private List<GameObject> children = new List<GameObject>(); // list to store the child objects of the held object
        private new string name;
        
        private bool typeThrowable;

        //Reference to script which includes mouse movement of player (looking around)
        //we want to disable the player looking around when rotating the object
        
        void Start()
        {
            layerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) //change E to whichever key you want to press to pick up
            {
                if (heldObj is null) //if currently not holding anything
                {
                    //perform raycast to check if player is looking at object within pickup range
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickUpRange))
                    {
                        //make sure pickup tag is attached
                        if (hit.transform.gameObject.CompareTag("canPickUpThrowable")) //checks if it is a heavy object like chair, table, etc
                        {
                            //pass in object hit into the PickUpObjectHeavy function
                            PickUpObjectThrowable(hit.transform.gameObject);
                            typeThrowable = false;
                        }
                    }
                }
                else
                {
                    if(canDrop)
                    {
                        StopClipping(); //prevents object from clipping through walls
                        DropObject();
                    }
                }
            }
            if (heldObj is not null) //if player is holding object
            {
                MoveObject(); //keep object position at holdPos
                if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop) //Mouse 0 (left click) is used to throw, change this if you want another button to be used)
                {
                    StopClipping();
                    ThrowObject();
                }

            }
        }
        
        #region Pickup Functions
        
        void PickUpObjectThrowable(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
                heldObjRb.isKinematic = true;
                heldObjRb.transform.parent = holdPos.transform; //parent object to hold position
                
                //Get all child objects of the object we are holding
                foreach (Transform child in heldObj.transform)
                {
                    children.Add(child.gameObject);
                }
                foreach (GameObject child in children)
                {
                    child.layer = layerNumber;
                }
                
                heldObj.layer = layerNumber; //change the object layer to the holdLayer
                //make sure object doesn't collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

            }
        }
        
        #endregion

        #region Drop, Move, Throw, Clipping

        void DropObject()
        {
            //re-enable collision with player
            Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
            foreach (GameObject child in children)
            {
                child.layer = 0;
            }
            children.Clear();
            heldObj.layer = 0; //object assigned back to default layer
            heldObjRb.isKinematic = false;
            heldObj.transform.parent = null; //un-parent object
            
            if (name == "toiletBrush")
            {
                Weapons.ResetStats(1);
            }

            name = null;
            heldObj = null; //undefine game object
        }
        void MoveObject()
        {
            if (typeThrowable)
            {
                //keep object position the same as the holdPosition position
                heldObj.transform.position = holdPos.transform.position;
            }
        }
        void ThrowObject()
        {
            if (heldObj.CompareTag("canLiftHeavy") || heldObj.CompareTag("canPickUpThrowable"))
            {
                //same as drop function, but add force to object before undefining it
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
                foreach (GameObject child in children)
                {
                    child.layer = 0;
                }
                children.Clear();
                heldObj.layer = 0;
                heldObjRb.isKinematic = false;
                heldObj.transform.parent = null;
                heldObjRb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
                heldObj = null;
            }
        }
        void StopClipping() //function only called when dropping/throwing
        {
            var clipRange = Vector3.Distance(heldObj.transform.position, transform.position); //distance from holdPos to the camera
            //have to use RaycastAll as object blocks raycast in center screen
            //RaycastAll returns array of all colliders hit within the cliprange
            RaycastHit[] hits;
            hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
            //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
            if (hits.Length > 1)
            {
                //change object position to camera position 
                heldObj.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
                //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
            }
        }
        #endregion
    }
}
