using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    [SerializeField] GameObject blockHitParticle;

    [SerializeField] GameObject roofHitParticle;

    [SerializeField] GameObject fixParticle;

    public void SpawnBlockHitParticle(Vector3 location)
    {
        Instantiate(blockHitParticle, location, Quaternion.identity);
    }

    public void SpawnRoofHitParticle(Vector3 location)
    {
        Instantiate(roofHitParticle, location, Quaternion.identity);
    }

    public void SpawnFixParticle(Vector3 location)
    {
        Instantiate(fixParticle, location, Quaternion.identity);
    }
}
