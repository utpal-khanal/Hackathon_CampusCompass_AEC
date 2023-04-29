using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FaceCamera : MonoBehaviour
{
    Transform mainCameraTransform;
    public TextMeshProUGUI roomPlateText;



    private void Start()
    {
        mainCameraTransform = Camera.main.transform;

    }

   /* private void LateUpdate()
    {
        transform.LookAt(
            transform.position + mainCameraTransform.rotation * Vector3.forward,
            mainCameraTransform.rotation * Vector3.up);
    }*/
}
