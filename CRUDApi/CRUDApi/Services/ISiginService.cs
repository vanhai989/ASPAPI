using CRUDApi.Models.AuthModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApi.Services
{
    interface ISiginService
    {
        Task<SiginResultModel> Sigin([FromBody] InforUser inforUser);
    }
}
