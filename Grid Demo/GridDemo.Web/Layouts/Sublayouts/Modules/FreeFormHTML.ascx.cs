using GridDemo.Web.UI;

namespace GridDemo.Web.Layouts.Sublayouts.Modules
{
	public partial class FreeFormHTML : GridModuleComponent
	{
		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);

			TitleFieldRenderer.Item = FreeFormHTMLRenderer.Item = ModuleItem;
		}
	}
}