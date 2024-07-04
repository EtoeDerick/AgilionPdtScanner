using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgilionPdtScanner.ViewModels;

namespace TestProjectAgilion
{
    public class BaseViewModelTests
    {
        //Check whether when a property changes, a property change event is fired.
        /*SetProperty_Updates_Property_And_Fires_PropertyChanged: 
         * Tests that when Title property is set to a new value, 
         * PropertyChanged event is fired and the property is updated correctly.
         */
        [Fact]
        public void SetProperty_Updates_Property_And_Fires_PropertyChanged()
        {
            // Arrange
            var viewModel = new BaseViewModel();
            bool propertyChangedFired = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Title))
                {
                    propertyChangedFired = true;
                }
            };

            // Act
            viewModel.Title = "New Title";

            // Assert
            Assert.True(propertyChangedFired);
            Assert.Equal("New Title", viewModel.Title);
        }

        /*SetProperty_Does_Not_Fire_PropertyChanged_If_Value_Is_Same: 
         * Ensures that setting the same value to Title property does not fire PropertyChanged event.         
         */
        [Fact]
        public void SetProperty_Does_Not_Fire_PropertyChanged_If_Value_Is_Same()
        {
            // Arrange
            var viewModel = new BaseViewModel();
            viewModel.Title = "Initial Title";
            bool propertyChangedFired = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.Title))
                {
                    propertyChangedFired = true;
                }
            };

            // Act
            viewModel.Title = "Initial Title"; // Same value

            // Assert
            Assert.False(propertyChangedFired);
        }
        /*IsBusy_Default_Value_Is_False: 
         * Checks that the default value of IsBusy property is false
         */
        [Fact]
        public void IsBusy_Default_Value_Is_False()
        {
            // Arrange
            var viewModel = new BaseViewModel();

            // Assert
            Assert.False(viewModel.IsBusy);
        }

        /*Set_IsBusy_Property: 
         * Tests that setting IsBusy property to true fires PropertyChanged event and updates the property correctly.
         */
        [Fact]
        public void Set_IsBusy_Property()
        {
            // Arrange
            var viewModel = new BaseViewModel();
            bool propertyChangedFired = false;
            viewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(viewModel.IsBusy))
                {
                    propertyChangedFired = true;
                }
            };

            // Act
            viewModel.IsBusy = true;

            // Assert
            Assert.True(propertyChangedFired);
            Assert.True(viewModel.IsBusy);
        }
    }
}
