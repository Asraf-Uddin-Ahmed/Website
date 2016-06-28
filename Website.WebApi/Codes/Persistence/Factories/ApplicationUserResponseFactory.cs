using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Website.Foundation.Core.Aggregates;
using Website.Identity.Managers;
using Website.Identity.Aggregates;
using Website.WebApi.Codes.Core.Factories;
using Website.WebApi.Models.Response;

namespace Website.WebApi.Codes.Persistence.Factories
{
    public class ApplicationUserResponseFactory : ResponseFactory<ApplicationUserResponseModel>, IApplicationUserResponseFactory
    {
        public ApplicationUserResponseFactory(HttpRequestMessage httpRequestMessage)
            :base(httpRequestMessage)
        {
        }

        public ApplicationUserResponseModel Create(ApplicationUser applicationUser)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserResponseModel>()
                    .ForMember(dest => dest.Url, opt => opt.MapFrom(src => UrlHelper.Link("GetUserById", new { id = src.Id })))
                    .ForMember(dest => dest.RoleUrl, opt => opt.MapFrom(src => UrlHelper.Link("GetRoleByUserID", new { userID = src.Id })))
                    .ForMember(dest => dest.ClaimUrl, opt => opt.MapFrom(src => UrlHelper.Link("GetClaimByUserID", new { userID = src.Id })));
            });
            return Mapper.Map<ApplicationUserResponseModel>(applicationUser);
        }

        public IEnumerable<ApplicationUserResponseModel> Create(IEnumerable<ApplicationUser> applicationUsers)
        {
            return applicationUsers.Select(u => this.Create(u));
        }
    }
}