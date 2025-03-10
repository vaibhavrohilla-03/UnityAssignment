using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CallNPCOutside : MonoBehaviour
{
    public List<NavMeshAgent> agents;
    public List<GameObject> doors;
    public Transform target;
    public float spacing = 2f;

    private Dictionary<NavMeshAgent, Vector3> initialPositions = new Dictionary<NavMeshAgent, Vector3>();
    private Dictionary<NavMeshAgent, Quaternion> initialRotations = new Dictionary<NavMeshAgent, Quaternion>();

    public void StartMoving()
    {   
        StopAllCoroutines();
        StartCoroutine(MoveAgents());
    }

    private IEnumerator MoveAgents()
    {
        SetDoorsActive(false);

        foreach (var agent in agents)
        {
            initialPositions[agent] = agent.transform.position;
            initialRotations[agent] = agent.transform.rotation;
            agent.GetComponent<Collider>().enabled = false;
        }

        for (int i = 0; i < agents.Count; i++)
        {
            Vector3 newPosition = target.position + new Vector3(0, 0, i * spacing);
            agents[i].SetDestination(newPosition);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitUntil(() => AllAgentsReachedDestination());

        yield return new WaitForSeconds(10f);
        StartCoroutine(ReturnAgents());
    }

    private IEnumerator ReturnAgents()
    {
        foreach (var agent in agents)
        {
            agent.SetDestination(initialPositions[agent]);
            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitUntil(() => AllAgentsReturned());

        foreach (var agent in agents)
        {
            LeanTween.rotate(agent.gameObject, initialRotations[agent].eulerAngles, 1f).setEase(LeanTweenType.easeOutQuad);
            agent.GetComponent<Collider>().enabled = true;
        }

        SetDoorsActive(true);
    }

    private bool AllAgentsReachedDestination()
    {
        foreach (var agent in agents)
        {
            if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                return false;
        }
        return true;
    }

    private bool AllAgentsReturned()
    {
        foreach (var agent in agents)
        {
            if (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
                return false;
        }
        return true;
    }

    private void SetDoorsActive(bool state)
    {
        foreach (var door in doors)
        {
            door.SetActive(state);
        }
    }
}
