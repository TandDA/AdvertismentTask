namespace AdvertismentTask.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Advertisement> Advertisements { get; }
        public PageViewModel PageViewModel { get; }
        public SortViewModel SortViewModel { get; }
        public IndexViewModel(IEnumerable<Advertisement> advs, PageViewModel pageViewModel, SortViewModel sortViewModel)
        {
            Advertisements = advs;
            PageViewModel = pageViewModel;
            SortViewModel = sortViewModel;
        }
    }
}
