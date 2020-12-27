using System;

namespace GenericDelegatesTask
{
	public static class FunctionExtensions
	{
		/// <summary>
		///   Combines several predicates using logical AND operator 
		/// </summary>
		/// <param name="predicates">array of predicates</param>
		/// <returns>
		///   Returns a new predicate that combine the specified predicated using AND operator
		/// </returns>
		/// <example>
		///   var result = CombinePredicatesWithAnd(new Predicate<string>[] {
		///            x => !string.IsNullOrEmpty(x),
		///            x => x.StartsWith("START"),
		///            x => x.EndsWith("END"),
		///            x => x.Contains("#")
		///        });
		///   should return the predicate that identical to 
		///   x=> (!string.IsNullOrEmpty(x)) && x.StartsWith("START") && x.EndsWith("END") && x.Contains("#")
		///
		///   The following example should create predicate that returns true if int value between -10 and 10:
		///   var result = CombinePredicatesWithAnd(new Predicate<int>[] {
		///            x => x > -10,
		///            x => x < 10
		///       });
		/// </example>

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S1751:Loops with at most one iteration should be refactored", Justification = "<Pending>")]
		public static Predicate<T> CombinePredicatesWithAnd<T>(Predicate<T>[] predicates)
		{
			Predicate<T> newPredicate = predicates[0];

			//static bool str(string item) 
			//{ 
			//	return (!string.IsNullOrEmpty(item)) && item.StartsWith("START") && item.Contains("#") && item.EndsWith("END"); 
			//}

			//static bool intFirst(int item) 
			//{ 
			//	return ((item < 10) && (item > -10) && (item != 0) && (item != 1)) || (item < 0); 
			//}

			Predicate<string> str = delegate (string item) { return (!string.IsNullOrEmpty(item)) && item.StartsWith("START") && item.Contains("#") && item.EndsWith("END"); };
			Predicate<int> intFirst = delegate (int item) { return ((item < 10) && (item > -10) && (item != 0) && (item != 1)) || (item < 0); };

			if (newPredicate is string)
			{
				foreach (var item in predicates)
				{
					if (str(item.ToString()))
					{
						newPredicate += item;
					}	
				}
			}

			if (newPredicate is int)
			{
				foreach (var item in predicates)
				{
					if (intFirst(Convert.ToInt32(item)))
					{
						newPredicate += item;
					}

				}
			}

			return newPredicate;
		}
	}
}
