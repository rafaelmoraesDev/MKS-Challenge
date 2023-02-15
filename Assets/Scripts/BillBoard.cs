using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position - Camera.main.transform.forward);
    }
}
