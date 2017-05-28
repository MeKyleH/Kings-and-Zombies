using UnityEngine;
using UnityEngine.Networking;

public class WeaponPositionSync : NetworkBehaviour
{
    [SerializeField]
    Transform cameraTransform;
    [SerializeField]
    Transform handMount;
    [SerializeField]
    Transform weaponPivot;
    [SerializeField]
    Transform rightHandHold;
    [SerializeField]
    Transform leftHandHold;
    [SerializeField]
    float threshold = 10f;
    [SerializeField]
    float smoothing = 5f;

    [SyncVar]
    float pitch;
    Vector3 lastOffset;
    float lastSyncedPitch;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        if (isLocalPlayer)
            weaponPivot.parent = cameraTransform;
        else
            lastOffset = handMount.position - transform.position;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            pitch = cameraTransform.localRotation.eulerAngles.x;
            if (Mathf.Abs(lastSyncedPitch - pitch) >= threshold)
            {
                CmdUpdatePitch(pitch);
                lastSyncedPitch = pitch;
            }
        }
        else
        {
            Quaternion newRotation = Quaternion.Euler(pitch, 0f, 0f);

            Vector3 currentOffset = handMount.position - transform.position;
            weaponPivot.localPosition += currentOffset - lastOffset;
            lastOffset = currentOffset;

            weaponPivot.localRotation = Quaternion.Lerp(weaponPivot.localRotation,
                newRotation, Time.deltaTime * smoothing);
        }
    }

    [Command]
    void CmdUpdatePitch(float newPitch)
    {
        pitch = newPitch;
    }

    void OnAnimatorIK()
    {
        if (!anim)
            return;

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandHold.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandHold.rotation);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandHold.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandHold.rotation);
    }
}