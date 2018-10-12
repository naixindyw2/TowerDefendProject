using UnityEngine;
using System.Collections.Generic;

namespace App.Game.Battle.Behaviour
{
    [DisallowMultipleComponent]
    [ExecuteInEditMode]
    public sealed class EntityShadowBehaviour : MonoBehaviour
    {
        public Transform target;
        private Transform circle;
        
        private void Start()
        {
            if (null == circle) {
                this.circle = transform.parent.Find("Selected");
                if (null == this.circle) {
                    return;
                }
            }
        }
        private void Update()
        {
            if (target == null) {
                return;
            }
            // stick on the ground
            transform.position = new Vector3(
                target.position.x,
                0,
                target.position.z);

            // scale for the height
            if (transform.parent != null) {
                float factor = Mathf.Clamp(
                    2.0f - target.position.y,
                    0.3f, 1.0f);

                transform.localScale = new Vector3(
                    factor, factor, factor);
            }

            if (null != this.circle) {
                this.circle.position = transform.position;
            }
        }
    }
}