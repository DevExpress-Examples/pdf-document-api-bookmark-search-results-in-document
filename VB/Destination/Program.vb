Imports System.Collections.Generic
Imports DevExpress.Pdf

Namespace Destination
    Friend Class Program

        Shared Sub Main(ByVal args() As String)
            ' Create a PDF document processor.
            Using documentProcessor As New PdfDocumentProcessor()

                ' Define search words.
                Dim words() As String = { "DX-B5000", "DX-RX800" }

                ' Load a PDF document. 
                documentProcessor.LoadDocument("..\..\Document.pdf")

                ' Specify the search parameters.
                Dim searchParameters As New PdfTextSearchParameters()
                searchParameters.CaseSensitive = True
                searchParameters.WholeWords = True

                ' Get bookmark list. 
                Dim bookmarks As IList(Of PdfBookmark) = documentProcessor.Document.Bookmarks
                For Each word As String In words
                    ' Get the search results from the FindText method called with search text and search parameters.
                    Dim results As PdfTextSearchResults = documentProcessor.FindText(word, searchParameters)

                    ' If the desired text is found, create a destination that corresponds to the found text
                    ' to be displayed at the upper corner of the window.
                    If results.Status = PdfTextSearchStatus.Found Then
                        Dim destination As New PdfXYZDestination(results.Page, 0, results.Rectangles(0).Top, Nothing)

                        ' Create a bookmark with the search word shown as title and the destination.      
                        Dim bookmark As New PdfBookmark() With {.Title = word, .Destination = destination}

                        ' Add the bookmark to the bookmark list.
                        bookmarks.Add(bookmark)
                    End If
                Next word
                ' Save the modified document.
                documentProcessor.SaveDocument("..\..\Result.pdf")
            End Using
        End Sub
    End Class
End Namespace


