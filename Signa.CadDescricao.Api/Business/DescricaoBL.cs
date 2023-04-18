using AutoMapper;
using Signa.CadDescricao.Api.Data.Repository;
using Signa.CadDescricao.Api.Domain.Entities;
using Signa.CadDescricao.Api.Domain.Models;
using Signa.Library.Core.Exceptions;
using Signa.Library.Core.Extensions;
using System.Collections.Generic;

namespace Signa.CadDescricao.Api.Business
{
    public class DescricaoBL
    {
        private readonly DescricaoDAO SQL;
        private readonly IMapper mapa;

        public DescricaoBL(DescricaoDAO descricao, IMapper mapa)
        {
            SQL = descricao;
            this.mapa = mapa;
        }
        public DescricaoModel Insert(DescricaoModel Cadastro) 
        {
            int id;
            var entidade = mapa.Map<DescricaoEntity>(Cadastro);

            if (Cadastro.TAB_DESCRICAO_ID.IsZeroOrNull())
            {
                SQL.VerDuplicidadeInsert(entidade);
                SQL.Insert(entidade);
            }
            else
            {
                id = Cadastro.TAB_DESCRICAO_ID;
                throw new SignaSqlNotFoundException("Já está cadastrado");
            }
            return Cadastro;
        }
        public IEnumerable<DescricaoModel> Pesquisa(DescricaoModel Cadastro) 
        {
            var entity = mapa.Map<DescricaoEntity>(Cadastro);
            var cadastro = SQL.Pesquisa(entity);

            if(cadastro == null)
            {
                throw new SignaSqlNotFoundException("Indic_Tipo, Codigo, Descricao: um destes filtros deve ser preenchido.");
            }
            return mapa.Map<IEnumerable<DescricaoModel>>(cadastro);
        }
        public void Delete(int id)
        {
            SQL.Delete(id);
        }
        public DescricaoModel SearchById(DescricaoEntity descricao)
        {
            var cadastro = SQL.SearchById(descricao);
            if (cadastro == null)
            {
                throw new SignaSqlNotFoundException("Nenhum Id encontrado");
            }
            cadastro.TAB_DESCRICAO_ID = descricao.TAB_DESCRICAO_ID;
            return mapa.Map<DescricaoModel>(cadastro);
        }
    }
}
