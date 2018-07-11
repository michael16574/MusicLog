using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLog.Database
{
    public class DatabaseWrapper
    {
        private DatabaseInstance _database;
        
        public DatabaseWrapper()
        {
            this._database = new DatabaseInstance();
        }
        
    }
}
