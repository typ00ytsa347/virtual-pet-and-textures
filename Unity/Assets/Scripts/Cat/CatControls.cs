using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CatControls : MonoBehaviour
{
    public CatMassageController catMassageController;
    public ArduinoControl arduino;
    public CatBehaviour catBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < catMassageController.massageWaiter.Count; i++)
        {
            if (catMassageController.massageWaiter[i] <= 0f)
            {
                if (arduino.touchValues[i] - 30f > 0f)
                {
                    catBehaviour.AddAffection((arduino.touchValues[i] - 30f) / 120f);

                }
            }
        }
     
   

        if(Input.GetMouseButtonDown(0))
        {
            catBehaviour.GoToOwner();
        }

        if (Input.GetMouseButtonDown(1))
        {
            catBehaviour.Bounce();
        }
    }
}
