using System;
using System.IO;

namespace AIEdit
{
	/// <summary>
	/// Summary description for IAIObject.
	/// </summary>
	public interface IAIObjectOld
	{
        string Name { get; set; }
        string ID { get; set; }
	}

	public interface IAIObject
	{
		string Name { get; set; }
		string ID { get; }

		void Write(StreamWriter stream);
	}
}
