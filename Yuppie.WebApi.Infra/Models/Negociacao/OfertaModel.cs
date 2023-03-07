using System;
using System.ComponentModel.DataAnnotations;

namespace Yuppie.WebApi.Infra.Models.Negociacao
{
    public class OfertaModel
    {
        [Key]
        public int id { get; set; }
        public DateTime create_date { get; set; }
        public int id_produto { get; set; }
        public int id_un_medida { get; set; }
        public int id_vendedor { get; set; }
        public float peso_un_medida { get; set; }
        public int qtd_disponivel { get; set; }
        public bool status { get; set; }
        public DateTime update_date { get; set; }
        public float vlkg { get; set; }
        public float vl_un_medida { get; set; }
        public DateTime? sent_message_date { get; set; }
        public int sent_message_counter { get; set; }
    }
}


