using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ActiveRagdoll {
    // Author: Sergio Abreu García | https://sergioabreu.me

    /// <summary> Tells the ActiveRagdoll what it should do. Input can be external (like the
    /// one from the player or from another script) and internal (kind of like sensors, such as
    /// detecting if it's on floor). </summary>
    public class InputModule : Module {
        // ---------- EXTERNAL INPUT ----------

        public delegate void onMoveDelegate(Vector2 movement);//委托
        public onMoveDelegate OnMoveDelegates { get; set; }//委托
        public void OnMove(InputValue value)//委托
        {
            OnMoveDelegates?.Invoke(value.Get<Vector2>());//委托
        }

        public delegate void onLeftArmDelegate(float armWeight);
        public onLeftArmDelegate OnLeftArmDelegates { get; set; }
        public void OnLeftArm(InputValue value) {
            OnLeftArmDelegates?.Invoke(value.Get<float>());
        }

        public delegate void onRightArmDelegate(float armWeight);
        public onRightArmDelegate OnRightArmDelegates { get; set; }
        public void OnRightArm(InputValue value) {
            OnRightArmDelegates?.Invoke(value.Get<float>());
        }

        // ---------- INTERNAL INPUT ----------

        [Header("--- FLOOR ---")]
        public float floorDetectionDistance = 0.3f;//检测距离
        public float maxFloorSlope = 60;//最大斜坡角度

        private bool _isOnFloor = true;//是否在地面上
        public bool IsOnFloor { get { return _isOnFloor; } }//是否在地面上

        Rigidbody _rightFoot, _leftFoot;


        void Start() {
            _rightFoot = _activeRagdoll.GetPhysicalBone(HumanBodyBones.RightFoot).GetComponent<Rigidbody>();//获取右脚
            _leftFoot = _activeRagdoll.GetPhysicalBone(HumanBodyBones.LeftFoot).GetComponent<Rigidbody>();//获取左脚
        }

        void Update() {
            UpdateOnFloor();
        }

        public delegate void onFloorChangedDelegate(bool onFloor);
        public onFloorChangedDelegate OnFloorChangedDelegates { get; set; }
        private void UpdateOnFloor() {
            bool lastIsOnFloor = _isOnFloor;//上一帧是否在地面上

            _isOnFloor = CheckRigidbodyOnFloor(_rightFoot, out Vector3 foo)
                         || CheckRigidbodyOnFloor(_leftFoot, out foo);//检测是否在地面上

            if (_isOnFloor != lastIsOnFloor)
                OnFloorChangedDelegates(_isOnFloor);
        }

        /// <summary>
        /// 检查刚体是否在地面上
        /// </summary>
        /// <param name="bodyPart">Part of the body to check</param
        /// <returns> True if the Rigidbody is on floor </returns>
        public bool CheckRigidbodyOnFloor(Rigidbody bodyPart, out Vector3 normal)//检查刚体是否在地面上
        {
            // Raycast
            Ray ray = new Ray(bodyPart.position, Vector3.down);//向下发射射线
            bool onFloor = Physics.Raycast(ray, out RaycastHit info, floorDetectionDistance, ~(1 << bodyPart.gameObject.layer));//检测是否在地面上

            // Additional checks
            onFloor = onFloor && Vector3.Angle(info.normal, Vector3.up) <= maxFloorSlope;

            if (onFloor && info.collider.gameObject.TryGetComponent<Floor>(out Floor floor))//如果检测到地面
                    onFloor = floor.isFloor;

            normal = info.normal;
            return onFloor;
        }
    }
} // namespace ActiveRagdoll