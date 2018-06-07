using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MarketCore.EmunsAndConst
{
    public enum SeasonCategoriesEnum
    {
        Summer,
        Winter,
        Spring,
        Autumn,
        All,
    }

    public enum GenderCategoriesEnum
    {
        Woman,
        Man,
        All,
    }

    public enum DressTypeEnum
    {
        DressAndSkirt,
        Tshirt,
        JeansAndTrousers,
        Shirt,
        Outerwear,
        Sweater,
        All,
    }

    public enum StyleEnum
    {
        [DisplayName("Нарядный")]
        Chic,
        [DisplayName("Офисный")]
        Office,
        [DisplayName("Повседневный")]
        Casual,
        [DisplayName("Романтический")]
        Romantic,
        [DisplayName("Спортивный")]
        Sport,
    }
}
