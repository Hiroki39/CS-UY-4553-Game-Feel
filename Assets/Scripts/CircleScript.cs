using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScript : MonoBehaviour
{
    float xSpeed = 50;
    float ySpeed = 40;
    Vector3 origin = new Vector3(0, 0, 1);
    public bool doSquish;
    public bool doTrail;
    public bool doShake;
    public bool doParticle;
    public bool doSound;
    TrailRenderer trail;
    Animator animator;
    Rigidbody2D rb;
    CameraShake camShake;
    public ParticleSystem particles;
    AudioSource _audioSource;
    public AudioClip hitSnd1;
    public AudioClip hitSnd2;

    void Awake()
    {
        //Grab relevant components in this object & in camera
        _audioSource = GetComponent<AudioSource>();
        trail = gameObject.GetComponent<TrailRenderer>();
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        camShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        particles = GameObject.Find("Particle System").GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        //Effect toggles
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            doSquish = !doSquish;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            doTrail = !doTrail;
            if (doTrail)
            {
                _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            doShake = !doShake;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            doParticle = !doParticle;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            doSound = !doSound;
        }


        //Animation reset 
        if (Vector3.Distance(transform.position, origin) < 1.0f)
            animator.SetBool("doSquish", false);

        if (doTrail)
        {
            trail.enabled = true;
        }
        else
        {
            trail.enabled = false;
        }

        if (!doParticle)
        {
            particles.Stop();
        }
        //Movement
        rb.AddForce(Vector3.left * xSpeed * Time.deltaTime);
        rb.AddForce(Vector3.up * ySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (doSquish)
        {
            animator.SetBool("doSquish", true);
        }
        if (doShake)
        {
            camShake.setDuration(0.15f);
            _audioSource.PlayOneShot(hitSnd2, 0.5f);
        }
        else
        {
            _audioSource.PlayOneShot(hitSnd1, 0.5f);
        }
        if (other.gameObject.CompareTag("LeftObstacle") || other.gameObject.CompareTag("RightObstacle"))
        {
            xSpeed *= -1;
        }
        else if (other.gameObject.CompareTag("UpObstacle") || other.gameObject.CompareTag("Player"))
        {
            ySpeed *= -1;
        }

        ContactPoint2D contact = other.GetContact(0);
        Vector3 pos = contact.point;
        particles.transform.position = pos;
        particles.Play();


    }
}
