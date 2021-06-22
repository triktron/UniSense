using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed = 10;
    Vector2 _input;

    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        var playerText = GetComponentInChildren<Text>();

        playerText.text = (playerInput.playerIndex + 1).ToString();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        transform.position += new Vector3(_input.x, 0, _input.y) * Speed * Time.deltaTime;
    }
}
