using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class Codejam
{
	bool verbose = false;

	public static void Main( string[] args )
	{
		Codejam cj = new Codejam();

		using (var fs = new FileStream(args[0], FileMode.Open)) {
			using (var sr = new StreamReader(fs)) {
				int[] num = cj.ParseLineNumbers( sr );
				for( int i=0; i<num[0]; i++ ) {
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Problem( sr )));
				}
			}
		}
	}

	int Problem( StreamReader sr )
	{
		folders = new Dictionary<string,Folder>();
		int[] num = ParseLineNumbers( sr );

		for( int n=0; n<num[0]; n++) {
			Mkdir( sr );
		}

		int count = 0;
		for( int m=0; m<num[1]; m++) {
			count += Mkdir( sr );
		}

		return count;
	}

	int Mkdir( StreamReader sr )
	{
		int count = 0;
		var f = folders;
		string path = "";
		foreach( var p in ParsePath( sr )) {
			path += "/" + p;
			if (f.ContainsKey(p)) {
				f = f[p].Children;
			} else {
				// Console.WriteLine("mkdir "+path);
				count++;
				f[p] = new Folder(p);
				f = f[p].Children;
			}
		}
		return count;
	}

	Dictionary<string,Folder> folders = new Dictionary<string,Folder>();

	public class Folder
	{
		string name;
		public Dictionary<string,Folder> Children = new Dictionary<string,Folder>();
		public Folder( string name )
		{
			this.name = name;
		}
	}

	string[] ParsePath( StreamReader sr )
	{
		string line = sr.ReadLine().Trim();
		if (line == "/") {
			return new string[] {};
		}
		return line.Substring(1).Split('/');
	}

	int[] ParseLineNumbers( StreamReader sr )
	{
		var result = new List<int>();

		string line = sr.ReadLine().Trim();
		if (verbose) Console.WriteLine(line);
		foreach( var s in line.Split()) {
			result.Add( int.Parse(s) );
		}

		return result.ToArray();
	}
}
