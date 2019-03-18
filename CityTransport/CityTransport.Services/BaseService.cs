namespace CityTransport.Services
{
    using CityTransport.Data;

    public abstract class BaseService
    {
        protected CityTransprtDbContext Db { get; private set; }

        protected BaseService(CityTransprtDbContext db)
        {
            this.Db = db;
        }

    }
}
