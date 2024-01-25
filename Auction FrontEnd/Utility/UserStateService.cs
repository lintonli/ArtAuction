namespace Auction_FrontEnd.Utility
{
    public class UserStateService
    {
        public bool IsLanding {  get;  private set; }
        public event Action OnChange;

        public void IsLandingPage()
        {
            IsLanding = true;
            NotifyStateChanged();
        }

        public void IsNotLanding()
        {
            IsLanding = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
