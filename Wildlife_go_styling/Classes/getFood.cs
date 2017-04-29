using System;
namespace FoodAcademy_HackNYU
{
	public class getFood
	{

	


		public static Food chooseFood(string name)
		{


			Food food = new Food();


			if (name == "Strawberry") {
				food.name = "Strawberrry";
				food.fat = 0.3;
					food.fiber = 2.0;
				food.protein =0.67;
				food.calories = 32.0;  //every 100g strawberry contains
				
			}
			if (name == "Pizza")
			{
				food.name = "Pizza";
				food.fat = 13.07;
				food.fiber = 1.5;
				food.protein = 11.47;
				food.calories = 291;

			}
			if (name == "Hamburger")
			{

				food.name = "Hamburger";
				food.fat = 15.79;
				food.fiber = 1.5;
				food.protein = 22.81;
				food.calories = 456;
			}
			if (name == "banana")
			{

				food.name = "banana";
				food.fat = 0.33;
				food.fiber = 2.6;
				food.protein = 1.09;
				food.calories = 89;
				food.servingMeasure = "";
			}
			if (name == "fries")
			{

				food.name = "fries";
				food.fat = 15.5;
				food.fiber = 0.5;
				food.protein = 4.3;
				food.calories = 298;
			}
			if (name == "friedChikenWings")
			{

				food.name = "fries";
				food.fat = 21.06;
				food.fiber = 0.6;
				food.protein = 21.34;
				food.calories = 319;
			}
			if (name == "Cola")
			{

				food.name = "Cola";
				food.fat = 0.0;
				food.fiber = 0.0;
				food.protein = 0.0;
				food.calories = 41;
			}


			return food;
		}
	}
}

