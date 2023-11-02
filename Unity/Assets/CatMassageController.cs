using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMassageController : MonoBehaviour
{
    public ArduinoControl arduinoControl;
    public CatBehaviour catBehaviour;

    public float min = 1f;
    public float max = 5f;

    public List<float> massageWaiter;
    public List<float> massageNeed;
    public List<GameObject> massagePoints;
    public int loveCounter = 0;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        massageNeed = new List<float>() { 0f, 0f, 0f, 0f };
        massageWaiter = new List<float>() { 2f, 4f, 2f, 1f };
        for(int i =0; i < massageNeed.Count; i++)
        {
            massageNeed[i] = Random.Range(min, max);
            massagePoints[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        for(int i = 0; i < massageNeed.Count; i++)
        {
            float touchNormalised = (arduinoControl.touchValues[i] - 30) / 200f;
            Color newColor = Color.Lerp(Color.red, Color.green, touchNormalised);
            massagePoints[i].GetComponent<MeshRenderer>().material.SetColor("_Color", newColor);
            if (newColor.g >= 0.8)
            {
                loveCounter++;
            }
            if (massageWaiter[i] <= 0f)
            {
                massagePoints[i].SetActive(true);
                massageNeed[i] -= Time.deltaTime;
                if(loveCounter > 20 && newColor.g >= 0.9)
                {
                    audioSource.Play();
                }
                if (massageNeed[i] <= 0f)
                {
                    massageWaiter[i] = Random.Range(min, max);
                    massageNeed[i] = Random.Range(min, max);
                    massagePoints[i].SetActive(false);
                }
            }
            else
            {
                massageWaiter[i] -= Time.deltaTime;
            }
           
        }
    }
}
