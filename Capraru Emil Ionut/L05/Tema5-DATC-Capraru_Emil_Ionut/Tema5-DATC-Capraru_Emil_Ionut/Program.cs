using Tema5_DATC_Capraru_Emil_Ionut.Models;
using Tema5_DATC_Capraru_Emil_Ionut.Repository;
using System;
using System.Collections.Generic;

namespace Tema5_DATC_Capraru_Emil_Ionut
{
    class Program
    {
        private static IStudentsRepository _studentsRepository;
        private static IMetricRepository _metricRepository;

        static void Main(string[] args)
        {
            _studentsRepository = new StudentsRepository();
            _metricRepository = new MetricRepository(_studentsRepository.GetAllStudents().Result);
            _metricRepository.GenerateMetric();

        }
    }
}