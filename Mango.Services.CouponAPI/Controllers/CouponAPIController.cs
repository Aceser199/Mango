﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;
        private IMapper _mapper;
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _responseDto = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();

                _responseDto.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon? obj = _db.Coupons.FirstOrDefault(x => x.Id == id);

                if (obj == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon was not found";
                }

                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon? obj = _db.Coupons.FirstOrDefault(x => x.Code.ToLower() == code.ToLower());

                if (obj == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon was not found";
                }

                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Update(obj);
                _db.SaveChanges();

                _responseDto.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon? obj = _db.Coupons.FirstOrDefault(x => x.Id == id);

                if (obj == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Coupon was not found";
                }

                _db.Coupons.Remove(obj!);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
            }

            return _responseDto;
        }
    }
}
