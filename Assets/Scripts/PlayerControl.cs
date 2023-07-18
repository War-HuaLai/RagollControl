using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;
    public Animator animator;
    public float speed = 150.0f;
    public float strafSpeed = 100.0f;
    public float jumpForce = 12000.0f;
    public bool isGround=false;
    public bool isLeft=false;
    public bool isRight=false;
    public bool isHand=false;
    public bool isEmpty=false;
    public bool isHandWall=false;
    public Camera cam;
    public Rigidbody Hips;
    // Start is called before the first frame update
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        Hips = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() 
    {
       // Debug.Log(isHandWall);
        if(isLeft==true&&isRight==true)
        {
            isHand=true;
        }
        else
        {
            isHand=false;
        }
        if(isLeft==false&&isRight==false)
        {
            isEmpty=true;
        }
        else
        {
            isEmpty=false;
        }
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        

        
        Hips.AddForce(cam.transform.forward * vertical * speed);
        if(CameraControl.instance.isCameraLock==true)
        {
            Hips.AddForce(cam.transform.right * horizontal * speed*2.5f,ForceMode.Force);
        }
        if(isHandWall)
        {
            //Vector3 perpendicularVector = Vector3.Cross(cam.transform.right, cam.transform.up);
            // 将该向量乘以力的大小，并施加力到刚体上
            Hips.AddForce(cam.transform.up*0.4f*vertical, ForceMode.VelocityChange);
        }
        Hips.AddForce(cam.transform.right * horizontal * speed*0.7f);
        if(Input.GetAxisRaw("Vertical")!=0)
        {
            animator.SetBool("isWalk",true);
        }
        else
        {
            animator.SetBool("isWalk",false);
        }
        if(Input.GetAxisRaw("Horizontal")>0)
        {
            animator.SetBool("isRight",true);
            animator.SetBool("isLeft",false);
        }
        else if(Input.GetAxisRaw("Horizontal")<0)
        {
            animator.SetBool("isRight",false);
            animator.SetBool("isLeft",true);
        }
        else
        {
            animator.SetBool("isRight",false);
            animator.SetBool("isLeft",false);
        }
        //如果一直按着A或者D，就会一直旋转
        
        if(Input.GetAxis("Jump")>0)
        {
            Debug.Log("Jump"+Input.GetAxis("Jump"));
            if(isGround)
            {
                Hips.AddForce(new Vector3(0,jumpForce,0));
                isGround = false;
            }
            
        }
    }
    
}
