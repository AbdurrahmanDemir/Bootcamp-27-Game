using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class waypoint : MonoBehaviour
{
    public Transform[] wayPointPos;
    Transform activePos;
    Vector3 pos;
    int nextPos=0;

    private void Start()
    {
        activePos = wayPointPos[0].transform;
    }
    private void Update()
    {
        pos = activePos.position - transform.position;
        transform.Translate(pos.normalized * 2f * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, activePos.position) <= .1f)
        {
            nextWayPos();
        }
    }

    void nextWayPos()
    {
        if (nextPos >= wayPointPos.Length - 1)
        {
            Debug.Log("bitti");
            return;
        }
        nextPos++;
        activePos = wayPointPos[nextPos].transform;
    }


}
