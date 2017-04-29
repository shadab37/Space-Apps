using System;
using System.Net;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FoodAcademy_HackNYU
{
	public class nutritionixController
	{

		public static string getNutritionFacts(string foodName)
		{

			Console.Out.WriteLine("Get Nutrition Facts");

			string nutritionFactsStr = "";

			//var request = HttpWebRequest.Create(@"https://trackapi.nutritionix.com/v2/natural/nutrients");

			//request.Method = "POST";
			//request.Headers.Set("x-app-id", "fdb1ec50");
			//request.Headers.Set("x-app-key", "ce5763eaf73879292142ecb3838bff94");
			////request.Headers.Set("Content-Type", "application/json");

			//string postData = "{\n \"query\":\"fries\",\n \"timezone\": \"US/Eastern\"\n}";

			// Create a request using a URL that can receive a post.   
			WebRequest request = WebRequest.Create("https://trackapi.nutritionix.com/v2/natural/nutrients");
			// Set the Method property of the request to POST.  
			request.Method = "POST";

			request.Headers.Set("x-app-id", "fdb1ec50");
			request.Headers.Set("x-app-key", "ce5763eaf73879292142ecb3838bff94");
			request.ContentType = "application/json";

			Console.Out.WriteLine("Get Nutrition Facts");

			// Create POST data and convert it to a byte array.  
			string postData = "{\n \"query\":\"" + foodName + "\",\n \"timezone\": \"US/Eastern\"\n}";


			Console.Out.WriteLine(postData);

			byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(postData);
		 
			request.ContentLength = byteArray.Length;
			// Get the request stream.  
			Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.  
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.  
			dataStream.Close();
			// Get the response.  
			WebResponse response = request.GetResponse();
			// Display the status.  
			Console.WriteLine(((HttpWebResponse)response).StatusDescription);
			// Get the stream containing content returned by the server.  
			dataStream = response.GetResponseStream();
			// Open the stream using a StreamReader for easy access.  
			StreamReader reader = new StreamReader(dataStream);
			// Read the content.  
			string responseFromServer = reader.ReadToEnd();
			// Display the content.  
			//Console.WriteLine(responseFromServer);
			// Clean up the streams.  
			reader.Close();
			dataStream.Close();
			response.Close();


			return responseFromServer.ToString();

		

			//using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			//{
			//	if (response.StatusCode != HttpStatusCode.OK)
			//	{
			//		Console.Out.WriteLine("request fail");
			//		return nutritionFactsStr;
			//	}
			//	else {
			//		using (StreamReader reader = new StreamReader(response.GetResponseStream()))
			//		{
			//			var content = reader.ReadToEnd();
			//			if (string.IsNullOrWhiteSpace(content))
			//			{
			//				Console.Out.WriteLine("Response contained empty body...");
			//				return nutritionFactsStr;
			//			}
			//			else {
			//				Console.Out.WriteLine("Response Body: \r\n {0}", content);


			//				nutritionFactsStr = content;

			//				return nutritionFactsStr;


			//			}

			//		}

			//		//Assert.NotNull(content);
			//	}

			//}



		}
}
}
