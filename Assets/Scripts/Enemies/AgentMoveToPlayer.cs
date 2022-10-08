using System;
using JAA.Data;
using JAA.Structure.Factory;
using JAA.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace CodeBase.Enemy
{
  public class AgentMoveToPlayer : Follow
  {
    private const float MinimalDistance = 1;
    
    [SerializeField] private NavMeshAgent _agent;
    
    private Transform _heroTransform;
    private IGameFactory _gameFactory;

    private void Start()
    {
      _gameFactory = AllServices.Container.Single<IGameFactory>();

      if (_gameFactory.HeroObject != null)
        InitializeHeroTransform();
      else
        _gameFactory.HeroCreated += HeroCreated;
    }

    private void Update()
    {
      if(IsInitialized() && IsHeroNotReached())
        _agent.destination = _heroTransform.position;
    }

    private void OnDestroy()
    {
      if(_gameFactory != null)
        _gameFactory.HeroCreated -= HeroCreated;
    }

    private bool IsInitialized() => 
      _heroTransform != null;

    private void HeroCreated() =>
      InitializeHeroTransform();

    private void InitializeHeroTransform() =>
      _heroTransform = _gameFactory.HeroObject.transform;

    private bool IsHeroNotReached() => 
      _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
  }
}