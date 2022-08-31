namespace AdvertismentTask.Models
{
    public class SortViewModel
    {
        public SortState TitleSort { get; } // значение для сортировки по заголовку
        public SortState TextSort { get; }    // значение для сортировки по длинне текста
        public SortState DateSort { get; }   // значение для сортировки по дате
        public SortState AvailableSort { get; } // значение для сортировки по доступности 
        public SortState Current { get; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            TextSort = sortOrder == SortState.TextAsc ? SortState.TextDesc : SortState.TextAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            AvailableSort = sortOrder == SortState.AvailableAsc ? SortState.AvailableDesc : SortState.AvailableAsc;
            Current = sortOrder;
        }
    }
}