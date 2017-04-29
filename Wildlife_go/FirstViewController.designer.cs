// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace FoodAcademy_HackNYU
{
    [Register ("FirstViewController")]
    partial class FirstViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton addMoreButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AnalysisLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel caloriesLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel caloriesText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel carbsLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel carbsText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton decreaseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel fatLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel fatText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton finishButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton IncreaseButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton MealTypeButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel proteinLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel proteinText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel quantityLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView SelectedPictureImageView { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel servingSizeText { get; set; }

        [Action ("AddMoreButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void AddMoreButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("DecreaseButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void DecreaseButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("FinishButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void FinishButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("IncreaseButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void IncreaseButton_TouchUpInside (UIKit.UIButton sender);

        [Action ("MealTypeButton_TouchUpInside:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void MealTypeButton_TouchUpInside (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (addMoreButton != null) {
                addMoreButton.Dispose ();
                addMoreButton = null;
            }

            if (AnalysisLabel != null) {
                AnalysisLabel.Dispose ();
                AnalysisLabel = null;
            }

            if (caloriesLabel != null) {
                caloriesLabel.Dispose ();
                caloriesLabel = null;
            }

            if (caloriesText != null) {
                caloriesText.Dispose ();
                caloriesText = null;
            }

            if (carbsLabel != null) {
                carbsLabel.Dispose ();
                carbsLabel = null;
            }

            if (carbsText != null) {
                carbsText.Dispose ();
                carbsText = null;
            }

            if (decreaseButton != null) {
                decreaseButton.Dispose ();
                decreaseButton = null;
            }

            if (fatLabel != null) {
                fatLabel.Dispose ();
                fatLabel = null;
            }

            if (fatText != null) {
                fatText.Dispose ();
                fatText = null;
            }

            if (finishButton != null) {
                finishButton.Dispose ();
                finishButton = null;
            }

            if (IncreaseButton != null) {
                IncreaseButton.Dispose ();
                IncreaseButton = null;
            }

            if (MealTypeButton != null) {
                MealTypeButton.Dispose ();
                MealTypeButton = null;
            }

            if (proteinLabel != null) {
                proteinLabel.Dispose ();
                proteinLabel = null;
            }

            if (proteinText != null) {
                proteinText.Dispose ();
                proteinText = null;
            }

            if (quantityLabel != null) {
                quantityLabel.Dispose ();
                quantityLabel = null;
            }

            if (SelectedPictureImageView != null) {
                SelectedPictureImageView.Dispose ();
                SelectedPictureImageView = null;
            }

            if (servingSizeText != null) {
                servingSizeText.Dispose ();
                servingSizeText = null;
            }
        }
    }
}