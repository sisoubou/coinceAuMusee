using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Références")]
    public Camera mainCamera;
    public TextMeshProUGUI interactionText; 
    public GameObject miniGameCanvas;
    public FirstPersonController fpsController;
    
    [Header("Paramètres")]
    public float interactionDistance = 3f;

    private bool isPlayingMiniGame = false;

    void Update()
    {
        if (isPlayingMiniGame) return;

        if (interactionText != null) interactionText.gameObject.SetActive(false);

        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                if (interactionText != null) interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    OuvrirMiniJeu();
                }
            }
        }
    }

    public void OuvrirMiniJeu()
    {
        isPlayingMiniGame = true;
        interactionText.gameObject.SetActive(false);
        miniGameCanvas.SetActive(true);

        fpsController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void FermerMiniJeu()
    {
        isPlayingMiniGame = false;
        miniGameCanvas.SetActive(false);

        fpsController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}