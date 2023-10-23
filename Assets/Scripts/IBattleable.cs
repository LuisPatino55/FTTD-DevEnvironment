
public interface IBattleable
{

    public int currentHealth;

    public float currentStamina;

    public float maxPanicPool;                      // panic level will affect attack cooldown, blocking, accuracy, evasion, and raise chance of stun 
    public float currentPanic = 0;

    public bool isRegening = false;
    public bool isBleeding = false;
}
