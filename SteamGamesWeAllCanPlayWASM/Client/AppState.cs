namespace SteamGamesWeAllCanPlay
{
    public class AppState
    {
        public HashSet<string> SelectedFriends { get; private set; } = new HashSet<string>();

        public event Action OnChange;

        public void SelectFriend(string steamId)
        {
            if(!SelectedFriends.Add(steamId))
            {
                SelectedFriends.Remove(steamId);
            }
            Console.WriteLine("SelectedFriends has been changed");
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            Console.WriteLine("NotifyStateChanged has been fired");
            OnChange?.Invoke();
        }
    }
}
