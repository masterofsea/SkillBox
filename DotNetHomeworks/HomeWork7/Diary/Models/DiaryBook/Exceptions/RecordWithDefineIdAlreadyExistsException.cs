using System;

namespace Diary.Models.DiaryBook.Exceptions
{
    public class RecordWithDefineIdAlreadyExistsException : Exception
    {
        public RecordWithDefineIdAlreadyExistsException() : base()
        {
        }

        public RecordWithDefineIdAlreadyExistsException(string message) : base(message)
        {
        }

        public RecordWithDefineIdAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}