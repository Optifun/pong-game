using System;
using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionDetector : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
        public event Action<Collider> TriggerExit;
        public event Action<Collider> TriggerStay;
        
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;
        public event Action<Collision> CollisionStay;

        private void OnTriggerEnter(Collider obj) =>
            TriggerEnter?.Invoke(obj);

        private void OnTriggerExit(Collider obj) =>
            TriggerExit?.Invoke(obj);

        private void OnTriggerStay(Collider obj) =>
            TriggerStay?.Invoke(obj);

        private void OnCollisionEnter(Collision obj) =>
            CollisionEnter?.Invoke(obj);

        private void OnCollisionExit(Collision obj) =>
            CollisionExit?.Invoke(obj);

        private void OnCollisionStay(Collision obj) =>
            CollisionStay?.Invoke(obj);
    }
}