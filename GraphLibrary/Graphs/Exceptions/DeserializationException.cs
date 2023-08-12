﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary.Graphs.Exceptions
{
	public class DeserializationException : Exception
	{
		public DeserializationException() : base() { }
		public DeserializationException(string message) : base(message) { }
		public DeserializationException(string message, Exception innerException) : base(message, innerException) { }
	}
}
