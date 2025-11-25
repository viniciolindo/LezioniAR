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

    private InputAction touchPositionAction;

    void Start()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        InputSystem.actions.FindAction("AR/Tap").performed += TapPerformed;
        touchPositionAction = InputSystem.actions.FindAction("AR/TapPosition");
    }

    void TapPerformed(InputAction.CallbackContext context)
    {
       // 1. Leggiamo la posizione del tocco dall'altra azione (touchPositionAction)
        Vector2 screenPosition = touchPositionAction.ReadValue<Vector2>();

        // 2. Eseguiamo il Raycast AR
        if (raycastManager.Raycast(screenPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // 3. Istanziamo l'oggetto nel punto colpito
            Pose hitPose = hits[0].pose;
            Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
        }
    }
}