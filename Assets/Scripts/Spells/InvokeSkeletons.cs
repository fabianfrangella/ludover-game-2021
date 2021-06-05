using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spells
{
    public class InvokeSkeletons : MonoBehaviour
    {
        public Transform skeleton;

        public void Invoke()
        {
            var position = transform.position;

            var skeletons =
                new List<Transform>
                {
                    Instantiate(skeleton, new Vector2(position.x + 1, position.y + 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1, position.y - 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1, position.y - 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1, position.y + 1), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1.5f, position.y + 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1.5f, position.y - 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x + 1.5f, position.y - 1.5f), Quaternion.identity),
                    Instantiate(skeleton, new Vector2(position.x - 1.5f, position.y + 1.5f), Quaternion.identity)
                };
            foreach (var s in skeletons)
            {
                s.parent = transform;
            }
        }
    }
}