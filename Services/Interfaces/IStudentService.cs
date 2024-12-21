using API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student;
using API_.NET_CRUD_Minimal_E.F_ORM.Models;

namespace API_.NET_CRUD_Minimal_E.F_ORM.Services.Interfaces
{
    public interface IStudentService
    {
        Task<Student> GetByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<StudentDTO> AddAsync(Student student);
        Task UpdateAsync(Student student);
        Task DeleteAsync(int id);
    }
}
