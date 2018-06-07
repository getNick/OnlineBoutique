using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MarketCore.EmunsAndConst;

namespace MarketCore.Classes
{
    public class UserSizes
    {
        [Key]
        public int UserSizesId { get; set; }
        /// <summary>
        /// Грудь
        /// </summary>
        public double? Breast { get; set; }
        /// <summary>
        /// Талия
        /// </summary>
        public double? Waist { get; set; }
        /// <summary>
        /// Бедра
        /// </summary>
        public double? Thigh { get; set; }
        /// <summary>
        /// Обхват бедра
        /// </summary>
        public double? ThighGirth { get; set; }
        /// <summary>
        /// Рост
        /// </summary>
        public double? Height { get; set; }
        /// <summary>
        /// Ширина плеч
        /// </summary>
        public double? ShouldersWidth { get; set; }


    }
}
