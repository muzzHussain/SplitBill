using DAL.ReadAndWriteFactory;

using DAL.Repos.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDataAccessLayer
    {
        public IReadFactory Read();
        public IWriteFactory Write();

        

    }
}
