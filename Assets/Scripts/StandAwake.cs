using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandAwake : MonoBehaviour
{
    private ConfigurableJoint hipJoint;
    public float Changetime=3;
    public float PositionSpring=500;
    private float elapsedTime = 0f;  // 过渡时间流逝
    private float startSpringValue = 0f;
    // Start is called before the first frame update
    private void Awake() {
        hipJoint=GetComponent<ConfigurableJoint>();
        //hipJoint.angularYZDrive.positionSpring在唤醒的5s内从0变为1000
        StartCoroutine(TransitionSpringValue());

    }
     private IEnumerator TransitionSpringValue()
    {
        while (elapsedTime < Changetime)
        {
            // 使用插值函数Lerp实现平滑过渡
            float t = elapsedTime / Changetime;
            float currentSpringValue = Mathf.Lerp(0f, PositionSpring, t);

            // 设置新的弹簧力值
            hipJoint.angularYZDrive= new JointDrive
            {
                positionSpring = currentSpringValue,
                maximumForce = 3.402823e+38f,
            };

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保最终值为目标值
        hipJoint.angularYZDrive= new JointDrive{positionSpring =PositionSpring,
        maximumForce = 3.402823e+38f,};
    }
}
