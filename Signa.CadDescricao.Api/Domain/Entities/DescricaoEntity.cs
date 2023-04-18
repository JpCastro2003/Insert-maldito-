namespace Signa.CadDescricao.Api.Domain.Entities
{
    public class DescricaoEntity
    {
        public int TAB_DESCRICAO_ID { get; set; }
        public string? INDIC_TIPO { get; set; }
        public string? CODIGO { get; set; }
        public string? DESCRICAO { get; set; }
        public int zUSUARIO_ID { get; set; }
        public int TAB_STATUS_ID { get; set; }
    }

    public class DescricaoInsertRetorno
    {
        public int retorno { get; set; }
        public string msg_ret { get; set; }
        public int tab_descricao_Id { get; set; }
    }
}
