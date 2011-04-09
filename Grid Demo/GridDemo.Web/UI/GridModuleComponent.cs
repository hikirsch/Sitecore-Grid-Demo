using System;
using Sitecore.Data.Items;
using Sitecore.Web.UI.WebControls;

namespace GridDemo.Web.UI
{
	/// <summary>
	/// A GridModuleComponent is a Module component for this custom Grid Layout. It exposes access to the ModuleItem
	/// for you easy access.
	/// </summary>
	public class GridModuleComponent : System.Web.UI.UserControl
	{
		/// <summary>
		/// Exception error message 
		/// </summary>
		private const string SUBLAYOUT_IS_NULL_EXCEPTION_MESSAGE = "You are trying to use a GridComponent on something that's not a Sublayout.";

		/// <summary>
		/// The Module item is the Sitecore Item that is the 
		/// </summary>
		protected Item ModuleItem { get; set; }

		/// <summary>
		/// When the page inits, we can then derive the actual Sitecore Item being rendered and keep that reference. 
		/// We use this reference to pull the data from Sitecore on our modules.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e)
		{
			// call our base first
			base.OnLoad(e);

			// We need to get the sublayout, it's the Parent item of this UserControl instance
			Sublayout sublayout = Parent as Sublayout;
			
			// if we didn't find the sublayout, then we throw an exception
			if( sublayout == null ) 
				throw new NullReferenceException(SUBLAYOUT_IS_NULL_EXCEPTION_MESSAGE);

			// set our module, you may want to grab some data from it.
			ModuleItem = Sitecore.Context.Database.GetItem(sublayout.DataSource);
		}
	}
}