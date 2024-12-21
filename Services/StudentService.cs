using API_.NET_CRUD_Minimal_E.F_ORM.DTOs.Student;
using API_.NET_CRUD_Minimal_E.F_ORM.Models;
using API_.NET_CRUD_Minimal_E.F_ORM.Repositories.Interfaces;
using API_.NET_CRUD_Minimal_E.F_ORM.Services.Interfaces;

namespace API_.NET_CRUD_Minimal_E.F_ORM.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            try
            {
                return await _studentRepository.GetByIdAsync(id);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An error occurred while retrieving the student.");
            }
        }

        public async Task<IEnumerable<Student>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _studentRepository.GetAllAsync(pageNumber, pageSize);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _studentRepository.GetTotalCountAsync();
        }

        public async Task<StudentDTO> AddAsync(Student student)
        {
            try
            {
                await _studentRepository.AddAsync(student);

                var studentDto = new StudentDTO
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                };

                return studentDto;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An error occurred while processing your request.");
            }
        }

        public async Task UpdateAsync(Student student)
        {
            try
            {
                await _studentRepository.UpdateAsync(student);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An error occurred while updating the student.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await _studentRepository.DeleteAsync(id);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("An error occurred while deleting the student.");
            }
        }
    }
}
