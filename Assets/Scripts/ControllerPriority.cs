using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class ControllerPriority : MonoBehaviour
{
    private bool usedMouseLast = false;
    private bool mouseWasHidden = false; // Indique si la souris �tait cach�e
    public GameObject firstButton;

    [SerializeField] private PanelType type; // Type de panel actuel

    private void Update()
    {
        DetectInputMethod();
    }

    private void DetectInputMethod()
    {
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            usedMouseLast = true;
            if (mouseWasHidden)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                mouseWasHidden = false;
            }
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ||
            Input.GetButtonDown("Submit") || Input.GetButtonDown("Cancel"))
        {
            if (usedMouseLast)
            {
                usedMouseLast = false;
                HideMouseAndSelectButton();
            }
        }
    }

    private void HideMouseAndSelectButton()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseWasHidden = true;

        // V�rifie si un bouton est d�j� s�lectionn�
        if (EventSystem.current.currentSelectedGameObject == null && firstButton != null && type != PanelType.Main)
        {
            EventSystem.current.SetSelectedGameObject(firstButton);
        }
    }
}
