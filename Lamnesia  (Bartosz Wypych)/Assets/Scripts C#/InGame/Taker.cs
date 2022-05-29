using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lamnesia.InGame
{
    public class Taker : MonoBehaviour
    {
        private List<Key> m_Keys;
        private Key m_Key;
        private Camera cam;

        private float range = 10f;

        void Update()
        {
            ShootRaycast();
        }

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }
        
        private void ShootRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position,
                    cam.transform.forward, out hit, range))
            {
                switch (hit.transform.tag)
                {
                    case "Key":

                        Debug.Log("I AM KEY, PRESS 'E' TO INTERACT");

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            m_Keys.Add(hit.transform.gameObject.GetComponent<Key>());

                            hit.transform.gameObject.SetActive(false);
                        } // "deleting" keys after interaction

                        break;

                    case "Door":

                        Debug.Log("I AM DOOR, PRESS 'E' TO INTERACT");

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            GameObject door = hit.transform.gameObject;

                            if (m_Keys.Count > 0)
                            {
                                for (int i = 0; i < m_Keys.Capacity; i++)
                                {
                                    m_Key = m_Keys[i];
                                    if (m_Key != null)
                                    {
                                        if (m_Key.CanOpen(door))
                                        {
                                            door.SetActive(false);
                                            //m_Keys.Remove(m_Key);
                                            return;
                                        }
                                    }
                                }
                            }
                            else Debug.Log("You have no keys bro, door is locked");
                        }

                        break;
                }
            }
        }
    }
}