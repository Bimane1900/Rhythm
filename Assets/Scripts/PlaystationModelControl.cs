using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.ParticleSystemModule;

public class PlaystationModelControl : MonoBehaviour
{
    [Header("Triangle")]
    public Light TriangleLight;
    public MeshRenderer TriangleRenderer;
    public ParticleSystem TriangleParticles;
    Material TriangleMat;


    [Header("Square")]
    public Light SquareLight;
    public MeshRenderer SquareRenderer;
    public ParticleSystem SquareParticles;
    Material SquareMat;

    [Header("Cross")]
    public Light CrossLight;
    public MeshRenderer CrossRenderer;
    public ParticleSystem CrossParticles;
    Material CrossMat;

    [Header("Circle")]
    public Light CircleLight;
    public MeshRenderer CircleRenderer;
    public ParticleSystem CircleParticles;
    Material CircleMat;

    // Start is called before the first frame update
    void Start()
    {
        CircleMat = CircleRenderer.material;
        TriangleMat = TriangleRenderer.material;
        SquareMat = SquareRenderer.material;
        CrossMat = CrossRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        //Triangle inputs management
        if(Input.GetButton("Triangle")){
            TriangleMat.EnableKeyword("_EMISSION");
            TriangleLight.intensity = 3;
        }
        else{
            TriangleMat.DisableKeyword("_EMISSION");
            TriangleLight.intensity = 0;
        }
        if(Input.GetButtonDown("Triangle"))
            TriangleParticles.Play();

        //Circle inputs management
        if(Input.GetButton("Circle")){
            CircleMat.EnableKeyword("_EMISSION");
            CircleLight.intensity = 3;
        }
        else{
            CircleMat.DisableKeyword("_EMISSION");
            CircleLight.intensity = 0;
        }
        if(Input.GetButtonDown("Circle"))
            CircleParticles.Play();
        
        //Square inputs management
        if(Input.GetButton("Square")){
            SquareMat.EnableKeyword("_EMISSION");
            SquareLight.intensity = 3;
        }
        else{
            SquareMat.DisableKeyword("_EMISSION");
            SquareLight.intensity = 0;
        }
        if(Input.GetButtonDown("Square"))
            SquareParticles.Play();
        
        //Cross inputs management
        if(Input.GetButton("Cross")){
            CrossMat.EnableKeyword("_EMISSION");
            CrossLight.intensity = 3;
        }
        else{
            CrossMat.DisableKeyword("_EMISSION");
            CrossLight.intensity = 0;
        }
        if(Input.GetButtonDown("Cross"))
            CrossParticles.Play();
    }
}
