using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDistance : MonoBehaviour
{
    AudioSource radar;
    public AudioClip safeRadar;
    public AudioClip dangerRadar;
    
    void Start()
    {
        //GetComponent<AudioSource>().Play(safeRadar);
        //GetComponent<AudioSource>().Stop(dangerRadar);
    }

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            //AudioSource.PlayClipAtPoint(dangerRadar);
            //AudioSource.Stop(safeRadar);
        }
        else
        {
            //GetComponent<AudioSource>().Play(safeRadar);
            //GetComponent<AudioSource>().Stop(dangerRadar);
        }
    }
}
