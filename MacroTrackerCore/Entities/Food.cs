namespace MacroTrackerCore.Entities
{
    public class Food
    {
        required public string Name { get; set; }
        required public int CaloriesPer100g { get; set; }
        required public int ProteinPer100g { get; set; }
        required public int CarbsPer100g { get; set; }
        required public int FatPer100g { get; set; }
    }
}
