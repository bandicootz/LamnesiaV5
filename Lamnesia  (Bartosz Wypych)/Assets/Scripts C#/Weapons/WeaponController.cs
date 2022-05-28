using UnityEngine;
using static Models;

public class WeaponController : MonoBehaviour
{
    private MouseLook mouseLook;

    [Header("Settings")]
    public WeaponSettingsModel settings;

    bool isInitialised;
    Vector3 newWeaponRotation;

    private void Start()
    {
        newWeaponRotation = transform.localRotation.eulerAngles;
    }

    public void Initialise(MouseLook MouseLook)
    {
        mouseLook = MouseLook;
        isInitialised = true;
    }

    private void Update()
    {
        if (!isInitialised)
        {
            return;
        }

        newWeaponRotation mouseX = Input.GetAxis("Mouse X") * settings.SwayAmount * Time.deltaTime;
        newWeaponRotation mouseY = Input.GetAxis("Mouse Y") * newWeaponRotation * Time.deltaTime;

        //newWeaponRotation -= mouseY;
        //newWeaponRotation = Mathf.Clamp(settings.SwayAmount, -90f, 90f);

        transform.localRotation = Quaternion.Euler(newWeaponRotation);
    }
}