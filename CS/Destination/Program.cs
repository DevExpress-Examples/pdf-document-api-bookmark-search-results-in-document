using System.Collections.Generic;
using DevExpress.Pdf;

namespace Destination {
    class Program {

        static void Main(string[] args) {
            // Create a PDF document processor.
            using (PdfDocumentProcessor documentProcessor = new PdfDocumentProcessor()) {

                // Define search words.
                string[] words = { "DX-B5000", "DX-RX800" };

                // Load a PDF document. 
                documentProcessor.LoadDocument(@"..\..\Document.pdf");

                // Specify the search parameters.
                PdfTextSearchParameters searchParameters = new PdfTextSearchParameters();
                searchParameters.CaseSensitive = true;
                searchParameters.WholeWords = true;

                // Get bookmark list. 
                IList<PdfBookmark> bookmarks = documentProcessor.Document.Bookmarks;
                foreach (string word in words) {
                    // Get the search results from the FindText method called with search text and search parameters.
                    PdfTextSearchResults results = documentProcessor.FindText(word, searchParameters);

                    // If the desired text is found, create a destination that corresponds to the found text
                    // to be displayed at the upper corner of the window.
                    if (results.Status == PdfTextSearchStatus.Found) {
                        PdfXYZDestination destination = new PdfXYZDestination(results.Page, 0, results.Rectangles[0].Top, null);

                        // Create a bookmark with the search word shown as title and the destination.      
                        PdfBookmark bookmark = new PdfBookmark() { Title = word, Destination = destination };

                        // Add the bookmark to the bookmark list.
                        bookmarks.Add(bookmark);
                    }
                }
                // Save the modified document.
                documentProcessor.SaveDocument(@"..\..\Result.pdf");
            }
        }
    }
}


