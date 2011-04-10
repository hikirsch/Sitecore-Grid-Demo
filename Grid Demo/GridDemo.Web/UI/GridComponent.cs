using System;
using System.Linq;
using System.Web.UI.WebControls;
using Sitecore.Data.Items;
using Sitecore.Layouts;
using Sitecore.Web.UI.WebControls;

namespace GridDemo.Web.UI
{
	/// <summary>
	/// A grid component is a piece of the grid where it parses all child items including this one and appends
	/// it to the GridPlacHolder.
	/// </summary>
	public partial class GridComponent : System.Web.UI.UserControl
	{
		/// <summary>
		/// This is the ID of the asp:PlaceHolder that's on each of the 
		/// </summary>
		private const string GRID_PLACERHOLDER_ID = "GridPlaceHolder";

		/// <summary>
		/// The exception message when we were unable to find either the Data Item assoicated with the Sitecore 
		/// sublayout, or the Page Meta-Data folder as a child.
		/// </summary>
		private const string SUBLAYOUT_IS_NULL_EXCEPTION_MESSAGE = "We couldn't find another data item linked to this item or the '{0}' Item name as a child to this page.";

		/// <summary>
		/// This constant represents the name of the item that holds all the rows for the grid, this is the start point 
		/// of where the grid starts rendering.
		/// </summary>
		private const string PAGE_META_DATA_FOLDER_NAME = "Page Meta-Data";

		/// <summary>
		/// This is the actual Item of the Grid component we want to render.
		/// </summary>
		protected Item DataItem { get; set; }

		/// <summary>
		/// When we initialize the page, we essentially need to grab this item's presentation and model layers.
		/// </summary>
		/// <param name="e">Event Args</param>
		/// <exception cref="NullReferenceException">Error is thrown when the Page Meta-Data item couldn't be found.</exception>
		protected override void OnInit(EventArgs e)
		{
			// call our base first
			base.OnInit(e);

			// We need to get the sublayout, it's the Parent item of this UserControl instance
			Sublayout sublayout = Parent as Sublayout;

			// if this isn't a Sublayout, we don't do anything, something probably happened that shouldn't.
			if (sublayout == null) 
				throw new NullReferenceException(string.Format(SUBLAYOUT_IS_NULL_EXCEPTION_MESSAGE, PAGE_META_DATA_FOLDER_NAME));

			// Get our data item from the sublayout.
			DataItem = GetDataItemFromSublayout(sublayout);
		}

		/// <summary>
		/// When we are ready to render the page, we want to 
		/// </summary>
		/// <param name="e">Event Args</param>
		protected override void OnPreRender(EventArgs e)
		{
			// call our base first
			base.OnPreRender(e);

			// if we have a source item, that means we got the actual item that this should be rendering
			if (DataItem == null) return;

			// we need to find the GridPlaceholder so we can put stuff into it
			PlaceHolder gridPlaceHolder = FindControl(GRID_PLACERHOLDER_ID) as PlaceHolder;

			// if we found the grid placeholder then we using the place holder to recursively
			// add all the child items and render them into this placeholder
			if (gridPlaceHolder != null)
			{
				RenderChildItems(DataItem, gridPlaceHolder);
			}
		}

		/// <summary>
		/// Using the sublayout, we can pull the actual data Item that we need to render.
		/// </summary>
		private static Item GetDataItemFromSublayout(Sublayout sublayout)
		{
			// if there's a Data Source, then that's the item we want,
			// otherwise it's most likely the Page Meta-Data folder we need to start rendering recursively anyway
			return !String.IsNullOrEmpty(sublayout.DataSource)
			       	? Sitecore.Context.Database
			       	  	.GetItem(sublayout.DataSource)
			       	: Sitecore.Context.Item
			       	  	.Children
			       	  	.FirstOrDefault(item => item.Name == PAGE_META_DATA_FOLDER_NAME);
		}

		/// <summary>
		/// This function is responsible for pulling the actual Rendering for current device that's loaded and
		/// essentially 
		/// </summary>
		private void RenderChildItems(Item rootItem, PlaceHolder placeholder)
		{
			rootItem.GetChildren()
				.ToList()
				.ForEach(item =>
				{
					// We need to get the path of the actual Sublayout's path
					// First we need to get the device's rendering for this child item (assume the first returned rendering)
					RenderingReference renderingReference = item
						.Visualization
						.GetRenderings(Sitecore.Context.Device, false)
						.First();

					// Now, get the actual sublayout item itself
					Item sublayoutItem = Sitecore.Context.Database
						.GetItem(renderingReference.RenderingID);

					// Create a new sublayout with the sublayout's path and data item path.
					Sublayout sublayout = new Sublayout
					{
						Path = sublayoutItem["Path"],
						DataSource = item.Paths.Path
					};

					// now place this sublayout on the grid.
					placeholder.Controls.Add(sublayout);
				});
		}
	}
}