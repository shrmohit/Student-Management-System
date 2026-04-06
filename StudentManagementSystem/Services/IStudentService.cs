
using StudentManagementAPI.Models;

namespace StudentManagementAPI.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetById(int id);
        Task Add(Student student);
        Task Update(Student student);
        Task Delete(int id);
    }
}
