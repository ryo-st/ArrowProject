using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {

    [SerializeField] AudioSource AbsorptionAudio, BreakAudio;

    void Start () {
        AbsorptionAudio.clip = Resources.Load<AudioClip>("Absorption_SE");
        BreakAudio.clip = Resources.Load<AudioClip>("Break_SE");
    }
	
    public void PlayAbsorption()
    {
        AbsorptionAudio.Play();
    }
    public void PlayBreak()
    {
        BreakAudio.Play();
    }
}
