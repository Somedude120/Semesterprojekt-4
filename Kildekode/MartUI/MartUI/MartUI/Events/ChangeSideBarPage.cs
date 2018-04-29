using MartUI.Main;
using Prism.Events;

namespace MartUI.Events
{
    public class ChangeSideBarPage : PubSubEvent<IViewModel>
    {

    }

    //public class ChangePageDTO
    //{
    //    public IViewModel Model { get; }
    //    public string ContentRegion { get; }
    //    public ChangePageDTO(IViewModel model, string contentRegion)
    //    {
    //        Model = model;
    //        ContentRegion = contentRegion;
    //    }
    //}
}