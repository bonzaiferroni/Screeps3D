using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils {
    public class ThirdPersonCamera : MonoBehaviour {

        [SerializeField] private float zoomSpeed = 10;
        [SerializeField] private Transform followTransform;
        private Vector3 posRef;
        private Quaternion camRotation;
        private float zoom;
        private float xRot = -135;
        private float yRot = 0;
        private Vector3 tether;
        private Vector3 tetherRef;
        private Vector3 height;

        // Use this for initialization
        void Start () {
            if (followTransform) {
                Init();
            } else {
                enabled = false;
            }
        }

        private void Init() {
            zoom = Vector3.Distance(transform.position, followTransform.position);
            height = Vector3.up * 1;
            tether = followTransform.position + height;
            enabled = true;
        }

        // Update is called once per frame
        void LateUpdate () {
            var tetherGarget = followTransform.position + height;
            if (Vector3.Distance(tetherGarget, tether) > .1f) {
                tether = Vector3.SmoothDamp(tether, tetherGarget, ref tetherRef, .1f);
            }

            zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButton(1)) {
                xRot -= Input.GetAxis("Mouse Y") * 5;
                yRot += Input.GetAxis("Mouse X") * 5;
            }

            var camRotation = Quaternion.Euler(xRot, yRot, 0);
            var target = tether + camRotation * Vector3.forward * zoom;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref posRef, .2f);
            transform.rotation = Quaternion.LookRotation(tether - transform.position);
        }

        public void FollowTransform(Transform tran) {
            followTransform = tran;
            Init();
        }
    }
}