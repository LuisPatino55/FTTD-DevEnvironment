
public interface IDurable 
{
    float CurrentDurability {  get; set; }
    float MaxDurability { get; set; }

    void ReduceDurability();

    void RepairDurability();
}
