using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfColiderisGround : MonoBehaviour
{
    public PlayerControl playerControl;
    // Start is called before the first frame update
    void Start()
    {
        playerControl = GameObject.FindObjectOfType<PlayerControl>().GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("碰撞到了");
        if(other.gameObject.tag=="Ground")
        playerControl.isGround = true;
    }
}
