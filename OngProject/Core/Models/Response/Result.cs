using System.Collections.Generic;

namespace OngProject.Core.Models.Response
{
    public class Result
    {
        public bool Success { get; protected set; }
        public string FailureMessage { get; protected set; }
        public List<string> ErrorList { get; protected set; }

        protected Result()
        {
            this.Success = true;
        }

        protected Result(string message)
        {
            this.Success = false;
            this.FailureMessage = message;
        }

        protected Result(List<string> errorList)
        {
            this.Success = false;
            this.ErrorList = errorList;
        }

        public static Result SuccessResult()
        {
            return new Result();
        }

        public static Result FailureResult(string message)
        {
            return new Result(message);
        }

        public static Result ErrorResult(List<string> errorList)
        {
            return new Result(errorList);
        }

        public bool isError()
        {
            return ErrorList != null && ErrorList.Count > 0;
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; protected set; }

        protected Result(T t)
        {
            this.Success = true;
            this.Data = t;
        }

        public static Result SuccessResult(T t)
        {
            return new Result<T>(t);
        }
    }
}
