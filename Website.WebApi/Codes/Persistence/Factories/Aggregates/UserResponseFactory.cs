using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Foundation.Core.SearchData;
using Website.WebApi.Codes.Core.Factories;
using Website.WebApi.Models;
using Website.WebApi.Models.Request;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public class UserResponseFactory : ResponseFactory<UserResponseModel>, IUserResponseFactory
    {
        private MapperConfiguration _mapperConfiguration;
        public UserResponseFactory(HttpRequestMessage httpRequestMessage)
            :base(httpRequestMessage)
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserResponseModel>()
                    .ForMember(dest => dest.Url, opt => opt.UseValue<string>("#NoUrl"));//opt.MapFrom(src => UrlHelper.Link("#NoUrl", new { id = src.ID }))
            });
        }

        public UserResponseModel Create(User user)
        {
            return _mapperConfiguration.CreateMapper().Map<UserResponseModel>(user);
        }

        public ResponseCollectionModel<UserResponseModel> Create(IEnumerable<User> users, Pagination pagination, SortBy sortBy, int totalItem)
        {
            return base.Create(users.Select(r => this.Create(r)), pagination, sortBy, totalItem);
        }
    }
}