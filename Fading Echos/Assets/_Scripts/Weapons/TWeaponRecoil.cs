using UnityEngine;

public class TWeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilFollowPos;
    [SerializeField] float kickBackAmount = -1;
    [SerializeField] float kickBackSpeed = 10;
    [SerializeField] float returnSpeed = 20;
    float currentRecoilPos;
    float finalRecoilPos;

    void Update()
    {
        currentRecoilPos = Mathf.Lerp(currentRecoilPos, 0, returnSpeed * Time.deltaTime);
        finalRecoilPos = Mathf.Lerp(finalRecoilPos, currentRecoilPos, kickBackSpeed * Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilPos);
    }

    public void TriggerRecoil()
    {
        currentRecoilPos += kickBackAmount;
    }
}
