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
				long[] num = cj.ParseLineNumbers( sr );
				for( int i=0; i<num[0]; i++ ) {
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Problem( sr )));
				}
			}
		}
	}

	int Problem( StreamReader sr )
	{
		long[] num = ParseLineNumbers( sr );
		long L = num[0];
		long P = num[1];
		long C = num[2];
		long result = 0;

		long n = L;
		int reps = 0;
		while (n<P) {
			reps++;
			n *= C;
		}

		var log = Math.Log( reps, C );
		if (double.IsInfinity(log)) {
			return 0;
		}

		log = Math.Ceiling(log);

		return (int) log;
	}

	long[] ParseLineNumbers( StreamReader sr )
	{
		var result = new List<long>();

		string line = sr.ReadLine().Trim();
		if (verbose) Console.WriteLine(line);
		foreach( var s in line.Split()) {
			result.Add( long.Parse(s) );
		}

		return result.ToArray();
	}
}
