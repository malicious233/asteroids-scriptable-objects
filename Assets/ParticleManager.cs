using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ScriptableEventVector3 onAsteroidHitParticleEvent;
    [SerializeField] GameObject splitParticlePrefab;

    private void CreateAsteroidSplitParticle(Vector3 _position)
    {
        Instantiate(splitParticlePrefab, _position, Quaternion.identity);
    }
    private void OnEnable()
    {
        onAsteroidHitParticleEvent.Register(CreateAsteroidSplitParticle);
    }

    private void OnDisable()
    {
        onAsteroidHitParticleEvent.Unregister(CreateAsteroidSplitParticle);
    }
}
