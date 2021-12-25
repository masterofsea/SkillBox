using System;

namespace Diary.Models.DiaryBook.Exceptions
{
    public class RecordDoesNotExistsException : Exception
    {
        public RecordDoesNotExistsException() : base()
        {
        }
        
        public RecordDoesNotExistsException(string message) : base(message)
        {
        }
        
        public RecordDoesNotExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}