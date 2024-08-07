﻿using AutoMapper;
using Online_Learning_Management.Domain.Entities.Students;
using Online_Learning_Management.Domain.Exceptions.Auth;
using Online_Learning_Management.Domain.Interfaces.Students;
using Online_Learning_Management.Infrastructure.DTOs.Student;

namespace Online_Learning_Management.Application.Students.Services
{
    public class StudentService : IStudentServices
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;


        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                throw new StudentNotfoundException();
            }
            return _mapper.Map<Student>(student);
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            if (students == null)
            {
                throw new ArgumentException("There are no students.");
            }
            return _mapper.Map<IEnumerable<Student>>(students);
        }

        public async Task AddStudentAsync(CreateStudentDTO createStudentDTO)
        {
            var student = _mapper.Map<Student>(createStudentDTO);
            await _studentRepository.AddStudentAsync(student);
        }

        public async Task UpdateStudentAsync(Guid id, UpdateStudentDTO updateStudentDTO)
        {
            var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
            if (existingStudent == null)
            {
                throw new StudentNotfoundException();
            }
            _mapper.Map(updateStudentDTO, existingStudent);
            await _studentRepository.UpdateStudentAsync(existingStudent);
        }

        public async Task DeleteStudentAsync(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                throw new StudentNotfoundException();
            }
            await _studentRepository.DeleteStudentAsync(id);
        }
    }
}
