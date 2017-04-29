using System;
namespace FoodAcademy_HackNYU
{
	public class people
	{
		private string gender;
		private string name;
		private int age;
		private int weight;  //公斤
		private int height;
		private int excerciseLevel;
		public people(string gender,string name, int age,int weight,int height,int excerciseLevel)
		{
			this.name = name;
			this.age = age;
			this.gender = gender;
			this.height = height;
			this.weight = weight;
			this.excerciseLevel = excerciseLevel;
		}
		public int requiredCaloriesTakenInForAnAdultPerDay() //only for adult
		{
			int result=0;
			if (gender == "male")
			{  if(18 <=age&&age<30 )
				switch (excerciseLevel)
					{
					case 1: result = (int)(1.1 * (15.2 * weight + 680 + 95 * 24));
							break;
					case 2: result = (int)(1.1 * (15.2 * weight + 680 + 120 * 24));
							break;
					case 3:result = (int)(1.1 * (15.2 * weight + 680 + 170 * 24));
							break;
						default:break;
					}
				if(30<=age&&age<60)
					switch (excerciseLevel)
					{
						case 1:
						result = (int)(1.1 * (11.5 * weight + 830 + 95 * 24));
							break;
						case 2:
						result = (int)(1.1 * (11.5 * weight + 830 + 120 * 24));
							break;
						case 3:
						result = (int)(1.1 * (11.5 * weight + 830 + 170 * 24));
							break;
						default: break;
					}
				if (age >= 60)
					switch (excerciseLevel)
					{
						case 1:
							result = (int)(1.1 * (13.4 * weight + 490 + 95 * 24));
							break;
						case 2:
						result = (int)(1.1 * (13.4 * weight + 490 + 120 * 24));
							break;
						case 3:
						result = (int)(1.1 * (13.4 * weight + 490 + 170 * 24));
							break;
						default: break;
					}
			}
			else
			{
				if (18 <= age && age < 30)
					switch (excerciseLevel)
					{
						case 1:
							result = (int)(1.1 * (14.6 * weight + 450 + 95 * 24));
							break;
						case 2:
							result = (int)(1.1 * (14.6 * weight + 450 + 120 * 24));
							break;
						case 3:
							result = (int)(1.1 * (14.6 * weight + 450 + 170 * 24));
							break;
						default: break;
					}
				if (30 <= age && age < 60)
					switch (excerciseLevel)
					{
						case 1:
							result = (int)(1.1 * (8.6 * weight + 830 + 95 * 24));
							break;
						case 2:
							result = (int)(1.1 * (8.6 * weight + 830 + 120 * 24));
							break;
						case 3:
							result = (int)(1.1 * (8.6 * weight + 830 + 170 * 24));
							break;
						default: break;
					}
				if (age >= 60)
					switch (excerciseLevel)
					{
						case 1:
							result = (int)(1.1 * (10.4 * weight + 600 + 95 * 24));
							break;
						case 2:
							result = (int)(1.1 * (10.4 * weight + 600 + 120 * 24));
							break;
						case 3:
							result = (int)(1.1 * (10.4 * weight + 600 + 170 * 24));
							break;
						default: break;
					}
			}
			return result;


		}

		public void setExcerciseLevel(int level) {
			excerciseLevel = level;
		
		}

		public void setName(string newName)
		{

			 name=newName;
		}
		public void setGender(string newGender)
		{

			 gender=newGender;
		}

		public void setWeight(int newWeight)
		{

			weight=newWeight;
		}
		public void setHeight(int newHeight)
		{

			height=newHeight;
		}
		public void setAge(int newAge)
		{
			 age=newAge;
		}





		public string getName() {

			return name;
		}
		public string getGender()
		{

			return gender;
		}

		public int getWeight()
		{

			return weight;
		}
		public int getHeight()
		{

			return height;
		}
		public int getAge() {
			return age;
		}
	}
}
