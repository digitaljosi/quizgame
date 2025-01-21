using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class B_Counter : MonoBehaviour
{
     [SerializeField] Text BallonsText;
    public bool Im_counter;
    public static int Baloes;
    // Start is called before the first frame update
    public void Start()
    {
        if(Im_counter == false){
        Baloes ++;
        }  else {
            Baloes = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Im_counter==true){
            BallonsText.text = "Balões encontrados: ("+Baloes+"/12)";
        }
    }
}