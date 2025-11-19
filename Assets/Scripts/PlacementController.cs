using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using UnityEngine.InputSystem; // <-- 1. IMPORTANTE: Aggiungi questo

public class PlacementController : MonoBehaviour
{
    public GameObject objectToPlace;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        Vector2 screenPosition = Vector2.zero;
        bool inputFound = false;

        // --- Inizio Logica Nuovo Input System ---

        // 2. Controlla il Tocco (per il dispositivo)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            inputFound = true;
        }
        // 3. Controlla il Mouse (per la simulazione in Editor)
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            screenPosition = Mouse.current.position.ReadValue();
            inputFound = true;
        }

        // --- Fine Logica Nuovo Input System ---

        // Se abbiamo trovato un input valido, lancia il raggio
        if (inputFound)
        {
            if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
            {
                // Colpito! Instanzia l'oggetto.
                Pose hitPose = hits[0].pose;
                Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
            }
        }
    }
}