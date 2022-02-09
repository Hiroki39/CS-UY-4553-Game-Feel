using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    float xPos = 1;
    float yRot = -90;
    float speed = 50;
    Vector3 origin = new Vector3(0,0,1);
    public bool doSquish;
    public bool doTrail;
    public bool doShake;
    public bool doParticle;
    public bool doSound;
    public bool left = true;
    TrailRenderer trail;
    Animator animator;
    Rigidbody2D rb;
    CameraShake camShake;
    ParticleSystem rightParticles;
    ParticleSystem leftParticles;
    void Awake()
    {
        //Grab relevant components in this object & in camera
        trail = gameObject.GetComponent<TrailRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rightParticles = GameObject.Find("Right Particles").GetComponent<ParticleSystem>();
        leftParticles = GameObject.Find("Left Particles").GetComponent<ParticleSystem>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Update is called once per frame
    void Update()
    {
        //Effect toggles
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            doSquish = !doSquish;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            doTrail = !doTrail;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            doShake = !doShake;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            doParticle = !doParticle;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)){
            doSound = !doSound;
        }


        //Animation reset 
        if (Vector3.Distance(transform.position, origin) < 1.0f)
            animator.SetBool("doSquish", false);

        if (doTrail)
        {
            trail.enabled = true;
        }
        else {
            trail.enabled = false;
        }

        if (!doParticle)
        {
            rightParticles.Stop();
            leftParticles.Stop();
        }
        //Movement
        rb.AddForce(Vector3.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(left);
        Debug.Log(doParticle);
        if (doSquish) {
            animator.SetBool("doSquish", true);
        }
        if (doShake) {
            camShake.setDuration(0.15f);
        }
        if (left)
        {
            if (doParticle)
            {
                leftParticles.Play();
            }
            left = false;
        } else
        {
            if (doParticle)
            {
                rightParticles.Play();
            }
            left = true;
        }
        Debug.Log(left);
        Debug.Log(doParticle);
        speed *= -1;
        //xPos *= -1;
        //yRot *= -1;
        //particles.transform.position = new Vector3(xPos, 0, 0);
        //particles.transform.rotation = new Quaternion(0, yRot, 0,0);


    }
}
