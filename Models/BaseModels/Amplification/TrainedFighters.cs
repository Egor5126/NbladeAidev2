using Models.Stats;

namespace Models.BaseModels.Amplification;

public class TrainedFighters : Amplification
{
    public TrainedFighters() : base(
        "Тренированные бойцы (гильдия)",
        new []
        {
            (StatType.Health, 1.05),
            (StatType.CriticalDamageResistance, 1.05)
        },
        "Ваше здоровье и сопротивление критическому урону увеличены на 5%")
    {
    }
}