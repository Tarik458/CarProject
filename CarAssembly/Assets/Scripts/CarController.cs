using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float steeringInput;
    private float speedInput;
    private float steeringAngle;
    private float speed = 0;

    public WheelCollider fDriverWheel, fPassengerWheel;
    public WheelCollider rDriverWheel, rPassengerWheel;
    public Transform fDriverTransform, fPassengerTransform;
    public Transform rDriverTransform, rPassengerTransform;

    public float maxSteerAngle = 30;
    public float enginePower = 60;


    public void GetInput()
    {
        steeringInput = Input.GetAxis("Horizontal");
        speedInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        steeringAngle = maxSteerAngle * steeringInput;
        fDriverWheel.steerAngle = steeringAngle;
        fPassengerWheel.steerAngle = steeringAngle;
    }

    private void Accelerate()
    {
        speed = speedInput * enginePower;
        rDriverWheel.motorTorque = speed;
        rPassengerWheel.motorTorque = speed;
    }

    private void UpdateWheelPoseMulti()
    {
        UpdateWheelPose(fDriverWheel, fDriverTransform);
        UpdateWheelPose(fPassengerWheel, fPassengerTransform);
        UpdateWheelPose(rDriverWheel, rDriverTransform);
        UpdateWheelPose(rPassengerWheel, rPassengerTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform)
    {
        Vector3 pos = transform.position;
        Quaternion quat = transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        transform.position = pos;

        transform.rotation = quat;
    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoseMulti();
    }
}
