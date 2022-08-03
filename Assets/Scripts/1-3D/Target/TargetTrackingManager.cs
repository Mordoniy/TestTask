using UnityEngine.XR.ARFoundation;
using Zenject;

public class TargetTrackingManager : IInitializable
{
    private SignalBus signalBus;
    private ARTrackedImageManager trackedManager;

    public TargetTrackingManager(SignalBus signalBus, ARTrackedImageManager trackedManager)
    {
        this.signalBus = signalBus;
        this.trackedManager = trackedManager;
        this.trackedManager.trackedImagesChanged += FireEvent;
    }

    ~TargetTrackingManager()
    {
        trackedManager.trackedImagesChanged -= FireEvent;
    }

    public void Initialize()
    {
    }

    private void FireEvent(ARTrackedImagesChangedEventArgs args)
    {
        signalBus.Fire(new ARTrackedSignal() {trackedChangedEventArgs = args});
    }
}