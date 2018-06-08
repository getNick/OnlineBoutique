using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace OnlineBoutique.Models.EmunsAndConst
{
    public static class SizeResponseController
    {
        public static string GetResponse(double rating)
        {
            if (rating >= 8)
            {
                return "Изделие отлично/хорошо сидит по фигуре, можно заказывать.";
            }
            if (rating >= 6)
            {
                return "ВНИМАНИЕ! изделие сядет на вашу фигуру, но с нюансами. Например, изделие может быть слишком плотно по фигуре в талии или бедрах, сильно подчеркивать контур фигуры, затруднять ваши движения и т.д.";
            }
            if (rating >= 4)
            {
                return "Изделие очень слабо, но сядет на Вашу фигуру.";
            }
            if (rating >= 2)
            {
                return "Изделие не сядет на вашу фигуру.";
            }
            return "Размер вам не подходит.";
        }

        public static double CalculateRating(double size,double value, double boundValue)
        {
            double rating = ((Math.Abs(size - value)) / Math.Abs(boundValue - value));
            if ((rating < 0) | (rating > 1))
            {
                return 0;
            }
            return (1.0-rating) * 10;
        }

        public static ProductVariation CalculateRating(ProductVariation pv, UserSizes user)
        {
            foreach (var sizeVariation in pv.SizeVariation)
            {
                double arrange = 0;
                foreach (var size in sizeVariation.ListParams)
                {
                    switch (size.TypeSize)
                    {
                        case SizesEnum.Breast:
                        {
                            if (user.Breast < size.Value)
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Breast,
                                    (double) size.Value, (double) size.Value * 0.95);
                            }
                            else
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Breast,
                                    (double) size.Value, (double) size.MaxValue);
                            }

                            size.UserSize = (double)user.Breast;
                            size.SizeResponse = SizeResponseController.GetResponse(size.Rating);
                        }
                            break;
                        case SizesEnum.Waist:
                        {
                            if (user.Waist < size.Value)
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Waist,
                                    (double) size.Value, (double) size.Value * 0.95);
                            }
                            else
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Waist,
                                    (double) size.Value, (double) size.MaxValue);
                            }
                            size.UserSize = (double)user.Waist;
                                size.SizeResponse = SizeResponseController.GetResponse(size.Rating);
                        }
                            break;
                        case SizesEnum.Hips:
                        {
                            if (user.Thigh < size.Value)
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Thigh,
                                    (double) size.Value, (double) size.Value * 0.95);
                            }
                            else
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.Thigh,
                                    (double) size.Value, (double) size.MaxValue);
                            }
                            size.UserSize = (double)user.Thigh;
                                size.SizeResponse = SizeResponseController.GetResponse(size.Rating);
                        }
                            break;
                        case SizesEnum.WidthOfTheShoulders:
                        {
                            if (user.ShouldersWidth < size.Value)
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.ShouldersWidth,
                                    (double) size.Value, (double) size.Value * 0.95);
                            }
                            else
                            {
                                size.Rating = SizeResponseController.CalculateRating((double) user.ShouldersWidth,
                                    (double) size.Value, (double) size.MaxValue);
                            }
                            size.UserSize = (double)user.ShouldersWidth;
                            size.SizeResponse = SizeResponseController.GetResponse(size.Rating);
                        }
                            break;
                    }

                    arrange += size.Rating;
                }

                arrange /= 4.0;
                sizeVariation.Rating = arrange;
                sizeVariation.SizeResponse = SizeResponseController.GetResponse(arrange);
            }

            return pv;
        }
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType().GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>()
                .Name;
        }
    }
}
    

