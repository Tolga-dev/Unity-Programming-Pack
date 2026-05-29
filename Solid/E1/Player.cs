using System;
using UnityEngine;

namespace Solid.E1
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerAudio playerAudio;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerMovement playerMovement;

        private void Start()
        {
            playerAudio = GetComponent<PlayerAudio>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        private void Update()
        {
            if(playerInput.GetInput() != Vector3.zero)
                playerMovement.SetInput(playerInput.GetInput());
            else if(playerInput.IsJump())
                playerMovement.SetJump();

            
        }
    }
}