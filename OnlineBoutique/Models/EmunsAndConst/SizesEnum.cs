using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketCore.EmunsAndConst
{
    public enum SizesEnum
    {
        [Display(Name = "Грудь")]
        Breast,
        [Display(Name = "Талия")]
        Waist,
        [Display(Name = "Бедра")]
        Hips,
        [Display(Name = "Обхват руки")]
        GirthHands,
        [Display(Name = "Длина изделия")]
        Height,
        [Display(Name = "Обхват бедра")]
        GirthOfThighs,
        [Display(Name = "Ширина плеч")]
        WidthOfTheShoulders
    }
}
