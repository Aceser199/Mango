﻿namespace Mango.Web.Utility
{
    public class SD
    {
        public static string CouponAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string TokenCookie = "JWTToken";

        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE,
        }

        // Todo: Remove these
        public const string RoleAdmin = "Admin";
        public const string RoleCustomer = "Customer";

    }
}
