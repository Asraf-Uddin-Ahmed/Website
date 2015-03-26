using System;
namespace Website.Web.Codes.Helper
{
    public interface IUrlMakerHelper
    {
        string GetSiteUrl();
        string GetUrlConfirmUser(string varificationCode);
        string GetUrlForgotPassword(string varificationCode);
    }
}
