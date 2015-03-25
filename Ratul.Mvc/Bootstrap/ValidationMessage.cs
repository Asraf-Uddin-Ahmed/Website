using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ratul.Mvc.Bootstrap
{
    public static class CustomValidationMessage
    {
        private static Dictionary<ActionResponseMessageType, string> _bootstrapClassByType;
        private static Dictionary<ActionResponseMessageType, string> _BootstrapClassByType
        {
            get
            {
                if (_bootstrapClassByType == null)
                {
                    _bootstrapClassByType = new Dictionary<ActionResponseMessageType, string>();
                    _bootstrapClassByType.Add(ActionResponseMessageType.Error, "alert-danger");
                    _bootstrapClassByType.Add(ActionResponseMessageType.Info, "alert-info");
                    _bootstrapClassByType.Add(ActionResponseMessageType.Success, "alert-success");
                    _bootstrapClassByType.Add(ActionResponseMessageType.Warning, "alert-warning");
                }
                return _bootstrapClassByType;
            }
        }

        public static MvcHtmlString BootstrapValidationMessage(this HtmlHelper htmlHelper, ActionResponse actionResponse)
        {
            string validationHtml = "<div class='alert "+_BootstrapClassByType[actionResponse.MessageType]+"'>" +
                                        "<a href='#' class='close' data-dismiss='alert'>&times;</a>" + actionResponse.Message +
                                    "</div>";
            MvcHtmlString mvcValidationHtml = new MvcHtmlString(validationHtml);
            return mvcValidationHtml;
        }
    }
}
