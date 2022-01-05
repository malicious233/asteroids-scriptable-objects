using DefaultNamespace.ScriptableEvents;
using UnityEngine;
using Variables;
using ScriptableEvents;
using Random = UnityEngine.Random;
using System.Collections;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [Header("Config:")]
        [SerializeField] private float _minForce;
        [SerializeField] private float _maxForce;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _minTorque;
        [SerializeField] private float _maxTorque;
        [SerializeField] private float _splitSizeTreshold;
        [SerializeField] private float _screenShakeDuration;
        [Header("References:")]
        [SerializeField] private Transform _shape;
        [SerializeField] private DefaultNamespace.ScriptableEvents.ScriptableEventInt _onAsteroidDestroyed;

        [Header("Event References:")]
        [SerializeField] private ScriptableEvents.ScriptableEventTransform _onAsteroidSplitEvent; //Perhaps should give it a better namespace name for this project...
        [SerializeField] private ScriptableEvents.ScriptableEventVector3 _onAsteroidHitParticleEvent;
        [SerializeField] private ScriptableEvents.ScriptableEventFloat _onScreenshakeEvent;

        public Transform Shape
        {
            get
            {
                return _shape;
            }
            set
            {
                _shape = value;
            }
        }


        private Rigidbody2D _rigidbody;
        private Vector3 _direction;
        private int _instanceId;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _instanceId = GetInstanceID();
            
            SetDirection();
            AddForce();
            AddTorque();
            SetSize();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.Equals(other.tag, "Laser"))
            {
               HitByLaser();
            }
        }

        

        private void HitByLaser()
        {
            AttemptSplit();
            _onAsteroidHitParticleEvent.Raise(transform.position);
            _onScreenshakeEvent.Raise(_screenShakeDuration * transform.localScale.magnitude);
            StartCoroutine(WaitForHitlagEnd());
            
        }

        IEnumerator WaitForHitlagEnd() //Make this coroutine accept a method in a parameter to be called when hitlag has ended perhaps?
        {
            while (Time.timeScale != 1.0f)
            yield return null;
            _onAsteroidDestroyed.Raise(_instanceId);
            
            Destroy(gameObject);
        }
        private void AttemptSplit()
        {
            _onAsteroidSplitEvent.Raise(Shape.transform);
        }

        // TODO Can we move this to a single listener, something like an AsteroidDestroyer?
        public void OnHitByLaser(IntReference asteroidId)
        {
            if (_instanceId == asteroidId.GetValue())
            {
                Destroy(gameObject);
            }
        }
        
        public void OnHitByLaserInt(int asteroidId)
        {
            if (_instanceId == asteroidId)
            {
                Destroy(gameObject);
            }
        }

        
        
        private void SetDirection()
        {
            var size = new Vector2(3f, 3f);
            var target = new Vector3
            (
                Random.Range(-size.x, size.x),
                Random.Range(-size.y, size.y)
            );

            _direction = (target - transform.position).normalized;
        }

        private void AddForce()
        {
            var force = Random.Range(_minForce, _maxForce);
            _rigidbody.AddForce( _direction * force, ForceMode2D.Impulse);
        }

        private void AddTorque()
        {
            var torque = Random.Range(_minTorque, _maxTorque);
            var roll = Random.Range(0, 2);

            if (roll == 0)
                torque = -torque;
            
            _rigidbody.AddTorque(torque, ForceMode2D.Impulse);
        }

        private void SetSize()
        {
            var size = Random.Range(_minSize, _maxSize);
            _shape.localScale = new Vector3(size, size, 0f);
        }

        public void SetSize(float _size)
        {
            _shape.localScale = new Vector3(_size, _size, 0f);
        }
    }
}
