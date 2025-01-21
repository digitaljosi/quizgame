using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolControl : MonoBehaviour
{
    private static float World_Volume;
    private float MyVol;
    public bool ImPlaying;
    public Transform VolSlider;
    public void Start(){
    MyVol = World_Volume;
    }
    // Update is called once per frame
    void Update()
    {
        if(ImPlaying == false){
            World_Volume = VolSlider.GetComponent<Slider>().value;
            MyVol = World_Volume;
        }
        this.GetComponent<AudioSource>().volume = MyVol;
    }
}
