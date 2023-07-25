using UnityEngine;

namespace EventBus.Handlers
{
    public interface IWeaponHitHandler : IGlobalSubscriber
    {
        void HandleHitWeapon(int penetratedObjects, Vector3[] penetrationPositions, RaycastHit[] hitScan,
            Quaternion hitRotation);
    }
}