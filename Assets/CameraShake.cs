using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 startpos;
    float duration;
    void Awake()
    {
        startpos = transform.position;
        duration = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0)
        {
            transform.position = startpos + Random.insideUnitSphere * 0.35f;
            duration -= Time.deltaTime;
        }
        else {
            duration = 0;
            transform.position = startpos;
        }
    }

    public void setDuration(float dur) {
        duration = dur;
    }
}
