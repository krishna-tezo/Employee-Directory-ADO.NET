﻿namespace EmployeeDirectory.Models
{
    public class ServiceResult<T>
    {
        public List<T> DataList { get; set; } = new List<T>();
        public T Data { get; set; }

        public bool IsOperationSuccess { get; set; }
        public string? Message { get; set; }

        private ServiceResult() { }

        public static ServiceResult<T> Success(List<T> dataList, string? message = null)
        {
            return new ServiceResult<T>
            {
                IsOperationSuccess = true,
                Message = message,
                DataList = dataList
            };
        }

        public static ServiceResult<T> Success(T data, string? message = null)
        {
            return new ServiceResult<T>
            {
                IsOperationSuccess = true,
                Message = message,
                Data = data
            };
        }
        

        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>
            {
                IsOperationSuccess = false,
                Message = message
            };
        }
    }
}
