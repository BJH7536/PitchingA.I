using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using BodyPart = Unity.MLAgentsExamples.BodyPart;
using Random = UnityEngine.Random;

public class PitcherAgent : Agent
{
    [Header("Target To Walk Towards")] 
    public Transform target;  // StrikeZone

    [Header("Body Parts")] 
    public Transform Hips;
    public Transform LeftUpLeg;
    public Transform LeftLeg;
    public Transform LeftFoot;
    public Transform LeftToeBase;

    public Transform RightUpLeg;
    public Transform RightLeg;
    public Transform RightFoot;
    public Transform RightToeBase;

    public Transform Spine1;

    public Transform LeftShoulder;
    public Transform LeftArm;
    public Transform LeftForeArm;
    public Transform LeftHand;

    public Transform Head;

    public Transform RightShoulder;
    public Transform RightArm;
    public Transform RightForeArm;
    public Transform RightHand;

    [Header("Right Hand Fingers")]
    public Transform RightHandIndex1;
    public Transform RightHandIndex2;
    public Transform RightHandIndex3;
    public Transform RightHandMiddle1;
    public Transform RightHandMiddle2;
    public Transform RightHandMiddle3;
    public Transform RightHandPinky1;
    public Transform RightHandPinky2;
    public Transform RightHandPinky3;
    public Transform RightHandRing1;
    public Transform RightHandRing2;
    public Transform RightHandRing3;
    public Transform RightHandThumb1;
    public Transform RightHandThumb2;
    public Transform RightHandThumb3;

    JointDriveController m_JdController;

    public override void Initialize()
    {
        m_JdController = GetComponent<JointDriveController>();
        m_JdController.SetupBodyPart(Hips);
        m_JdController.SetupBodyPart(LeftUpLeg);
        m_JdController.SetupBodyPart(LeftLeg);
        m_JdController.SetupBodyPart(LeftFoot);
        m_JdController.SetupBodyPart(LeftToeBase);
        m_JdController.SetupBodyPart(RightUpLeg);
        m_JdController.SetupBodyPart(RightLeg);
        m_JdController.SetupBodyPart(RightFoot);
        m_JdController.SetupBodyPart(RightToeBase);
        m_JdController.SetupBodyPart(Spine1);
        m_JdController.SetupBodyPart(LeftShoulder);
        m_JdController.SetupBodyPart(LeftArm);
        m_JdController.SetupBodyPart(LeftForeArm);
        m_JdController.SetupBodyPart(LeftHand);
        m_JdController.SetupBodyPart(Head);
        m_JdController.SetupBodyPart(RightShoulder);
        m_JdController.SetupBodyPart(RightArm);
        m_JdController.SetupBodyPart(RightForeArm);
        m_JdController.SetupBodyPart(RightHand);
        m_JdController.SetupBodyPart(RightHandIndex1);
        m_JdController.SetupBodyPart(RightHandIndex2);
        m_JdController.SetupBodyPart(RightHandIndex3);
        m_JdController.SetupBodyPart(RightHandMiddle1);
        m_JdController.SetupBodyPart(RightHandMiddle2);
        m_JdController.SetupBodyPart(RightHandMiddle3);
        m_JdController.SetupBodyPart(RightHandPinky1);
        m_JdController.SetupBodyPart(RightHandPinky2);
        m_JdController.SetupBodyPart(RightHandPinky3);
        m_JdController.SetupBodyPart(RightHandRing1);
        m_JdController.SetupBodyPart(RightHandRing2);
        m_JdController.SetupBodyPart(RightHandRing3);
        m_JdController.SetupBodyPart(RightHandThumb1);
        m_JdController.SetupBodyPart(RightHandThumb2);
        m_JdController.SetupBodyPart(RightHandThumb3);

    }

    /// <summary>
    /// Loop over body parts and reset them to initial conditions.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        ////Reset all of the body parts
        //foreach (var bodyPart in m_JdController.bodyPartsDict.Values)
        //{
        //    bodyPart.Reset(bodyPart);
        //}

        ////Random start rotation to help generalize
        //hips.rotation = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);

        //UpdateOrientationObjects();

        ////Set our goal walking speed
        //MTargetWalkingSpeed =
        //    randomizeWalkSpeedEachEpisode ? Random.Range(0.1f, m_maxWalkingSpeed) : MTargetWalkingSpeed;

        //SetResetParameters();
    }

    /// <summary>
    /// Add relevant information on each body part to observations.
    /// </summary>
    public void CollectObservationBodyPart(BodyPart bp, VectorSensor sensor)
    {
        ////GROUND CHECK
        //sensor.AddObservation(bp.groundContact.touchingGround); // Is this bp touching the ground

        ////Get velocities in the context of our orientation cube's space
        ////Note: You can get these velocities in world space as well but it may not train as well.
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformDirection(bp.rb.velocity));
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformDirection(bp.rb.angularVelocity));

        ////Get position relative to hips in the context of our orientation cube's space
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformDirection(bp.rb.position - hips.position));

        //if (bp.rb.transform != hips && bp.rb.transform != handL && bp.rb.transform != handR)
        //{
        //    sensor.AddObservation(bp.rb.transform.localRotation);
        //    sensor.AddObservation(bp.currentStrength / m_JdController.maxJointForceLimit);
        //}
    }

    /// <summary>
    /// Loop over body parts to add them to observation.
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)
    {
        //var cubeForward = m_OrientationCube.transform.forward;

        ////velocity we want to match
        //var velGoal = cubeForward * MTargetWalkingSpeed;
        ////ragdoll's avg vel
        //var avgVel = GetAvgVelocity();

        ////current ragdoll velocity. normalized
        //sensor.AddObservation(Vector3.Distance(velGoal, avgVel));
        ////avg body vel relative to cube
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformDirection(avgVel));
        ////vel goal relative to cube
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformDirection(velGoal));

        ////rotation deltas
        //sensor.AddObservation(Quaternion.FromToRotation(hips.forward, cubeForward));
        //sensor.AddObservation(Quaternion.FromToRotation(head.forward, cubeForward));

        ////Position of target position relative to cube
        //sensor.AddObservation(m_OrientationCube.transform.InverseTransformPoint(target.transform.position));

        //foreach (var bodyPart in m_JdController.bodyPartsList)
        //{
        //    CollectObservationBodyPart(bodyPart, sensor);
        //}
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)

    {
        //var bpDict = m_JdController.bodyPartsDict;
        //var i = -1;

        //var continuousActions = actionBuffers.ContinuousActions;
        //bpDict[hips].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], continuousActions[++i]);
        //bpDict[spine].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], continuousActions[++i]);

        //bpDict[thighL].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], 0);
        //bpDict[thighR].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], 0);
        //bpDict[shinL].SetJointTargetRotation(continuousActions[++i], 0, 0);
        //bpDict[shinR].SetJointTargetRotation(continuousActions[++i], 0, 0);
        //bpDict[footR].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], continuousActions[++i]);
        //bpDict[footL].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], continuousActions[++i]);

        //bpDict[armL].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], 0);
        //bpDict[armR].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], 0);
        //bpDict[forearmL].SetJointTargetRotation(continuousActions[++i], 0, 0);
        //bpDict[forearmR].SetJointTargetRotation(continuousActions[++i], 0, 0);
        //bpDict[head].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], 0);

        ////update joint strength settings
        //bpDict[chest].SetJointStrength(continuousActions[++i]);
        //bpDict[spine].SetJointStrength(continuousActions[++i]);
        //bpDict[head].SetJointStrength(continuousActions[++i]);
        //bpDict[thighL].SetJointStrength(continuousActions[++i]);
        //bpDict[shinL].SetJointStrength(continuousActions[++i]);
        //bpDict[footL].SetJointStrength(continuousActions[++i]);
        //bpDict[thighR].SetJointStrength(continuousActions[++i]);
        //bpDict[shinR].SetJointStrength(continuousActions[++i]);
        //bpDict[footR].SetJointStrength(continuousActions[++i]);
        //bpDict[armL].SetJointStrength(continuousActions[++i]);
        //bpDict[forearmL].SetJointStrength(continuousActions[++i]);
        //bpDict[armR].SetJointStrength(continuousActions[++i]);
        //bpDict[forearmR].SetJointStrength(continuousActions[++i]);
    }

    void FixedUpdate()
    {
    }

}