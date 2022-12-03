using JAA.Data;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    private const float MinimalDistance = 1;
    
    [SerializeField] private NavMeshAgent _agent;
    
    private Transform _heroTransform;

    private void Update()
    {
      if(IsInitialized() && IsHeroNotReached())
        _agent.destination = _heroTransform.position;
    }

    public void Construct(Transform heroObjectTransform)
    {
      _heroTransform = heroObjectTransform;
    }

    private bool IsInitialized() => 
      _heroTransform != null;

    private bool IsHeroNotReached() => 
      _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}