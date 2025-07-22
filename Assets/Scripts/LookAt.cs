using System;
using UnityEngine;

namespace Zadaca2
{
    public class LookAt : MonoBehaviour
    {
        private Camera playerCamera;

        private void Start()
        {
            playerCamera = Camera.main;
        }

        private void Update()
        {
            if (!playerCamera) return; 
            
            transform.LookAt(playerCamera.transform);
        }
    }
}