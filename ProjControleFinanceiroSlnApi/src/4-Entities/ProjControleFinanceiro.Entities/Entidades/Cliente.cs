using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjControleFinanceiro.Entities.Entidades
{
    public class Cliente : Entity
    {

        //EF 
        public IEnumerable<Transacao> transacoes { get; set; }
        public Cliente(){ }
    

    }
}
