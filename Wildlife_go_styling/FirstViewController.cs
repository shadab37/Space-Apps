
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Plugin.Media;
using System.Collections.Generic;


using Newtonsoft.Json.Linq;


using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using UIKit;

using CoreGraphics;

namespace FoodAcademy_HackNYU
{
	public partial class FirstViewController : UIViewController
	{


		public UIImagePickerController imagePicker = new UIImagePickerController();
		public UIImagePickerController takeImagePicker = new UIImagePickerController();

		protected UIImage defaultPic = new UIImage(); 



		protected Food food = new Food();


		protected LinkedList<Food> foodCollection = new LinkedList<Food>();


		//partial void SelectButtonClick(UIButton sender)
		//{
		//	selectImage();
		//}



		async void selectImage()
		{
			var selectedImage = await CrossMedia.Current.PickPhotoAsync();
			SelectedPictureImageView.Image = new UIImage(NSData.FromStream(selectedImage.GetStream()));
			SelectedPictureImageView.Image = MaxResizeImage(SelectedPictureImageView.Image, 1024, 768);
			await analyseImage(SelectedPictureImageView.Image.AsJPEG().AsStream());

			food.image = SelectedPictureImageView.Image;

		}

		async Task analyseImage(Stream imageStream)
		{
			try
			{
				VisionServiceClient visionClient = new VisionServiceClient("c19d4b8bb6c242ea99a8a998195a24f0");
				VisualFeature[] features = { VisualFeature.Tags, VisualFeature.Categories, VisualFeature.Description };
				var analysisResult = await visionClient.AnalyzeImageAsync(imageStream, features.ToList(), null);

				Tag[] list = analysisResult.Tags.ToArray();


				Console.Out.WriteLine("Tags:\n");
				foreach (Tag t in list)
				{
					Console.Out.WriteLine(t.Name);
					Console.Out.WriteLine(t.Confidence);
				}
				Console.Out.WriteLine("Cats:\n");
				foreach (Category c in analysisResult.Categories.ToArray())
				{
					Console.Out.WriteLine(c.Name);
					Console.Out.WriteLine(c.Score);

				}
				AnalysisLabel.Text = string.Empty;

				analysisResult.Description.Tags.ToList().ForEach(tag => AnalysisLabel.Text = AnalysisLabel.Text + tag + "\n");



				// .ForEach(tag => AnalysisLabel.Text = AnalysisLabel.Text + tag + "\n");
				string foodName = analysisResult.Description.Tags.ToList()[0];



				//getFood getfood = new getFood();




				//string json = @"
				//    {
				//        food: [

				//            {
				//                name: ""fries"",
				//                fat: 15.5;,
				//                fiber: 0.5,
				//                protein: 4.3,
				//                calories: 298
				//            }
				//        ]
				//    }";


				string json = nutritionixController.getNutritionFacts(foodName);

				Console.Out.WriteLine(json);


				string matchIdToFind = foodName;
				JObject jo = JObject.Parse(json);

				JObject match = jo["foods"].Values<JObject>()
					.Where(m => m["food_name"].Value<string>() == matchIdToFind)
					.FirstOrDefault();




				food.name = match.GetValue("food_name").ToString();
				food.calories = double.Parse(match.GetValue("nf_calories").ToString(), System.Globalization.CultureInfo.InvariantCulture);
				food.protein = double.Parse(match.GetValue("nf_protein").ToString(), System.Globalization.CultureInfo.InvariantCulture);
				food.carbs = double.Parse(match.GetValue("nf_total_fat").ToString(), System.Globalization.CultureInfo.InvariantCulture);
				food.fat = double.Parse(match.GetValue("nf_total_carbohydrate").ToString(), System.Globalization.CultureInfo.InvariantCulture);






				//food = getFood.chooseFood(foodName);
				caloriesText.Text = food.calories.ToString();
				fatText.Text = food.fat.ToString();
				carbsText.Text = food.carbs.ToString();
				proteinText.Text = food.protein.ToString();

				food.quantity++;

				quantityLabel.Text = food.quantity.ToString();


			
			}
			catch (Microsoft.ProjectOxford.Vision.ClientException ex)
			{
				AnalysisLabel.Text = ex.Error.Message;
			}
		}


		protected FirstViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}


		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			defaultPic = SelectedPictureImageView.Image;

			//SelectedPictureImageView
			UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(actionSheet);
			SelectedPictureImageView.AddGestureRecognizer(tapGesture);
			//SelectedPictureImageView.Image = food.image;
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			food.quantity = 0;

			quantityLabel.Text = food.quantity.ToString();


			//takePictureView.Layer.CornerRadius = takePictureView.Frame.Size.Width / 2;
			//takePictureView.ClipsToBounds = true;

		}





		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}



		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{


			// determine what was selected, video or image
			bool isImage = false;
			switch (e.Info[UIImagePickerController.MediaType].ToString())
			{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;
				case "public.video":
					Console.WriteLine("Video selected");
					break;
			}

			// get common info (shared between images and video)
			NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
			if (referenceURL != null)
				Console.WriteLine("Url:" + referenceURL.ToString());

			// if it was an image, get the other image info
			if (isImage)
			{
				// get the original image
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;

				if (originalImage != null)
				{
					// do something with the image
					MaxResizeImage(originalImage, 8, 8); // resize image
					Console.WriteLine("got the original image");

					//profileView.Image = null;
					food.image = MaxResizeImage(originalImage, 1024, 768); // display



				}
			}
			else { // if it's a video
				   // get video url
				NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
				if (mediaURL != null)
				{
					Console.WriteLine(mediaURL.ToString());
				}
			}

			imagePicker.DismissModalViewController(true);
		}



		void Handle_Canceled(object sender, EventArgs e)
		{

			imagePicker.DismissModalViewController(true);

		}



		async void takePicture()
		{

			await CrossMedia.Current.Initialize();
			var selectedImage = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

		


			SelectedPictureImageView.Image = new UIImage(NSData.FromStream(selectedImage.GetStream()));
			 

			SelectedPictureImageView.Image = MaxResizeImage(SelectedPictureImageView.Image, 1024, 768);


			await analyseImage(SelectedPictureImageView.Image.AsJPEG().AsStream());

		}

		protected void Camera_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;

			if (originalImage != null)
			{
				// do something with the image
				Console.WriteLine("got the original image");
				// resize image




				food.image = MaxResizeImage(originalImage, 1024, 768); // display
			}
			takeImagePicker.DismissModalViewController(true);
		}

		void Camera_Canceled(object sender, EventArgs e)
		{
			//var imagePicker = new UIImagePickerController();
			takeImagePicker.DismissModalViewController(true);
		}



		public UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
		{


			Console.WriteLine("Re Size original image");


			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Min(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1) return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContext(new CGSize(width, height));
			sourceImage.Draw(new CGRect(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}


		//actionSheet
		public void actionSheet()
		{


			//Action Sheet
			UIAlertController actionSheetAlert = UIAlertController.Create("Change Picture", "Select an item from below", UIAlertControllerStyle.ActionSheet);
			actionSheetAlert.AddAction(UIAlertAction.Create("Add Photo", UIAlertActionStyle.Default, (action) => selectImage()));
			actionSheetAlert.AddAction(UIAlertAction.Create("Take Picture", UIAlertActionStyle.Default, (action) => takePicture()));
			actionSheetAlert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, (action) => Console.WriteLine("Cancel button pressed.")));

			UIPopoverPresentationController presentationPopover = actionSheetAlert.PopoverPresentationController;
			if (presentationPopover != null)
			{
				presentationPopover.SourceView = this.View;
				presentationPopover.PermittedArrowDirections = UIPopoverArrowDirection.Up;
			}

			this.PresentViewController(actionSheetAlert, true, null);
		}




	

		partial void IncreaseButton_TouchUpInside(UIButton sender)
		{
			increaseQuant();
		}

		async void increaseQuant()
		{
			if (food.name != null)
			{
				caloriesText.Text = (double.Parse(caloriesText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) + food.calories).ToString();
				fatText.Text = (double.Parse(fatText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) + food.fat).ToString();
				proteinText.Text = (double.Parse(proteinText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) + food.protein).ToString();
				carbsText.Text = (double.Parse(carbsText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) + food.carbs).ToString();
			

				food.quantity++;



				quantityLabel.Text = food.quantity.ToString();
			}

		}

		partial void DecreaseButton_TouchUpInside(UIButton sender)
		{
			decreaseQuant();
		}

		async void decreaseQuant()
		{

			if (food.name != null && int.Parse(quantityLabel.Text.ToString()) > 1 )
			{
				caloriesText.Text = (double.Parse(caloriesText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) - food.calories).ToString();
				fatText.Text = (double.Parse(fatText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) - food.fat).ToString();
				proteinText.Text = (double.Parse(proteinText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) - food.protein).ToString();
				carbsText.Text = (double.Parse(carbsText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture) - food.carbs).ToString();


				food.quantity--;
				quantityLabel.Text = food.quantity.ToString();
			}

		}

		partial void FinishButton_TouchUpInside(UIButton sender)
		{
			
		}







		partial void AddMoreButton_TouchUpInside(UIButton sender)
		{
			



			Food thisFood = new Food();

			thisFood = food;

			thisFood.calories = double.Parse(caloriesText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
			thisFood.fat = double.Parse(fatText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
			//thisFood = double.Parse(fiberText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);
			thisFood.protein = double.Parse(proteinText.Text.ToString(), System.Globalization.CultureInfo.InvariantCulture);

			Console.Out.WriteLine(food.name); 

			foodCollection.AddLast(thisFood);



			resetFood();


		}

		async void resetFood()
		{
			food.name = null;
			food.calories = 0;
			food.fat = 0;
			food.image = null;
			food.protein = 0;
			food.quantity = 0;
			food.carbs = 0;

			quantityLabel.Text = food.quantity.ToString();
			caloriesText.Text = food.calories.ToString();
			fatText.Text = food.fat.ToString();
			proteinText.Text = food.protein.ToString();
			carbsText.Text = food.carbs.ToString();


			SelectedPictureImageView.Image = defaultPic;

		

	

		}



		partial void MealTypeButton_TouchUpInside(UIButton sender)
		{
			throw new NotImplementedException();
		}
	}
}
