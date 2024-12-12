using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSurroundings : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    
    private int forestCounter = 0;
    private int desertCounter = 0;
    private int rockCounter = 0;
    private int waterCounter = 0;
    private int mountainCounter = 0;

    private const int FORESTMIN = 3;
    private const int FORESTMAX = 6;
    private const int DESERTMIN = 3;
    private const int DESERTMAX = 6;

    private const int ROCKMIN = 3;
    private const int WATERMIN = 1;
    private const int MOUNTAINMIN = 1;


    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Rock":
                rockCounter++;
                break;
            case "Tree":
                forestCounter++;
                break;
            case "Bush":
                forestCounter++;
                break;
            case "Water":
                waterCounter++;
                break;
            case "Weed":
                desertCounter++;
                break;
            case "Log":
                desertCounter++;
                break;
            case "Mountain":
                mountainCounter++;
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Rock":
                rockCounter--;
                break;
            case "Tree":
                forestCounter--;
                break;
            case "Bush":
                forestCounter--;
                break;
            case "Water":
                waterCounter--;
                break;
            case "Weed":
                desertCounter--;
                break;
            case "Log":
                desertCounter--;
                break;
            case "Mountain":
                mountainCounter--;
                break;
        }
    }

    private void Update()
    {
        string a = outputDescription();
        //Debug.Log(a);

        if (Input.GetKeyUp(KeyCode.E))
        {
            audioSource.clip = (AudioClip)Resources.Load("Music");
            audioSource.Play();
        }
    }

    private string outputDescription()
    {
        string total = "";
        if(forestCounter >= FORESTMAX)
        {
            total = total + " " + "Deep Forest";
        }
        else if(forestCounter >= FORESTMIN) 
        {
            total = total + " " + "Forest";
        }

        if (desertCounter >= DESERTMAX)
        {
            total = total + " " + "Hot Desert";
        }
        else if (desertCounter >= DESERTMIN)
        {
            total = total + " " + "Desert";
        }

        if (waterCounter >= WATERMIN)
        {
            total = total + " " + "Water";
        }

        if (rockCounter >= ROCKMIN)
        {
            total = "Rocky" + " " + total;
        }

        if (mountainCounter >= MOUNTAINMIN)
        {
            total = "Windy" + " " + total;
        }

        return total;
    }
}
