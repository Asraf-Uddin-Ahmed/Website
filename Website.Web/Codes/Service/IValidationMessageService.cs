using System;
namespace Website.Web.Codes.Service
{
    public interface IValidationMessageService
    {
        string GetErrorMessageFromModelState(System.Web.Mvc.ViewDataDictionary viewData);
        void StoreActionResponseMessageError(string message);
        void StoreActionResponseMessageError(System.Web.Mvc.ViewDataDictionary viewData);
        void StoreActionResponseMessageInfo(string message);
        void StoreActionResponseMessageSuccess(string message);
        void StoreActionResponseMessageWarning(string message);
    }
}
