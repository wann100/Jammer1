using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Jammer_1.Helpers
{ 
    class wrapLayout:Layout<View>
    {
      
        Dictionary<View, SizeRequest> layoutCache = new Dictionary<View, SizeRequest>();
        public double spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        public BindableProperty SpacingProperty { get; private set; }

        public wrapLayout()
        {
            VerticalOptions = HorizontalOptions = LayoutOptions.FillAndExpand;
        }

        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            throw new NotImplementedException();
        }
    }
}
