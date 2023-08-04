//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.XR.CoreUtils;
//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;
//public class ContinuousMovement : MonoBehaviour
//{
//    public float speed = 1;
//    public XRNode inputSource;
//    public float gravity = -9.8f;
//    public LayerMask groundLayer;
//    public float additionalHeight = 0.2f;

//    private float fallingSpeed;
//    private XROrigin xROrigin;
//    private Vector2 inputAxis;
//    private CharacterController character;

//    // Start is called before the first frame update
//    void Start()
//    {
//        character = GetComponent<CharacterController>();
//        xROrigin = GetComponent<XROrigin>();
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
//        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
//    }

//    void FixedUpdate()
//    {
//        CapsuleFollowHeadset();

//        Quaternion headYaw = Quaternion.Euler(0, xROrigin.CameraFloorOffsetObject.transform.eulerAngles.y, 0);
//        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

//        character.Move(direction * Time.fixedDeltaTime * speed);

//        // Gravity
//        bool isGrounded = CheckIfGrounded();
//        if (isGrounded)
//            fallingSpeed = 0;
//        else 
//            fallingSpeed += gravity * Time.fixedDeltaTime;
        
//        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
//    }
//    void CapsuleFollowHeadset()
//    {
//        character.height = xROrigin.CameraInOriginSpaceHeight + additionalHeight;
//        Vector3 capsuleCenter = transform.InverseTransformPoint(xROrigin.CameraFloorOffsetObject.transform.position);
//        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
//    }

//    bool CheckIfGrounded()
//    {
//        // Tells us if on ground

//        Vector3 rayStart = transform.TransformPoint(character.center);
//        float rayLength = character.center.y + 0.01f;
//        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
//        return hasHit;
//    }
//}
