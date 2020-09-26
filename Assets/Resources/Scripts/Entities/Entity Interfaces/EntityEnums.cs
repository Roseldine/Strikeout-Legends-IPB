
public class EntityEnums
{
    // ================================================= Entity Categories
    public enum entityType { livingEntity, objectEntity }
    public enum livingEntity { playerEntity, npcEntity }
    public enum objectEntity { item, projectile, sfx, vfx }


    // ================================================= CC Status Categories
    public enum ccStatus { slowed, stunned, pinned, knockedUp, knockedDown, knockedBack }


    // ================================================= Movement Categories
    public enum movementType { npcIdle, npcWaypoints, npcRandom, player }
    public enum movementStates { idle, walking, running, jumping, falling }
    public enum movementController { rigidbody, transform, navMeshAgent }
    public enum playerController { mobileJoystick, pcAxis }
}
