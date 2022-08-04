using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetTracking : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private GameObject obj;

    public void OnChanged(ARTrackedSignal signal)
    {
        ARTrackedImagesChangedEventArgs eventArgs = signal.trackedChangedEventArgs;
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            Destroy(obj);
            obj = null;
        }
    }

    private void UpdateImage(Component trackedImage)
    {
        Vector3 position = trackedImage.transform.position;
        if (obj == null)
        {
            obj = Instantiate(prefab, position, Quaternion.identity);
        }
        else
        {
            obj.transform.position = position;
        }
    }
}