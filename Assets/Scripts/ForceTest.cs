using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTest : MonoBehaviour
{
   
    public float force = 1.0f;
    public int layerMask;
    public int isLeftorRight;

    private Dictionary<GameObject, FixedJoint> grabbedObjects = new Dictionary<GameObject, FixedJoint>();

    [SerializeField]
    private bool isGrabbing = false;

    private void Start()
    {
        layerMask = LayerMask.GetMask("Wall","Ground");
    }

    private void FixedUpdate()
    {
        //Debug.Log("状态" + isGrabbing);
        if (Input.GetMouseButtonDown(isLeftorRight))
        {
            isGrabbing = true;
        }
        if (Input.GetMouseButtonUp(isLeftorRight))
        {
            isGrabbing = false;
        }
       
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.15f, layerMask);
        if (colliders.Length > 0)
        {
            if (PlayerControl.instance.isHand)
            {
                PlayerControl.instance.isHandWall = true;
            }
            else
            {
                PlayerControl.instance.isHandWall = false;
            }

            // 如果鼠标按下
            if (isGrabbing && !grabbedObjects.ContainsKey(colliders[0].gameObject))
            {
                CameraControl.instance.isCameraLock = true;
                FixedJoint fixedJoint = colliders[0].gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = GetComponent<Rigidbody>();
                fixedJoint.breakForce = 10000;

                grabbedObjects.Add(colliders[0].gameObject, fixedJoint);
            }
            // 如果鼠标抬起
            if (!isGrabbing && grabbedObjects.ContainsKey(colliders[0].gameObject))
            {
                //CameraControl.instance.isCameraLock = false;
                FixedJoint fixedJoint = grabbedObjects[colliders[0].gameObject];
                Destroy(fixedJoint);

                grabbedObjects.Remove(colliders[0].gameObject);
            }
        }

        if (PlayerControl.instance.isEmpty)
        {
            CameraControl.instance.isCameraLock = false;
            foreach (KeyValuePair<GameObject, FixedJoint> pair in grabbedObjects)
            {
                FixedJoint fixedJoint = pair.Value;
                Destroy(fixedJoint);
            }

            grabbedObjects.Clear();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.15f);
    }
}
