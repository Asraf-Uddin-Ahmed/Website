using System;
namespace Website.Web.Codes.Core.Services
{
    public interface IUrlMakerService
    {
        string GetSiteUrl();
        string GetUrlConfirmUser(string varificationCode);
        string GetUrlForgotPassword(string varificationCode);
    }
}
