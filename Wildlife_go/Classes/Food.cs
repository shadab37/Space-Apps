using System;
using UIKit;

namespace FoodAcademy_HackNYU
{
	public class Food
	{
		public int quantity { get; set; }
		public string name { get; set; }
		public double calories { get; set; }
		public double fat { get; set; }
		public double carbs { get; set; }
		public double protein { get; set; }
		public double fiber { get; set; }
		public UIImage image { get; set; }
		public string servingMeasure { get; set; }
		public int serving { get; set; }

	}
}
