using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Website.Foundation.Aggregates;
using Website.Foundation.Services;
using Website.Web.App_Start;

namespace Website.Web.Models.Account
{
    public class ShowUserModel
    {
        private IUserService _userService;

        private List<IUser> _listUser;
        public List<IUser> ListUser
        {
            get
            {
                if(_listUser == null)
                {
                    _listUser = _userService.GetAll().ToList();
                }
                return _listUser;
            }
        }

        public ShowUserModel()
        {
            _userService = NinjectWebCommon.GetConcreteInstance<IUserService>();
        }
    }
}