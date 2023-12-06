using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBall : MonoBehaviour
{
    public PitcherAgent _Agent;
    public Transform StrikeZone;
    public Rigidbody _rb;

    float distance = 18.44f;

    private void OnCollisionEnter(Collision collision)
    {
        // 지면과 닿을 때, reward에 따라 보상 및 EndEpisode
        if (collision.transform.CompareTag("ground"))
        {
            float reward = 5 - 5 * (transform.localPosition - StrikeZone.localPosition).magnitude / distance;

            Debug.Log("Reward : " + reward);

            _Agent.AddReward(reward);
            _Agent.EndEpisode();
        }
        // 스트라이크 존에 닿을 때, 보상 1 및 EndEpisode
        else if (collision.transform.CompareTag("StrikeZone"))
        {
            _Agent.AddReward(1);
            _Agent.EndEpisode();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 손에서 떨어질 때, z방향 가속도와 비례하여 보상.
        if (collision.transform.CompareTag("PitcherRightHand"))
        {
            _Agent.AddReward(_rb.velocity.z);
        }
    }

}