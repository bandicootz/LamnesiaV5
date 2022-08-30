using System.Collections.Generic;
using Lamnesia.InGame.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Lamnesia.InGame
{
    public class Taker : MonoBehaviour
    {
        private List<Key> m_Keys = new List<Key>();
        private Key m_Key;
        private Camera cam;
        private int keysQuantiny = 0;

        [Header("MUSIC PACK FOR EVERY SITATION")]
        public AudioClip startMusic;
        public AudioClip seekMusic;

        [Header("TUTORIAL MESSAGES HERE")]
        public string keyTipMessage = "Press [E] to collect a key";
        public string openDoorMessage = "Press [E] to open a door";
        public string errorOpenMessage = "You have no keys bro, door is locked";

        [Header("REFERENCES TO OBJECTS")]
        [SerializeField] private TextMeshProUGUI tipUI;
        [SerializeField] private GameObject keyUI;
        [SerializeField] private GameObject loseScreen;
        private float range = 5f;

        void Update()
        {
            ShootRaycast();
        }

        void Start()
        {
            cam = GetComponentInChildren<Camera>();
            loseScreen.SetActive(false);

            //Start music
            AudioManager.Instance.PlayMusic(startMusic);
            AudioManager.Instance.musicSource.loop = true;
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
                            var key = hit.transform.gameObject.GetComponent<Key>();
                            m_Keys.Add(key);
                            keysQuantiny++;
                            keyUI.SetActive(true);
                            if (keysQuantiny > 1)
                                keyUI.GetComponentInChildren<TextMeshProUGUI>().text = "x" + keysQuantiny;


                            key.myParticle.gameObject.SetActive(false);
                            key.gameObject.SetActive(false);

                            AudioManager.Instance.PlaySound(key.interactSound);
                            tipUI.text = " ";
                        }

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
                                            //Switching keys visual showing depends on quantity of actual keys
                                            keysQuantiny--;
                                            if (keysQuantiny > 1)
                                                keyUI.GetComponentInChildren<TextMeshProUGUI>().text = "x" + keysQuantiny;
                                            else if (keysQuantiny == 1)
                                                keyUI.GetComponentInChildren<TextMeshProUGUI>().text = "";
                                            else
                                                keyUI.SetActive(false);
                                            
                                            AudioManager.Instance.PlaySound(door.GetComponent<Door>().interactSound);
                                            //door.SetActive(false);
                                            
                                            tipUI.text = " ";
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

                    default:

                        tipUI.text = "";
                        break;

                }
            }
        }
    }
}