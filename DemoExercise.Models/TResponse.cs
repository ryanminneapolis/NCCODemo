using System;
using System.Collections.Generic;
using System.Text;

namespace DemoExercise.Models
{
	public class TResponse<T>
	{
		public string Message { get; set; }

		public bool IsSuccesful { get; set; }

		public T Payload { get; set; }
	}
}
