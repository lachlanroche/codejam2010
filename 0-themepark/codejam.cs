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
				for( long i=0; i<num[0]; i++ ) {
					Console.WriteLine(string.Format("Case #{0}: {1}", 1+i, cj.Count( sr )));
				}
			}
		}
	}

	long Count( StreamReader sr )
	{
		long[] num = ParseLineNumbers( sr );
		long R = num[0];
		long K = num[1];
		long N = num[2];
		num = ParseLineNumbers( sr );

		long head = 0;
		long count = 0;
		for( long i=0; i<R; i++) {
			long c = 0;
			for( long j=0; j<N; j++ ) {
				long k = (j+head)%N;
				if (c + num[k] > K) {
					head = k;
					break;
				}
				c += num[k];
				// Console.Write(num[k]);
				// Console.Write(" ");
			}
			// Console.WriteLine();
			count += c;
		}

		return count;
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
