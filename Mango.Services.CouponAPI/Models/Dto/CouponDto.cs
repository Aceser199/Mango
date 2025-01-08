﻿namespace Mango.Services.CouponAPI.Models.Dto
{
    public class CouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
