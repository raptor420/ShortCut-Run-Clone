using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour
{
    [SerializeField] GameObject waterparticle;
    // Start is called before the first frame update
    public void PlayWaterParticle()
    {
        if(waterparticle)
        Instantiate(waterparticle, transform.position,Quaternion.identity);

    }
}
