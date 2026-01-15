namespace Jtbd.Webapp.Service
{
    public class UIStateService
    {
        public int SidebarWidth { get; set; } = 250; // Valor inicial

        public event Action? OnChange;

        public void SetWidth(int newWidth)
        {
            SidebarWidth = newWidth;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
