using CLModel;
using Dapper;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace CLData.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly MySQLConfiguration _mySQLConfiguration;
        protected MySqlConnection DbConnection()
        {
            return new MySqlConnection(_mySQLConfiguration.ConnectionString);
        }

        public CarRepository(MySQLConfiguration mySQLConfiguration)
        {
            _mySQLConfiguration = mySQLConfiguration;
        }
        public Task<bool> DeleteCar(Car car)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            var db = DbConnection();
            var sql = "SELECT id, make, model, color, year, doors FROM cars";
            return await db.QueryAsync<Car>(sql, new { });
        }

        public async Task<Car> GetCarById(int id)
        {
            var db = DbConnection();
            var sql = "SELECT id, make, model, color, year, doors FROM cars Where id = @Id";
            return await db.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });
        }

        public async Task<bool> InsertCar(Car car)
        {
            var db = DbConnection();
            var sql = @"Insert Into cars (make, model, color, year, doors) 
                      values (@Make, @Model, @Color, @Year, @Doors)";
            return await db.ExecuteAsync(sql, new { car.Make, car.Model, car.Color, car.Year, car.Doors }) > 0;
        }

        public Task<bool> UpdateCar(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
