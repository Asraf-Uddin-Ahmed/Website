using Ratul.Mvc.Bootstrap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Web.Codes;

namespace Website.Web.Codes.Service
{
    public class ValidationMessageService : IValidationMessageService
    {
        public string GetErrorMessageFromModelState(ViewDataDictionary viewData)
        {
            string errorMessage = "";
            foreach (ModelState modelState in viewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    errorMessage += ("<br>" + error.ErrorMessage);
                }
            }
            return errorMessage;
        }

        public void StoreActionResponseMessageError(ViewDataDictionary viewData)
        {
            string message = GetErrorMessageFromModelState(viewData);
            UserSession.ActionResponseMessage = new ActionResponse(ActionResponseMessageType.Error, message);
        }

        public void StoreActionResponseMessageError(string message)
        {
            UserSession.ActionResponseMessage = new ActionResponse(ActionResponseMessageType.Error, message);
        }

        public void StoreActionResponseMessageSuccess(string message)
        {
            UserSession.ActionResponseMessage = new ActionResponse(ActionResponseMessageType.Success, message);
        }

        public void StoreActionResponseMessageInfo(string message)
        {
            UserSession.ActionResponseMessage = new ActionResponse(ActionResponseMessageType.Info, message);
        }

        public void StoreActionResponseMessageWarning(string message)
        {
            UserSession.ActionResponseMessage = new ActionResponse(ActionResponseMessageType.Warning, message);
        }
    }
}