using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabBox : MonoBehaviour
{
    private bool isGrab=false;
    public Animator animator;
    [SerializeField]
    private GameObject grabbedObject;
    private int Grabnum=0;
    public Rigidbody rb;
    public int isLeftorRight;
    public bool isGrabbing=false;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetMouseButtonDown(isLeftorRight))
        {
            isGrab=true;
        }
        if(Input.GetMouseButtonUp(isLeftorRight))
        {
            isGrab=false;
        }
        if(isGrab)
        {
             if(isLeftorRight == 0)
            {
                PlayerControl.instance.isLeft=true;
                animator.SetBool("isLeftHandUp", true);
            }
            else if(isLeftorRight == 1)
            {
                PlayerControl.instance.isRight=true;
                animator.SetBool("isRightHandUp", true);
            }
            if(grabbedObject!=null&&Grabnum==0)
            {
                FixedJoint fx=grabbedObject.AddComponent<FixedJoint>();
                fx.connectedBody=rb;//连接刚体
                fx.breakForce=10000;//断开力
                Grabnum=1;
                
            }
            //Grabnum=1;
            
        }
        if(!isGrab)
        {
            if(isLeftorRight == 0)
            {
                PlayerControl.instance.isLeft=false;
                animator.SetBool("isLeftHandUp", false);
            }
            else if(isLeftorRight == 1)
            {
                PlayerControl.instance.isRight=false;
                animator.SetBool("isRightHandUp", false);
            }
            if(grabbedObject!=null&&Grabnum==1)
            {
                Destroy(grabbedObject.GetComponent<FixedJoint>());
                Grabnum=0;
            }
            //Grabnum=0;
            
            
        }
        
        if (PlayerControl.instance.isEmpty)
        {
            //如果grabbleObject身上存在FixedJoint组件
            if(grabbedObject!=null&&grabbedObject.GetComponent<FixedJoint>()!=null)
            {
                //grabbedObject身上有多个FixedJoint组件，所以要用GetComponents<FixedJoint>()，销毁所有的FixedJoint组件
                FixedJoint[] fx=grabbedObject.GetComponents<FixedJoint>();
                for(int i=0;i<fx.Length;i++)
                {
                    Destroy(fx[i]);
                } 
            }
            
            

        }
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="Box")
        {
            Debug.Log("Box");
            grabbedObject=other.gameObject;
        }
        
    }

    private void OnTriggerExit(Collider other) 
    {
        grabbedObject=null;
    }
}
