using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour
{
    public GameObject[] placedPrefabs;
    public void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            // Handle added event
            if ( newImage.referenceImage.name == "DefaultMaterial_AO" )
            {
                // Instantiate or activate a prefab associated with this image
                // Example: Instantiate(placedPrefabs[0], newImage.transform.position, newImage.transform.rotation);
                Instantiate(placedPrefabs[0], newImage.transform);
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            // Handle updated event
        }

        foreach (var removed in eventArgs.removed)
        {
            // Handle removed event
            TrackableId removedImageTrackableId = removed.Key;
            ARTrackedImage removedImage = removed.Value;
        }
    }
}
