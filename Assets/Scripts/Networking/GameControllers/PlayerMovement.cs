using Photon.Pun;
using UnityEngine;

namespace Networking.GameControllers {
    public class PlayerMovement : MonoBehaviour {
        [SerializeField] private float movementSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float speedH = 2.0f;
        [SerializeField] private float speedV = 2.0f;
        
        private float _yaw = 0.0f;
        private float _pitch = 0.0f;
        private PhotonView _photonView;
        private CharacterController _controller;

#region Unity methods

        protected void Start() {
            _photonView = GetComponent<PhotonView>();
            _controller = GetComponent<CharacterController>();
        }

        protected void Update() {
            if (!_photonView.IsMine) {
                return;
            }
            
            Move();
            Rotate();
        }

#endregion

        private void Move() {
            if (Input.GetKey(KeyCode.W)) {
                _controller.Move(transform.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.A)) {
                _controller.Move(-transform.right * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.S)) {
                _controller.Move(-transform.forward * Time.deltaTime * movementSpeed);
            }

            if (Input.GetKey(KeyCode.D)) {
                _controller.Move(transform.right * Time.deltaTime * movementSpeed);
            }
        }

        private void Rotate() {
            _yaw += speedH * Input.GetAxis("Mouse X");
            _pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
        }
    }
}