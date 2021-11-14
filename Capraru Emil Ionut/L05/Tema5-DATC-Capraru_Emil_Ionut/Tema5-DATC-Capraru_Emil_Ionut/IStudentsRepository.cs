using Tema5_DATC_Capraru_Emil_Ionut.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tema5_DATC_Capraru_Emil_Ionut
{
    public interface IStudentsRepository
    {
        Task<List<StudentEntity>> GetAllStudents();
    }
}

