using DAL.DataBase;
using DAL.ReadAndWriteFactory;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly IReadFactory _readFactory;
        private readonly IWriteFactory _writeFactory;
       
        private readonly SplitBillDbContext _context;

        public DataAccessLayer(DbContextOptions<SplitBillDbContext> options)
        {
            _context = new SplitBillDbContext(options);
            _readFactory = new ReadFactory(_context);
            _writeFactory = new WriteFactory(_context);
            
        }

        public IReadFactory Read()
        {
            return _readFactory;
        }

        public IWriteFactory Write()
        {
            return _writeFactory;
        }

       

    }
}
