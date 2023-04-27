using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CopyMotion : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField]
    private ConfigurableJoint cj;
    private Rigidbody body;
    private Quaternion _initialJointsRotation;
    public bool mirrol;
    // Start is called before the first frame update
    void Start()
    {
        cj=GetComponent<ConfigurableJoint>();
        _initialJointsRotation = cj.transform.localRotation;
        
    }

    // Update is called once per frame
    private void FixedUpdate() 
    {
        UpdateJointTargets();
    }
    private void UpdateJointTargets() 
    {
        if(!mirrol)
        {
            ConfigurableJointExtensions.SetTargetRotationLocal(cj, targetTransform.localRotation, _initialJointsRotation);
        }
        else
        {
            ConfigurableJointExtensions.SetTargetRotationLocal(cj, Quaternion.Euler(0,180,0)*targetTransform.localRotation, _initialJointsRotation);
        }
        
    }
}
