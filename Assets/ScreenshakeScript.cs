using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents;

public class ScreenshakeScript : MonoBehaviour
{
    [SerializeField] ScriptableEventFloat onScreenshakeEvent;
    [SerializeField] Transform cameraTransform;

    [Header("SCREENSHAKE PROPERTIES:")]
    [SerializeField] float magnitude;
    float shakeTimeRemaining;
    float shakePower;
    float shakeFadeTime;
    private void Screenshake(float _duration)
    {
        shakeTimeRemaining = _duration;
        shakePower = magnitude;
        shakeFadeTime = magnitude / _duration;

        
    }

    public void LateUpdate()
    {

        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.unscaledDeltaTime;

            float x = Random.Range(-1f, 1) * shakePower;
            float y = Random.Range(-1f, 1) * shakePower;

            cameraTransform.position = new Vector3(x, y, -10f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.unscaledDeltaTime);

            
        }

        
    }

    private void OnEnable()
    {
        onScreenshakeEvent.Register(Screenshake);
    }

    private void OnDisable()
    {
        onScreenshakeEvent.Unregister(Screenshake);
    }
}
