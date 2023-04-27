using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator animator;
    public float speed = 150.0f;
    public float strafSpeed = 100.0f;
    public float jumpForce = 60000.0f;
    public bool isGround=false;
    public Rigidbody Hips;
    // Start is called before the first frame update
    void Start()
    {
        Hips = GetComponent<Rigidbody>();
    }
    private void FixedUpdate() 
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Hips.AddForce(Vector3.forward * vertical * speed);
        Hips.AddForce(Vector3.right * horizontal * speed*0.7f);
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
            if(isGround)
            {
                Hips.AddForce(new Vector3(0,jumpForce,0));
                isGround = false;
            }
            
        }
    }
    
}
