using Dapper;
using Signa.CadDescricao.Api.Domain.Entities;
using Signa.Library.Core.Data.Repository;
using System.Collections.Generic;
using System.Data;

namespace Signa.CadDescricao.Api.Data.Repository
{
    public class DescricaoDAO : RepositoryBase
    {
        public int Insert(DescricaoEntity descricao)
        {
            var sql = "SP_040_INC_TAB_DESCRICAO";
            var param = new
            {
                descricao = new DbString { Value = descricao.DESCRICAO },
                codigo = new DbString { Value = descricao.CODIGO },
                indic_tipo = new DbString { Value = descricao.INDIC_TIPO },
                zUSUARIO_ID = descricao.zUSUARIO_ID
            };
            using (var db = Connection)
            {
                var resultado = db.QueryFirstOrDefault<DescricaoInsertRetorno>(sql, param, commandType: CommandType.StoredProcedure);
                return resultado.tab_descricao_Id;
            }
        }
        public DescricaoEntity VerDuplicidadeInsert(DescricaoEntity descricao)
        {
            var sql = "SP_040_VER_DUP_TAB_DESCRICAO";
            var param = new
            {
                TAB_DESCRICAO_ID = descricao.TAB_DESCRICAO_ID,
                descricao = new DbString { Value = descricao.DESCRICAO },
                codigo = new DbString { Value = descricao.CODIGO },
                indic_tipo = new DbString { Value = descricao.INDIC_TIPO },
                TAB_STATUS_ID = descricao.TAB_STATUS_ID,
            };
            using (var db = Connection)
            {
                var Query = db.QueryFirstOrDefault(sql, param, commandType: CommandType.StoredProcedure);
                return Query;
            }
            
        }
        public void Delete(int id)
        {
            var sql = "SP_040_EXC_TAB_DESCRICAO";
            var param = new
            {
                TAB_DESCRICAO_ID = id,
                zUSUARIO_ID = 1,
            };
            using (var db = Connection)
            {
                db.Execute(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
        public DescricaoEntity SearchById(DescricaoEntity descricao)
        {
            var sql = "SP_040_CON_TAB_DESCRICAO";

            var param = new
            {
                TAB_DESCRICAO_ID = descricao.TAB_DESCRICAO_ID,
                descricao = new DbString { Value = descricao.DESCRICAO },
                codigo = new DbString { Value = descricao.CODIGO },
                indic_tipo = new DbString { Value = descricao.INDIC_TIPO },
            };
            using (var db = Connection)
            {
                return db.QueryFirst<DescricaoEntity>(sql, param, commandType: CommandType.StoredProcedure);
            }
        }
        public IEnumerable<DescricaoEntity> Pesquisa(DescricaoEntity descricao)
        {
            var sql = "Select TAB.TAB_DESCRICAO_ID, TAB.codigo, TAB.descricao, TAB.indic_tipo From  TAB_DESCRICAO TAB WHERE TAB.indic_tipo = @indic_tipo  AND TAB_STATUS_ID = 1";

            if (descricao.DESCRICAO != null)
            {
                sql += " AND TAB.descricao = @descricao";
            }
            if(descricao.CODIGO != null)
            {
                sql += " AND TAB.codigo = @codigo";
            }

            var param = new
            {
                indic_tipo = descricao.INDIC_TIPO,
                codigo = descricao.CODIGO,
                descricao = descricao.DESCRICAO
            };

            using (var db = Connection)
            {
                return db.Query<DescricaoEntity>(sql, param);
            }
        }
    }
}
