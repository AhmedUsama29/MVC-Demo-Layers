

namespace Demo.DataAccess.Repositories.Classes
{
    public  class DepartmentRepository(AppDBContext _dbContext) : GenericRepository<Department>(_dbContext),
                                                                  IDepartmentRepository
    {

    }
}