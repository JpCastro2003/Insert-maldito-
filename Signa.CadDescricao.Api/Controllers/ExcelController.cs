using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.CadDescricao.Api.Business;
using Signa.CadDescricao.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Signa.CadDescricao.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Bearer")]

    public class ExcelController : Controller
    {
        private readonly ExcelBL _excelBLL;

        public ExcelController(ExcelBL excelBLL)
        {
            _excelBLL = excelBLL;
        }

        [HttpPost]
        [Route("excel")]
        public ActionResult<string> GerarExcel(ExcelModel dadosPlanilha) => Ok(_excelBLL.GerarExcel(dadosPlanilha));
    }
}

