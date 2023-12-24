using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;
using Unity.MLAgents.Sensors;
using BodyPart = Unity.MLAgentsExamples.BodyPart;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Data;

public class PitcherAgent : Agent
{
    [Header("Target To Hit")] 
    public Transform StrikeZone;

    [Header("Body Parts")] 
    public Transform Hips;
    public Transform LeftUpLeg;
    public Transform LeftLeg;
    public Transform LeftFoot;

    public Transform RightUpLeg;
    public Transform RightLeg;
    public Transform RightFoot;

    public Transform Spine;

    public Transform LeftArm;
    public Transform LeftForeArm;
    public Transform LeftHand;

    public Transform Head;

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

    [Header("Right Hand Fingers")]
    public GameObject BaseballBall;

    JointDriveController m_JdController;
    Vector3 BallInitPos;
    float distance = 18.44f;

    public override void Initialize()
    {
        //Setup each body part
        m_JdController = GetComponent<JointDriveController>();
        m_JdController.SetupBodyPart(Hips);
        m_JdController.SetupBodyPart(LeftUpLeg);
        m_JdController.SetupBodyPart(LeftLeg);
        m_JdController.SetupBodyPart(LeftFoot);
        m_JdController.SetupBodyPart(RightUpLeg);
        m_JdController.SetupBodyPart(RightLeg);
        m_JdController.SetupBodyPart(RightFoot);
        m_JdController.SetupBodyPart(Spine);
        m_JdController.SetupBodyPart(LeftArm);
        m_JdController.SetupBodyPart(LeftForeArm);
        m_JdController.SetupBodyPart(LeftHand);
        m_JdController.SetupBodyPart(Head);
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

        BallInitPos = BaseballBall.transform.localPosition;
    }

    /// <summary>
    /// Loop over body parts and reset them to initial conditions.
    /// </summary>
    public override void OnEpisodeBegin()
    {
        BaseballBall.transform.localPosition = BallInitPos;
        BaseballBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        BaseballBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // Curriculum Learning에 따른 스트라이크 존과의 거리 재조정
        StrikeZone.localPosition = new Vector3(0, -0.274f, Academy.Instance.EnvironmentParameters.GetWithDefault("strikezone_offset", distance));

        //Reset all of the body parts
        foreach (var bodyPart in m_JdController.bodyPartsDict.Values)
        {
            bodyPart.Reset(bodyPart);
        }
    }

    /// <summary>
    /// Add relevant information on each body part to observations.
    /// </summary>
    public void CollectObservationBodyPart(BodyPart bp, VectorSensor sensor)
    {
        //GROUND CHECK
        sensor.AddObservation(bp.groundContact.touchingGround); // Is this bp touching the ground

        // BodyPart로부터 무엇을 관측해야 할까?

        if (bp.rb.transform != Hips)            // Hips를 제외한 모든 BodyPart에 대한 관측
        {
            sensor.AddObservation(bp.rb.transform.localRotation);
            sensor.AddObservation(bp.currentStrength / m_JdController.maxJointForceLimit);
        }
    }

    /// <summary>
    /// Loop over body parts to add them to observation.
    /// </summary>
    public override void CollectObservations(VectorSensor sensor)       // 관측값을 Brain한테 전달
    {
        // [관측값]
        // 야구공의 3차원 로컬 좌표 : 3개
        // 스트라이크 존의 3차원 로컬 좌표 : 3개
        // 모든 BodyPart의 로컬 회전값 (4차원) + 가해지는 힘 + touchingGround유무 : 29(Hip제외) * 6 + 1(Hip) * 1 = 175개

        sensor.AddObservation(BaseballBall.transform.localPosition);
        sensor.AddObservation(StrikeZone.localPosition);

        foreach (var bodyPart in m_JdController.bodyPartsList)
        {
            CollectObservationBodyPart(bodyPart, sensor);
        }
    }

    // Brain으로부터 받은 action들을 각 관절들에다가 적용.
    public override void OnActionReceived(ActionBuffers actionBuffers)      // Brain으로부터 받은 Action들
    {
        var bpDict = m_JdController.bodyPartsDict;
        var i = -1;

        var continuousActions = actionBuffers.ContinuousActions;

        // 29 * 4  = 116

        foreach(var bodyPart in bpDict.Keys)
        {
            if (bodyPart.transform != Hips) { 
                bpDict[bodyPart].SetJointTargetRotation(continuousActions[++i], continuousActions[++i], continuousActions[++i]);
                //update joint strength settings
                bpDict[bodyPart].SetJointStrength(continuousActions[++i]);
            }
        }

        // 머리의 높이가 엉덩이의 높이까지 내려오면,
        if(Head.position.y <= Hips.position.y)
        {
            var statsRecorder = Academy.Instance.StatsRecorder;
            statsRecorder.Add("Distance_Between_Ball_and_StrikeZone", BaseballBall.GetComponent<BaseballBall>().calculateDistance());

            AddReward(-50);     // 패널티
            EndEpisode();       // Episode 종료
        }
    }

    // 공과 닿아있는동안 Reward를 -Mathf.Pow(1.8f, StepCount - 30) 만큼 감소
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("BaseballBall"))
        {
            AddReward(-Mathf.Pow(1.8f, StepCount - 30));
        }
    }
}