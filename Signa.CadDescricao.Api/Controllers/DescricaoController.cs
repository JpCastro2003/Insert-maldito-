using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Signa.CadDescricao.Api.Business;
using Signa.CadDescricao.Api.Domain.Entities;
using Signa.CadDescricao.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace Signa.CadDescricao.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Authorize("Bearer")]
    public class DescricaoController : Controller
    {
        private readonly DescricaoBL descricaoBL;

        public DescricaoController(DescricaoBL descricaoBL)
        {
            this.descricaoBL = descricaoBL;
        }
        [HttpPost]
        [Route("cadastro")]
        public ActionResult<DescricaoModel> Insert(DescricaoModel descricao)
        {
            try
            {
                return Ok(descricaoBL.Insert(descricao));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("cadastro/{id}")]
        public ActionResult Delete(int id)
        {
            descricaoBL.Delete(id);
            return Ok();
        }

        [HttpGet]
        [Route("searchById")]
        public ActionResult<DescricaoModel> SearchById(DescricaoEntity descricao) => Ok(descricaoBL.SearchById(descricao));

        [HttpGet]
        [Route("pesquisa")]
        public ActionResult<IEnumerable<DescricaoModel>> Pesquisa(DescricaoModel Cadastro) => Ok(descricaoBL.Pesquisa(Cadastro)); 
    }
}
