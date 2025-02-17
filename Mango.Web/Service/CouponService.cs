﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private const string couponApi = "/api/coupon";

        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = $"{SD.CouponAPIBase}{couponApi}",
                Data = couponDto
            });
        }

        public async Task<ResponseDto> DeleteCouponsAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = $"{SD.CouponAPIBase}{couponApi}/{id}",
            });
        }

        public async Task<ResponseDto> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}{couponApi}"
            });
        }

        public async Task<ResponseDto> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}{couponApi}/GetByCode/{couponCode}",
            });
        }

        public async Task<ResponseDto> GetCouponByIdAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = $"{SD.CouponAPIBase}{couponApi}/{id}",
            });
        }

        public async Task<ResponseDto> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = $"{SD.CouponAPIBase}{couponApi}",
                Data = couponDto
            });
        }
    }
}
