using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MPTK;
using MidiPlayerTK;

public class MovingNote : MonoBehaviour
{
    public ParticleSystem CollisionParticleSystem;
    public ParticleSystem TrailParticleSystem;

    public ParticleSystem ModelParticleSystem;
    // public float xSpeed;
    // public float ySpeed;
    // public float zSpeed;
    public MidiStreamPlayer streamPlayer;
    public MPTKEvent note;
    public bool autoPlay = false;

    public GameObject Sphere;
    
    public string Button = "";
    Vector3 speed = new Vector3(0,0,0);
    bool pressable = false;
    bool missed = false;
    


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(QueueParticleTrail());
        TrailParticleSystem.Play();
        Sphere.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        transform.position += speed*Time.deltaTime;
        if(!autoPlay && Input.GetButton(Button) && !pressable){
            missed = true;
            Debug.LogError("Missed true");
        }
        else if (!autoPlay && !Input.GetButton(Button)){
            missed = false;
            Debug.LogError("Missed False");
        }
    }

    public void SetSpeed(Vector3 spd){
        speed = spd;
    }

    // private IEnumerator QueueParticleTrail(){
    //     yield return new WaitForSeconds(0.2f);
    // }

    private IEnumerator DestoryAfterParticle(float lifetime){
        yield return new WaitForSeconds(lifetime);
        Debug.Log($"In Coroutine: {Time.time}");
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider other) {
        //xSpeed = 0;
        //ySpeed = 0;
        //zSpeed = 0;
        TrailParticleSystem.Stop();
        if(!autoPlay){
            pressable = true; 
            StartCoroutine(DestoryAfterParticle(CollisionParticleSystem.main.startLifetimeMultiplier-0.02f));
            Debug.Log($"Coroutine started: {Time.time}");
        }
        else{
            streamPlayer.MPTK_PlayEvent(note);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other) {   
        if(Input.GetButton(Button) && !autoPlay && pressable && !missed){
            pressable = false;
            Debug.Log($"I HIT");
            CollisionParticleSystem.Play();
            ModelParticleSystem.Play();
            streamPlayer.MPTK_PlayEvent(note);
            Sphere.SetActive(false);
        }
    } 

    private void OnTriggerExit(Collider other) {
        Debug.Log(other.name);
        //Destroy(gameObject);
    }
}
