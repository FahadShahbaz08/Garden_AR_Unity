using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FlowerGenertor : MonoBehaviour
{
    public GameObject[] flowers;

    public XROrigin SessionOrigin;
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHit = new List<ARRaycastHit>();

    private void Update()
    {
        // Check if there's a touch input
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Get the touch position
            Vector2 touchPosition = Input.GetTouch(0).position;

            // Raycast from the touch position
            bool hit = raycastManager.Raycast(touchPosition, raycastHit, TrackableType.PlaneWithinPolygon);

            if (hit)
            {
                // Instantiate a random flower from the array
                GameObject _object = Instantiate(flowers[Random.Range(0, flowers.Length)]);
                _object.transform.position = raycastHit[0].pose.position;
            }

            // Deactivate all planes and disable the plane manager
            foreach (var plane in planeManager.trackables)
            {
                plane.gameObject.SetActive(false);
            }

            planeManager.enabled = false;
        }
    }
}
