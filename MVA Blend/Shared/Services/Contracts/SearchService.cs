using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services.Contracts
{
    public sealed class SearchService : IService
    {
        private SearchService() { Setup(); }

        private static SearchService _Instance;
        public static SearchService Instance
        {
            get { return _Instance ?? (_Instance = new SearchService()); }
        }

        public void Setup()
        {
            Cleanup();
            var s = Windows.ApplicationModel.Search.SearchPane.GetForCurrentView();
            s.QueryChanged += s_QueryChanged;
            s.QuerySubmitted += s_QuerySubmitted;
            s.ResultSuggestionChosen += s_ResultSuggestionChosen;
            s.SuggestionsRequested += s_SuggestionsRequested;
            s.VisibilityChanged += s_VisibilityChanged;
        }

        public void Cleanup()
        {
            var s = Windows.ApplicationModel.Search.SearchPane.GetForCurrentView();
            s.QueryChanged += s_QueryChanged;
            s.QuerySubmitted += s_QuerySubmitted;
            s.ResultSuggestionChosen += s_ResultSuggestionChosen;
            s.SuggestionsRequested += s_SuggestionsRequested;
            s.VisibilityChanged += s_VisibilityChanged;
        }

        public Action<Windows.ApplicationModel.Search.SearchPaneVisibilityChangedEventArgs> VisibilityChanged { get; set; }
        void s_VisibilityChanged(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneVisibilityChangedEventArgs args)
        {
            try { VisibilityChanged(args); }
            catch { }
        }

        public Action<Windows.ApplicationModel.Search.SearchPaneSuggestionsRequestedEventArgs> SuggestionsRequested { get; set; }
        void s_SuggestionsRequested(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneSuggestionsRequestedEventArgs args)
        {
            try { SuggestionsRequested(args); }
            catch { }
        }

        public Action<Windows.ApplicationModel.Search.SearchPaneResultSuggestionChosenEventArgs> ResultSuggestionChosen { get; set; }
        void s_ResultSuggestionChosen(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneResultSuggestionChosenEventArgs args)
        {
            try { ResultSuggestionChosen(args); }
            catch { }
        }

        public Action<Windows.ApplicationModel.Search.SearchPaneQuerySubmittedEventArgs> QuerySubmitted { get; set; }
        void s_QuerySubmitted(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneQuerySubmittedEventArgs args)
        {
            try { QuerySubmitted(args); }
            catch { }
        }

        public Action<Windows.ApplicationModel.Search.SearchPaneQueryChangedEventArgs> QueryChanged { get; set; }
        void s_QueryChanged(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneQueryChangedEventArgs args)
        {
            try { QueryChanged(args); }
            catch { }
        }

        public void ShowUI()
        {
            Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().Show();
        }

    }
}
