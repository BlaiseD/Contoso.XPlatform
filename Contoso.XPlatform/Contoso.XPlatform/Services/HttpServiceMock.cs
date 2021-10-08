﻿using Contoso.Bsl.Business.Requests;
using Contoso.Bsl.Business.Responses;
using Contoso.Domain;
using Contoso.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.XPlatform.Services
{
    public class HttpServiceMock : IHttpService
    {
        public Task<GetListResponse> GetObjectDropDown(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<GetListResponse> GetList(GetTypedListRequest request, string url = null)
        {
            return Task.FromResult(new GetListResponse { Success = true, List = new List<EntityModelBase> { } });
        }

        public Task<GetEntityResponse> GetEntity(GetEntityRequest request, string url = null)
        {
            return Task.FromResult(new GetEntityResponse { Success = true });
        }

        public Task<SaveEntityResponse> SaveEntity(SaveEntityRequest request, string url = null)
        {
            return Task.FromResult(new SaveEntityResponse { Success = true });
        }

        public Task<DeleteEntityResponse> DeleteEntity(DeleteEntityRequest request, string url = null)
        {
            return Task.FromResult(new DeleteEntityResponse { Success = true });
        }
    }
}
