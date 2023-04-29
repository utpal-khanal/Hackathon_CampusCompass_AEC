using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oneTimeChange : MonoBehaviour
{
    public Image original;
    public Sprite newImage;

   
    public void updateImage()
    {
        original.sprite = newImage;
    }
}
