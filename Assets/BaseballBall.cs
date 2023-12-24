using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.MLAgents;
using UnityEngine;

public class BaseballBall : MonoBehaviour
{
    public PitcherAgent _Agent;
    public Transform StrikeZone;
    public Rigidbody _rb;

    float distance = 18.44f;
    
    private void OnCollisionEnter(Collision collision)
    {
        // 지면과 닿을 때,
        if (collision.transform.CompareTag("ground"))
        {
            _Agent.AddReward(calculateDistance());         // 스트라이크 존에 도달한 정도에 비례하여 보상량 결정 max:5

            _Agent.EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 스트라이크 존에 닿을 때,
        if (other.CompareTag("StrikeZone"))
        {
            _Agent.AddReward(10);                           // 차별되는 큰 보상 지급                     
            _Agent.AddReward(_rb.velocity.z * 1.5f);        // 더욱 빠르게 던지는 것을 유도하기 위해

            _Agent.EndEpisode();
        }
    }

    /// <summary>
    /// 전체 수평 거리에 대한 야구공과의 수평 거리의 비율
    /// </summary>
    /// <returns></returns>
    public float calculateDistance()
    {
        distance = Academy.Instance.EnvironmentParameters.GetWithDefault("strikezone_offset", distance);

        float Dist = Mathf.Sqrt(Mathf.Pow(transform.localPosition.x - StrikeZone.localPosition.x, 2) + Mathf.Pow(transform.localPosition.z - StrikeZone.localPosition.z, 2)) / distance;

        var statsRecorder = Academy.Instance.StatsRecorder;
        statsRecorder.Add("Distance_Between_Ball_and_StrikeZone", Dist);

        return 5 * (1 - Dist);
    }

}