using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleManager : MonoBehaviour
{
    public List<GameObject> bloodParticleList;
    public List<GameObject> smokeParticleList;

    public void bloodParticle(Vector3 pos)
    {
        foreach (var item in bloodParticleList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = pos;

                item.GetComponent<ParticleSystem>().Play();
                break;
            }
        }


    }
    public void smokeParticle(Vector3 pos)
    {
        foreach (var item in smokeParticleList)
        {
            if (!item.activeInHierarchy)
            {
                item.SetActive(true);
                item.transform.position = pos;

                item.GetComponent<ParticleSystem>().Play();
                break;
            }
        }


    }
}
