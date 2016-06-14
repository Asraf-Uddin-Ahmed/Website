using System;
using System.Collections.Generic;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;
using Website.WebApi.Models.Common;
using Website.WebApi.Models.Request;
using Website.WebApi.Models.Response;
namespace Website.WebApi.Codes.Core.Factories
{
    public interface IUserResponseFactory
    {
        ResponseCollectionModel<UserResponseModel> Create(IEnumerable<User> users, Pagination pagination, SortBy sortBy, int totalItem);
        UserResponseModel Create(User user);
    }
}
