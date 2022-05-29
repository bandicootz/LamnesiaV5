using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lamnesia.InGame
{
    public class Taker : MonoBehaviour
    {
        private List<Key> m_Keys;
        private Key m_Key;
        private Camera cam;
        
        [Header("TUTORIAL MESSAGES HERE")]
        public string keyTipMessage = "Press [E] to collect a key";
        public string openDoorMessage = "Press [E] to open a door";
        public string errorOpenMessage = "You have no keys bro, door is locked";

        [Header("REFERENCES TO OBJECTS")]
        [SerializeField] private TextMeshProUGUI tipUI;
        [SerializeField] private GameObject keyUI;

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

                        tipUI.color = Color.green;
                        tipUI.text = keyTipMessage;

                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            m_Keys.Add(hit.transform.gameObject.GetComponent<Key>());

                            hit.transform.gameObject.SetActive(false);

                            tipUI.text = "";
                            keyUI.SetActive(true);
                        } // "deleting" keys after interaction

                        break;

                    case "Door":
                        
                        tipUI.color = Color.yellow;
                        tipUI.text = openDoorMessage;

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

                                            //turn off UI elements
                                            keyUI.SetActive(false);
                                            tipUI.text = "";
                                            //m_Keys.Remove(m_Key);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                tipUI.color = Color.red;
                                tipUI.text = errorOpenMessage;
                            }
                        }

                        break;
                }
            }
        }
    }
}