using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Shared.Services
{
    public sealed class SecondaryTileService : IService
    {
        private SecondaryTileService() { Setup(); }

        private static SecondaryTileService _Instance;
        public static SecondaryTileService Instance
        {
            get { return _Instance ?? (_Instance = new SecondaryTileService()); }
        }

        public void Setup() { Cleanup(); }
        public void Cleanup() { }

        public async Task<bool> Pin(TileInfo info)
        {
            System.Diagnostics.Contracts.Contract.Requires(info != null, "TileInfo");
            if (Exists(info))
                return true;

            var tile = new SecondaryTile()
            {
                TileId = info.TileId,
                DisplayName = info.DisplayName,
                Arguments = info.Arguments,
                PhoneticName = info.PhoneticName,
                LockScreenDisplayBadgeAndTileText = info.LockScreenDisplayBadgeAndTileText,
                LockScreenBadgeLogo = info.LockScreenBadgeLogo,
            };

            tile.VisualElements.BackgroundColor = info.VisualElements.BackgroundColor;
            tile.VisualElements.ForegroundText = info.VisualElements.ForegroundText;
            tile.VisualElements.ShowNameOnSquare150x150Logo = info.VisualElements.ShowNameOnSquare150x150Logo;
            tile.VisualElements.ShowNameOnSquare310x310Logo = info.VisualElements.ShowNameOnSquare310x310Logo;
            tile.VisualElements.ShowNameOnWide310x150Logo = info.VisualElements.ShowNameOnWide310x150Logo;
            tile.VisualElements.Square150x150Logo = info.VisualElements.Square150x150Logo;
            tile.VisualElements.Square30x30Logo = info.VisualElements.Square30x30Logo;
            tile.VisualElements.Square310x310Logo = info.VisualElements.Square310x310Logo;
            tile.VisualElements.Square70x70Logo = info.VisualElements.Square70x70Logo;
            tile.VisualElements.Wide310x150Logo = info.VisualElements.Wide310x150Logo;

            var result = await tile.RequestCreateForSelectionAsync(info.Rect(), info.RequestPlacement);
            return result;
        }

        public bool Exists(TileInfo info)
        {
            return SecondaryTile.Exists(info.TileId);
        }

        public async Task<bool> Unpin(TileInfo info)
        {
            System.Diagnostics.Contracts.Contract.Requires(info != null, "TileInfo");
            if (!SecondaryTile.Exists(info.TileId))
                return true;
            var tile = new SecondaryTile(info.TileId);
            var result = await tile.RequestDeleteForSelectionAsync(info.Rect(), info.RequestPlacement);
            return result;
        }

        public class TileInfo
        {
            public FrameworkElement AnchorElement { get; set; }
            public Windows.UI.Popups.Placement RequestPlacement { get; set; }

            public string TileId { get; set; }
            public string Arguments { get; set; }
            public string DisplayName { get; set; }
            public string PhoneticName { get; set; }
            public bool LockScreenDisplayBadgeAndTileText { get; set; }
            public Uri LockScreenBadgeLogo { get; set; }

            public TileVisualElements VisualElements = new TileVisualElements();
            public class TileVisualElements
            {
                public Windows.UI.Color BackgroundColor { get; set; }
                public ForegroundText ForegroundText { get; set; }
                public bool ShowNameOnSquare150x150Logo { get; set; }
                public bool ShowNameOnSquare310x310Logo { get; set; }
                public bool ShowNameOnWide310x150Logo { get; set; }
                public Uri Square150x150Logo { get; set; }
                public Uri Square30x30Logo { get; set; }
                public Uri Square310x310Logo { get; set; }
                public Uri Square70x70Logo { get; set; }
                public Uri Wide310x150Logo { get; set; }
            }

            public Rect Rect()
            {
                if (this.AnchorElement == null)
                    return new Rect();
                var transform = this.AnchorElement.TransformToVisual(null);
                var point = transform.TransformPoint(new Point());
                var size = new Size(this.AnchorElement.ActualWidth, this.AnchorElement.ActualHeight);
                return new Rect(point, size);
            }
        }
    }
}
