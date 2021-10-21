using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifHandler : MonoBehaviour
{
    [SerializeField] private Text _collisionText;
    [SerializeField] private Text _triggerText;

    private void OnCollisionEnter(Collision collision)
    {
        _collisionText.color = Color.green;
    }

    private void OnCollisionExit(Collision collision)
    {
        _collisionText.color = Color.black;
    }

    private void OnTriggerEnter(Collider other)
    {
        _triggerText.color = Color.green;
    }

    private void OnTriggerExit(Collider other)
    {
        _triggerText.color = Color.black;
    }
}
