namespace Models.Stats;

// Из чего складывается итоговое значение одной характеристики персонажа.
// Сумма всех частей равна итоговому значению (Total).
public sealed class StatBreakdown
{
    // Базовые характеристики класса (до усилений).
    public int Base { get; set; }

    // Вещи: броня, оружие, бижутерия и плащ — их характеристики и белые статы, без камней.
    public int Gear { get; set; }

    // Камни: камни силы (фиксированные значения) и проценты камней концентрации.
    public int Gems { get; set; }

    // Эликсиры и усиления гильдии: на сколько они увеличили характеристики класса и вещей.
    public int Boost { get; set; }

    // Аура воина.
    public int Aura { get; set; }

    public int Total => Base + Gear + Gems + Boost + Aura;
}
