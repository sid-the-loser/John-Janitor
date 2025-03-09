using System.Collections.Generic;
using Sid.Scripts.Player;
using Unity.Mathematics;
using UnityEngine;

namespace Main.Scripts.Player
{
    public class NewPickUp : MonoBehaviour
    {
        public GameObject player;
        public Transform holdPos;
        public GameObject holdPosObj;
        private static GameObject heldWeapon;
        public Transform holdPosHeavy;
        public float throwForce = 10f; //force at which the object is thrown at
        public float pickUpRange = 5f; //how far the player can pickup the object from
        private float rotationSensitivity = 1f; //how fast/slow the object is rotated in relation to mouse movement
        public static GameObject heldObj; //object which we pick up
        private Rigidbody heldObjRb; //rigidbody of object we pick up
        private bool canDrop = true; //this is needed so we don't throw/drop object when rotating the object
        private int layerNumber; //layer index
        private List<GameObject> children = new List<GameObject>(); // list to store the child objects of the held object
        private string name;
        
        private bool TypeHeavy = false;
        private bool TypeWeapon = false;

        public static bool isWeaponHeld;

        //Reference to script which includes mouse movement of player (looking around)
        //we want to disable the player looking around when rotating the object
        private PlayerMovement mouseLookScript;
        
        void Start()
        {
            layerNumber = LayerMask.NameToLayer("holdLayer"); //if your holdLayer is named differently make sure to change this ""

            mouseLookScript = player.GetComponent<PlayerMovement>();
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
                        if (hit.transform.gameObject.CompareTag("canLiftHeavy")) //checks if it is a heavy object like chair, table, etc
                        {
                            //pass in object hit into the PickUpObjectHeavy function
                            PickUpObjectHeavy(hit.transform.gameObject);
                            TypeHeavy = true; TypeWeapon = false;
                            isWeaponHeld = true;
                            PickUpThrowable.isThrowableHeld = false;
                            
                        }else if (hit.transform.gameObject.CompareTag("canPickUpWeapon")) //Checks if it is a Light object like cup, ball, etc
                        {
                            PickUpObjectWeapon(hit.transform.gameObject);
                            TypeHeavy = false; TypeWeapon = true;
                            isWeaponHeld = true;
                            PickUpThrowable.isThrowableHeld = false;
                        }
                    }
                }
                else
                {
                    if(canDrop == true)
                    {
                        StopClipping(); //prevents object from clipping through walls
                    }
                }
            }
            if (heldObj is not null) //if player is holding object
            {
                MoveObject(); //keep object position at holdPos
                RotateObject();
                if (Input.GetKeyDown(KeyCode.Mouse0) && canDrop == true && isWeaponHeld) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
                {
                    StopClipping();
                    ThrowObject();
                }
                if (Input.GetKeyUp(KeyCode.Q) && isWeaponHeld)
                {
                    DropObject();
                }
            }
        }
        
        #region Pickup Functions
        
        void PickUpObjectHeavy(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
                heldObjRb.isKinematic = true;
                heldObjRb.transform.parent = holdPosHeavy.transform; //parent object to holdposition
                heldObj.transform.localEulerAngles = new Vector3(90, 0, 0);
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
                //make sure object doesnt collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);
            }
        }
        void PickUpObjectWeapon(GameObject pickUpObj)
        {
            if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
            {
                heldObj = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
                heldObjRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
                heldObjRb.isKinematic = true;
                heldObjRb.transform.parent = holdPos.transform; //parent object to holdposition
                
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
                //make sure object doesnt collide with player, it can cause weird bugs
                Physics.IgnoreCollision(heldObj.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

                name = heldObj.name;
                UpdatingStats(name);
            }
        }
        
        #endregion

        #region Drop, Move, Rotate, Throw

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
            heldObj.transform.parent = null; //unparent object
            
            ResetingStats(name);

            name = null;
            heldObj = null; //undefine game object
            isWeaponHeld = false;
        }
        void MoveObject()
        {
            if (TypeHeavy)
            {
                //keep object position the same as the holdPosition position
                heldObj.transform.position = holdPosHeavy.transform.position;
            }else if (TypeWeapon)
            {
                heldObj.transform.position = holdPos.transform.position;
            }
        }
        void RotateObject()
        {
            if (Input.GetKey(KeyCode.R))//hold R key to rotate, change this to whatever key you want
            {
                canDrop = false; //make sure throwing can't occur during rotating
                
                mouseLookScript.enabled = false;
                
                //disable player being able to look around
                //mouseLookScript.verticalSensitivity = 0f;
                //mouseLookScript.lateralSensitivity = 0f;

                float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
                float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
                //rotate the object depending on mouse X-Y Axis
                heldObj.transform.Rotate(Vector3.down, XaxisRotation);
                heldObj.transform.Rotate(Vector3.right, YaxisRotation);
            }
            else
            {
                //re-enable player being able to look around
                //mouseLookScript.verticalSensitivity = originalvalue;
                //mouseLookScript.lateralSensitivity = originalvalue;
                mouseLookScript.enabled = true;
                canDrop = true;
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
        
        #endregion
        
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

        private static void UpdatingStats(string name)
        {
            if (name == "toiletBrush")
            {
                Weapons.UpgradeStats(1);
            }
            else if (name == "plunger")
            {
                Weapons.UpgradeStats(2);
            }
            else if (name == "featherDuster")
            {
                Weapons.UpgradeStats(3);
            }
            else if (name == "broom")
            {
                Weapons.UpgradeStats(4);
            }
            else if (name == "mop")
            {
                Weapons.UpgradeStats(5);
            }
            else if (name == "werFloorSign")
            {
                Weapons.UpgradeStats(6);
            }
            else if (name == "fireExtinguisher")
            {
                Weapons.UpgradeStats(7);
            }
            else if (name == "cleaningSprayBottles")
            {
                Weapons.UpgradeStats(8);
            }
            else if (name == "cleaningGloves")
            {
                Weapons.UpgradeStats(9);
            }
            else if (name == "stapler")
            {
                Weapons.UpgradeStats(10);
            }
            else if (name == "officePhone")
            {
                Weapons.UpgradeStats(11);
            }
            else if (name == "ladder")
            {
                Weapons.UpgradeStats(12);
            }
        }

        private void ResetingStats(string name)
        {
            if (name == "toiletBrush")
            {
                Weapons.ResetStats(1);
            }
            else if (name == "plunger")
            {
                Weapons.ResetStats(2);
            }
            else if (name == "featherDuster")
            {
                Weapons.ResetStats(3);
            }
            else if (name == "broom")
            {
                Weapons.ResetStats(4);
            }
            else if (name == "mop")
            {
                Weapons.ResetStats(5);
            }
            else if (name == "werFloorSign")
            {
                Weapons.ResetStats(6);
            }
            else if (name == "fireExtinguisher")
            {
                Weapons.ResetStats(7);
            }
            else if (name == "cleaningSprayBottles")
            {
                Weapons.ResetStats(8);
            }
            else if (name == "cleaningGloves")
            {
                Weapons.ResetStats(9);
            }
            else if (name == "stapler")
            {
                Weapons.ResetStats(10);
            }
            else if (name == "officePhone")
            {
                Weapons.ResetStats(11);
            }
            else if (name == "ladder")
            {
                Weapons.ResetStats(12);
            }
        }
    }
}