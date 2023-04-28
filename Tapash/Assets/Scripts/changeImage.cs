using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImage : MonoBehaviour
{
    public Image original;
    public Sprite newImage;
    public Sprite anotherImage;
    int i = 1;



    public void NewImage()
    {
        i++;
        if (i%2==0)
        {
            original.sprite = newImage;
        }
        else
        {
            original.sprite = anotherImage;

        }
            
        
    }
   
}
