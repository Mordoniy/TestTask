using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Zenject;

public class ARInstaller : MonoInstaller
{
    [SerializeField] private ARTrackedImageManager trackedManager;
    
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<ARTrackedImageManager>().FromInstance(trackedManager).AsCached();
        Container.BindInterfacesTo<TargetTrackingManager>().AsSingle();
        Container.DeclareSignal<ARTrackedSignal>();
    }
}
