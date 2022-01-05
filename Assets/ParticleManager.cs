using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ScriptableEventVector3 onAsteroidSplitParticleEvent;
    [SerializeField] GameObject splitParticlePrefab;

    private void CreateAsteroidSplitParticle(Vector3 _position)
    {
        Instantiate(splitParticlePrefab, _position, Quaternion.identity);
    }
    private void OnEnable()
    {
        onAsteroidSplitParticleEvent.Register(CreateAsteroidSplitParticle);
    }

    private void OnDisable()
    {
        onAsteroidSplitParticleEvent.Unregister(CreateAsteroidSplitParticle);
    }
}
