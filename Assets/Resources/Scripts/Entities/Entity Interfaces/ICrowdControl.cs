

using UnityEngine;

public interface ICrowdControl
{
    // slowed, stunned, pinned, knockedUp, knockedDown, knockedBack

    void CC_Slowed(float duration, float ammount);

    void CC_Stunned(float duration);

    void CC_Pinned(float duration, Transform pinningObject);

    void CC_KnockedUp(float duration);

    void CC_KnockedDown(float duration);

    void CC_KnockedBack(float duration);
}
