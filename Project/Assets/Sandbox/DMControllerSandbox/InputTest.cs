using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    public DMGeneralPrototype.Paddle[] players;

    public GameObject controllerPrefab;

    public GameObject player1;
    public GameObject player2;

    List<InputDevice> devices = new List<InputDevice>();

    private void Start()
    {
        InputSystem.onDeviceChange +=
        (device, change) =>
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    Debug.Log("New device added: " + device + " : " + device.deviceId);
                    break;
                case InputDeviceChange.Reconnected:
                    Debug.Log("Reconnected device: " + device + " : " + device.deviceId);
                    break;
                case InputDeviceChange.Removed:
                    Debug.Log("Device removed: " + device + " : " + device.deviceId);
                    break;
            }
        };
    }

    private void Update()
    {
        if (devices.Count < 2)
        {
            foreach (InputDevice device in InputSystem.devices)
            {
                if (device.IsActuated(0.1f) && !devices.Contains(device) && device is Gamepad)
                {
                    devices.Add(device);
                    Debug.Log("Added new controller: " + device.deviceId);
                }
            }
        }
        else
        {
            foreach (InputDevice disconnectedDevice in InputSystem.disconnectedDevices)
                if (devices.Contains(disconnectedDevice)) return;

            Gamepad gamepad1 = devices[0] as Gamepad;
            Gamepad gamepad2 = devices[1] as Gamepad;

            player1.transform.position += (Vector3)gamepad1.leftStick.ReadValue() * Time.deltaTime * 10;
            player2.transform.position += (Vector3)gamepad2.leftStick.ReadValue() * Time.deltaTime * 10;
        }
    }
}
