using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    public Canvas canvas4;
    public Canvas canvas5;
    public Canvas canvas6;
    TrailRenderer trail;
    Animator animator;
    Rigidbody2D rb;
    CameraShake camShake;
    ParticleSystem particles;
    AudioSource _audioSource;
    public AudioClip hitSnd1;
    public AudioClip hitSnd2;

    Color32[] colorArray = { new Color32(159, 154, 154, 255), new Color32(233, 154, 53, 255) };

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
            Debug.Log(colorArray[doSquish ? 1 : 0]);
            canvas1.GetComponent<Image>().color = colorArray[doSquish ? 1 : 0];
            Debug.Log(canvas1.GetComponent<Image>().color);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
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
            canvas2.GetComponent<Image>().color = colorArray[doTrail ? 1 : 0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            doShake = !doShake;
            canvas3.GetComponent<Image>().color = colorArray[doShake ? 1 : 0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            doParticle = !doParticle;
            canvas4.GetComponent<Image>().color = colorArray[doParticle ? 1 : 0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            doSound = !doSound;
            if (doSound)
            {
                if (doTrail)
                {
                    _audioSource.Play();
                }
            }
            else
            {
                _audioSource.Stop();
            }
            canvas5.GetComponent<Image>().color = colorArray[doSound ? 1 : 0];
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1 - Time.timeScale;
            canvas6.GetComponent<Image>().color = colorArray[1 - (int)Time.timeScale];
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

        if (transform.position.y < -7)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
            if (doSound)
            {
                _audioSource.PlayOneShot(hitSnd2, 0.5f);
            }
        }
        else
        {
            if (doSound)
            {
                _audioSource.PlayOneShot(hitSnd1, 0.5f);
            }
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
