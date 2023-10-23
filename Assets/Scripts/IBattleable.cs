
public interface IBattleable
{
    public int CurrentHealth { get; set; }
    public int CurrentStamina { get; set; }
    public int CurrentPanic { get; set; }
    public int MaxPanic { get; set; }   // panic level will affect attack cooldown, blocking, accuracy, evasion, and raise chance of stun 

    public void TakeDamage(int damageAmount);
    public void BleedOut(int damageAmount, int statusDuration);
    public void GainHealth(int healAmount);
    public void RegenHealth(int healAmount, int statusDuration);

}
