﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Web.Codes.Core.Services.UriMaker;

namespace Website.Web.Codes.Persistence.Services.UriMaker
{
    public class ForgotPasswordUriBuilder : IForgotPasswordUriBuilder
    {
        private const string _URI = "/Account/ChangePassword?code=";
        private string _verificationCode;
        private string _VerificationCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_verificationCode))
                {
                    throw new NullReferenceException("VerificationCode is not provided");
                }
                return _verificationCode;
            }
            set
            {
                _verificationCode = value;
            }
        }

        public void Build(string verificationCode)
        {
            _VerificationCode = verificationCode;
        }

        public string GetUri()
        {
            return _URI + _VerificationCode;
        }
    }
}