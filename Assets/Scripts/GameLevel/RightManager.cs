using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightManager : MonoBehaviour
{
    [SerializeField]
    private GameObject right1, right2, right3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //kalan hakları kontrol eden fonksiyon
    public void controlRights(int rightNumber)
    {
        switch(rightNumber)
        {
            case 3:
                right1.SetActive(true);
                right2.SetActive(true);
                right3.SetActive(true);
                break;

            case 2:
                right1.SetActive(true);
                right2.SetActive(true);
                right3.SetActive(false);
                break;       

            case 1:
                right1.SetActive(true);
                right2.SetActive(false);
                right3.SetActive(false);
                break;

            case 0:
                right1.SetActive(false);
                right2.SetActive(false);
                right3.SetActive(false);
                break;    
        }
    }
}
