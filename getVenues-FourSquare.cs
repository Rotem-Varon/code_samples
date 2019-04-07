/// <summary>
/// This method gets the Venues for the provided postal code within the specified radius and category.
/// The default value for intent (checkin) is being used.
/// Upper bound for radius and limit is not checked as it controlled by the API
/// </summary>
/// <param name="postalCode"> The postal code that the radius will be measured from </param>
/// <param name="category"> The category. The default is Food </param>
/// <param name="radius"> The radius in meters. Up to 100,000 with a default of 1000</param>
/// <param name="limit"> Number of records to return. Up to 50 with a default of 30</param>
/// <returns>In case of success, a list of venues. Otherwise null</returns>
/// <exception cref="ArgumentException">This method can throw an ArgumentException upon invalid input or propagating exception from SharpSquare</exception>
public List<FourSquareEntityItems<Venue>> getVenues(string postalCode, string category = "Food", int radius = 1000, int limit = 30)
{
 // basic input validation
 if (string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(category) ||
     radius < 1 || limit < 1)
 {
  log.Error("Invalid input");
  throw new ArgumentException(FourSquare.Data.Properties.Resources.Invalid_Input_en);
 }

 // let's build the query
 Dictionary<string, string> parameters = new Dictionary<string, string>();
 parameters.Add("near", postalCode);
 parameters.Add("radius", radius.ToString());
 parameters.Add("limit", limit.ToString());

 //  The same can be achieved by querying for all the categories:
 //  List<Category> c = sharpSquare.GetVenueCategories();
 //  taking all the sub-categories of 'category' and filtering results by that
 parameters.Add("cat", category);
 log.Info(string.Format("FourSquare query with: Postal Code [{0}], Radius [{1}], Limit [{2}], Category [{3}]", 
   postalCode, radius.ToString(), limit.ToString(), category));
 // Execute the query
 return SharpSquare.SearchVenues(parameters);
}