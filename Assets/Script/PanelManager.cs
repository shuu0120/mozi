using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [SerializeField] GameObject panelexplain1;
    [SerializeField] GameObject panelexplain2;
    [SerializeField] GameObject panelexplain3;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void Onclickexplain1()
    {
        panelexplain1.SetActive(true);
        
    }
    public void Onclickexplain2()
    {
        panelexplain1.SetActive(false);
        panelexplain2.SetActive(true);
    }

    public void Onclickexplain3()
    {
        panelexplain2.SetActive(false);
        panelexplain3.SetActive(true);
    }

    public void back1()
    {
        panelexplain2.SetActive(false);
        panelexplain1.SetActive(false);
    }
    public void back2()
    {
        panelexplain3.SetActive(false);
        panelexplain2.SetActive(true);
    }
    public void back()
    {
        
        panelexplain3.SetActive(false);
        panelexplain1.SetActive(false);
    }


}
