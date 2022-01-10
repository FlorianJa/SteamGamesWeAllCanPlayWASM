using Steam.Models.SteamCommunity;

namespace SteamGamesWeAllCanPlayWASM.Client
{
    class OwnedGameModelComparer : IEqualityComparer<OwnedGameModel>
    {
        public bool Equals(OwnedGameModel x, OwnedGameModel y)
        {
            return x.AppId == y.AppId;
        }
        public int GetHashCode(OwnedGameModel other)
        {
            return other.AppId.GetHashCode();
        }
}
}
