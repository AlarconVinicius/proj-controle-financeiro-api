using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjControleFinanceiro.Mobile.Configuracao
{
    public static class AppSettings
    {
        private static string DatabaseName = "controle_financeiro_db.db";
        private static string DatabaseDirectory = FileSystem.AppDataDirectory;
        public static string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
    }
}
