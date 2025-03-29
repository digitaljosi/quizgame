using System;
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

    [SerializeField]
    private BoolValue dadosSalvos;
    bool bDadosSalvos = false;
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


    public bool VerificaSeDadosEstaoSalvos(){
        return dadosSalvos.RuntimeValue;
    }

    public void SetDadosSalvos(){        
        dadosSalvos.RuntimeValue = true;
    }

    public int GetBoloesEncontrados(){
        return Baloes;
    }
}