using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour {

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform rightFootObj = null;
    public Transform leftFootObj = null;    
    public Transform lookObj = null;
    public float IKWeight=1.0f;

    void Start ()
    {
        animator = GetComponent<Animator>();
        Transform topmostParentTransform = transform.root; 
        print("Topmost Parent: " + topmostParentTransform.name);    
        //find child or grandchild object with name "RH"
        rightHandObj = topmostParentTransform.FindRecursive("RH");
        print("Right Hand Obj: " + rightHandObj.name);


       
        GameObject go = GameObject.Find("RH");
        if (go!=null)
        print("Right Hand Obj: " + go.name);
        else
        print("Right Hand Obj not found!");
        
        //rightHandObj = go.transform;//topmostParentTransform.transform.Find("RH");//.transform;
        leftHandObj = topmostParentTransform.transform.FindRecursive("LH").transform;  
        rightFootObj = topmostParentTransform.transform.FindRecursive("RF");//.transform;  
        leftFootObj = topmostParentTransform.transform.FindRecursive("LF");//.transform;  
        lookObj = Camera.main.transform;    
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {
        if(animator) {
       
            //if the IK is active, set the position and rotation directly to the goal.
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand,IKWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand,IKWeight);  
                    animator.SetIKPosition(AvatarIKGoal.RightHand,rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand,rightHandObj.rotation);
                }
                // Set the left hand target position and rotation, if one has been assigned
                if(leftHandObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,IKWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,IKWeight);  
                    animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHandObj.rotation);     
                }
                // Set the right foot target position and rotation, if one has been assigned
                if(rightFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,IKWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,IKWeight);  
                    animator.SetIKPosition(AvatarIKGoal.RightFoot,rightFootObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightFoot,rightFootObj.rotation);
                }
                // Set the left foot target position and rotation, if one has been assigned
                if(leftFootObj != null) {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,IKWeight);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,IKWeight);  
                    animator.SetIKPosition(AvatarIKGoal.LeftFoot,leftFootObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftFoot,leftFootObj.rotation);
                }   
            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {          
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,0);  

                animator.SetLookAtWeight(0);
            }
        }
    }
}

public static class TransformExtensions
{
    /// <summary>
    /// Recursively searches for a child or grandchild Transform by name.
    /// </summary>
    /// <param name="parent">The parent Transform to start the search from.</param>
    /// <param name="name">The name of the Transform to find.</param>
    /// <returns>The found Transform, or null if not found.</returns>
    public static Transform FindRecursive(this Transform parent, string name)
    {
        // Check immediate children first
        Transform foundTransform = parent.Find(name);
        if (foundTransform != null)
        {
            return foundTransform;
        }

        // Recursively search in children's children
        foreach (Transform child in parent)
        {
            foundTransform = child.FindRecursive(name);
            if (foundTransform != null)
            {
                return foundTransform;
            }
        }
        return null;
    }
}
