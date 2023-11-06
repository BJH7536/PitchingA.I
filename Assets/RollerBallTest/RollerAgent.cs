using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgentsExamples;

public class RollerAgent : Agent
{
    Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    public Transform Target;
    public override void OnEpisodeBegin()
    {
        // Agent가 떨어지면, momentum을 zero로
        if(transform.localPosition.y <= 0)
        {
            rBody.angularVelocity = Vector3.zero;
            rBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 0.5f, 0);
        }

        // Target을 새로운 곳으로 옮기기
        Target.localPosition = new Vector3(Random.value * 8 - 4, 0.25f, Random.value * 8 - 4);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target과 Agent의 위치
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(transform.localPosition);

        // Agent의 velocity
        sensor.AddObservation(rBody.velocity.x);
        sensor.AddObservation(rBody.velocity.y);

    }

    public float forceMultiplier = 10;
    public float distance;
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // 보상은 Target과의 거리와 관련있다.
        float distanceToTarget = Vector3.Distance(transform.localPosition, Target.localPosition);
        distance = distanceToTarget;

        // Target에 닿았을 때 보상을 주고 에피소드 재시작
        if (distanceToTarget < 0.9f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // platform에서 떨어진다면 에피소드 재시작
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
