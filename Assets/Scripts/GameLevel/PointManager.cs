using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    private int totalPoint;
    private int point;

    [SerializeField]
    private Text pointText;

    void Start()
    {
        pointText.text = totalPoint.ToString();
    }

    //puanı arttıran fonksiyon
    public void increasePoint(string level)
    {
        switch(level)
        {
            case "Kolay":
                point = 5;
                break;
            case "Orta":
                point = 10;
                break;
            case "Zor":
                point = 15;
                break;    
        }

        totalPoint += point;
        pointText.text = totalPoint.ToString();
    }
}
