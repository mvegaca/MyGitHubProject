﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using MyGitHubProject.Helpers;
using MyGitHubProject.Models;
using MyGitHubProject.Services;

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace MyGitHubProject.ViewModels
{
    public class ImageGalleryDetailViewModel : Observable
    {
        private static UIElement _image;
        private object _selectedImage;
        private ObservableCollection<SampleImage> _source;

        public object SelectedImage
        {
            get => _selectedImage;
            set
            {
                Set(ref _selectedImage, value);
                ImagesNavigationHelper.UpdateImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey, ((SampleImage)SelectedImage).ID);
            }
        }

        public ObservableCollection<SampleImage> Source
        {
            get => _source;
            set => Set(ref _source, value);
        }

        public ImageGalleryDetailViewModel()
        {
            // TODO WTS: Replace this with your actual data
            Source = SampleDataService.GetGallerySampleData();
        }

        public void SetImage(UIElement image) => _image = image;

        public void Initialize(string sampleImageId, NavigationMode navigationMode)
        {
            if (!string.IsNullOrEmpty(sampleImageId) && navigationMode == NavigationMode.New)
            {
                SelectedImage = Source.FirstOrDefault(i => i.ID == sampleImageId);
            }
            else
            {
                var selectedImageId = ImagesNavigationHelper.GetImageId(ImageGalleryViewModel.ImageGallerySelectedIdKey);
                if (!string.IsNullOrEmpty(selectedImageId))
                {
                    SelectedImage = Source.FirstOrDefault(i => i.ID == selectedImageId);
                }
            }

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation(ImageGalleryViewModel.ImageGalleryAnimationOpen);
            animation?.TryStart(_image);
        }

        public void SetAnimation()
        {
            ConnectedAnimationService.GetForCurrentView()?.PrepareToAnimate(ImageGalleryViewModel.ImageGalleryAnimationClose, _image);
        }
    }
}
