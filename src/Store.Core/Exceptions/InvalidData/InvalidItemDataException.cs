﻿namespace Store.Core.Exceptions.InvalidData;

public class InvalidItemDataException : Exception
{
    public InvalidItemDataException() { }
    public InvalidItemDataException(string message) : base(message) { }
    public InvalidItemDataException(string message, Exception inner) : base(message, inner) { }
}