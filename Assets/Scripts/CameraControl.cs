using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public float SpineOffest;
    float mouseX, mouseY;
    public Transform root;
    public bool isCameraLock=false;
    public ConfigurableJoint hipJoint,SpineJoint;
    public float RotateSpeed=0.05f;
    // Start is called before the first frame update
    private void Awake() {
        instance=this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//锁定鼠标
    }

    // Update is called once per frame
    private void FixedUpdate() {
        CamControl();
    }
    void CamControl()
    {
        if(isCameraLock==false)
        {
            mouseX += Input.GetAxis("Mouse X") * RotateSpeed;//获取鼠标移动距离
            mouseY -= Input.GetAxis("Mouse Y") * RotateSpeed;//获取鼠标移动距离
            mouseY = Mathf.Clamp(mouseY, -105, -80);
            root.rotation = Quaternion.Euler(mouseY, mouseX, 0);//旋转角色
            
            hipJoint.targetRotation = Quaternion.Euler(0, -mouseX, 0);//旋转髋关节
            SpineJoint.targetRotation = Quaternion.Euler(-mouseY+SpineOffest, 0, 0);//旋转脊椎关节
        }
        
    }
}
