using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjCF.Mobile.Data.Configuration
{
    public static class Configuracao
    {
        private static string DatabaseName = "controle_financeiro_db.db";
        private static string DatabaseDirectory = FileSystem.AppDataDirectory;
        public static string DatabasePath = Path.Combine(DatabaseDirectory, DatabaseName);
    }
}
