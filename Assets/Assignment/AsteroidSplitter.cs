using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents;

namespace Asteroids
{
    public class AsteroidSplitter : MonoBehaviour
    {
        [Header("SPLIT PROPERTIES:")]
        public Asteroid asteroidPrefab;
        [Tooltip("How many fragments an asteroid splits into")]
        [SerializeField] int splitAmount;
        [Tooltip("How big does an asteroid have to be in order to split")]
        [SerializeField] float sizeToSplit;

        [Header("HITLAG PROPERTIES:")]
        [SerializeField] ScriptableEventFloat onHitlag;
        [SerializeField] float splitHitlagDuration;

        /// <summary>
        /// Spawns a few splitted asteroids at this position
        /// </summary>
        /// <param name="_destroyPosition">Position at which asteroid was destroyed at</param>
        public void SplitAsteroids(Transform _destroyTransform) //I could do that the event passes through both position and the shape, which can let me customize the size to split treshold in this class, instead of on the asteroid itself
        {
            if (_destroyTransform.localScale.x > sizeToSplit || _destroyTransform.localScale.y > sizeToSplit)
            {
                for (int i = 0; i < splitAmount; i++)
                {
                    //Set spawn position for asteroidlet
                    Vector2 spawnPosition = _destroyTransform.position;
                    spawnPosition = spawnPosition + Random.insideUnitCircle * _destroyTransform.localScale;
                    Asteroid asteroid = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);

                    //Configure asteroidlet
                    asteroid.SetSize(_destroyTransform.localScale.x / 2); //This part is janky but uhhhhh

                    //Do hitlag
                    onHitlag.Raise(splitHitlagDuration);
                }
            }
            
        }
    }
}


