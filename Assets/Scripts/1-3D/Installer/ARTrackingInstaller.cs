using UnityEngine;
using Zenject;

public class ARTrackingInstaller : MonoInstaller
{
    [SerializeField] private TargetTracking target;

    public override void InstallBindings()
    {
        Container.Bind<TargetTracking>().FromInstance(target).AsSingle();

        Container.BindSignal<ARTrackedSignal>().ToMethod<TargetTracking>(x => x.OnChanged).FromResolve();
    }
}