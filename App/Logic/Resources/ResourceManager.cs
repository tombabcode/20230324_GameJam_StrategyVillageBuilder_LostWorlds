namespace _20230324_GameJam_StrategyVillageBuilder_LostWorlds.App.Logic.Resources;

public sealed class ResourceManager {

    // Building materials
    public int Wood { get; private set; }
    public int Stone { get; private set; }

    // Food
    public int Meat { get; private set; }
    public int Fruits { get; private set; }
    public int Vegetables { get; private set; }
    public int Bread { get; private set; }

    // Tools
    public int Tools { get; private set; }
    public int Weapons { get; private set; }
    public int Armor { get; private set; }

    // Other
    public int Coins { get; private set; }
    public int Metal { get; private set; }
    public int Grain { get; private set; }

    // Statuses
    public float Happiness { get; private set; }
    public float Wealth { get; private set; }
    public float Faith { get; private set; }

    // Helpers
    public int TotalMaterials => Wood + Stone;
    public int TotalFood => Meat + Fruits + Vegetables + Bread;
    public int TotalTools => Tools + Weapons + Armor;

}
