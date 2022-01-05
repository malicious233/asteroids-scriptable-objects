using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScriptableEvents;
public class HitlagManager : MonoBehaviour
{
    [SerializeField] ScriptableEventFloat onHitlagEvent;

    bool waiting;

    private void Stop(float _duration)
    {   if (waiting)
        { return; }
        Time.timeScale = 0.0f;
        StartCoroutine(Wait(_duration));
    }

    IEnumerator Wait(float _duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(_duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }

    private void OnEnable()
    {
        onHitlagEvent.Register(Stop);
    }

    private void OnDisable()
    {
        onHitlagEvent.Unregister(Stop);
    }
}
