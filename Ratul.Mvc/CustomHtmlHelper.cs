﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Ratul.Mvc
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString MenuLink(this HtmlHelper htmlHelper, 
            string linkText, string actionName, string controllerName, string cssClassName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (actionName == currentAction && controllerName == currentController)
            {
                return htmlHelper.ActionLink(
                    linkText,
                    actionName,
                    controllerName,
                    null,
                    new
                    {
                        @class = cssClassName
                    });
            }
            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }

        public static string AddCssClassIfNeeded(this HtmlHelper htmlHelper,
            string actionName, string controllerName, string cssClassName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase)
                && actionName.Equals(currentAction, StringComparison.OrdinalIgnoreCase))
            {
                return cssClassName;
            }
            return "";
        }

        public static string AddCssClassIfNeeded(this HtmlHelper htmlHelper,
            string[] actionNames, string controllerName, string cssClassName)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");
            if (controllerName.Equals(currentController, StringComparison.OrdinalIgnoreCase)
                && Array.Exists(actionNames, s => s.Equals(currentAction, StringComparison.OrdinalIgnoreCase)))
            {
                return cssClassName;
            }
            return "";
        }
    }
}
