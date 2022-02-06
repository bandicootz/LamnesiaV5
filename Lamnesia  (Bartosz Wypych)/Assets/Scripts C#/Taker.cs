using System.Collections.Generic;
using UnityEngine;

public class Taker : MonoBehaviour
{
    private List<Key> _keys;
    private Key _key;

    [SerializeField]
    private Camera _Cam;

    private float range = 10f;

    void Update()
    {
       ShootRaycast();
      
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(_Cam.transform.position, _Cam.transform.forward, out hit, range))
        {
            switch (hit.transform.tag)
            {
                case "Key":

                    Debug.Log("I AM KEY, PRESS 'E' TO INTERACT");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _keys.Add(hit.transform.gameObject.GetComponent<Key>());

                        hit.transform.gameObject.SetActive(false);
                    } // "deleting" keys after interaction
                        break;

                case "Door":

                    Debug.Log("I AM DOOR, PRESS 'E' TO INTERACT");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameObject door = hit.transform.gameObject;

                        if (_keys.Count > 0)
                        {
                            for (int i = 0; i < _keys.Capacity; i++)
                            {
                                _key = _keys[i];
                                if (_key != null)
                                {
                                    if (_key.canOpen(door))
                                    {
                                        _keys.Remove(_key);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    else Debug.Log("You have no keys bro, door is locked");

                    break;
            }
        }
    }
}
