Project: Moby Dick
Author: Robert Gerster
Sponsor: Source Allies

About:
The project's main thrust is to capture the text (given) of the book "Moby Dick" written by Herman Melvillem and output the most often 100 words in the book. 
One would expect words like I, a, the etc. to be the most often used words... BUT WAIT! Also given is a file of stop words, or in other words, a file of words 
that are excluded from the 100 words. Once these are filtered off, the expected happened - whale was not excluded and was the most often used word in the book.

Development versions:
Program 1.0 - The framework for reading from a saved file was developed.
Program 1.1 - There is a lot of material about the book project printed in the file that needed to be skipped prior to counting words. Fortunately the last line 
				before Chapter 1 was unique so it served as a stopping point. Should this project be amended to be used by another book, a starting point would 
				need to be fed to the algorithm
Program 1.2 - The code is reading data one line at a time. This version split the line into individual words to be run through the algorithm. The lines were limited 
				to 20 to facilitate testing
Program 1.3 - The words  a, "a, a-, and a. should not be separate words. Each should counted as just an a. This version is the first attempt to trim off punctuation.
Program 1.4 - There were two things going on in this fersion. This is the first version where the uniqueness of a word in the list appears. In addition punctuation
				was once again a problem as the file put no spaces between hyphens.
Program 1.5 - The stop words were added here. First to be read from a file, then to weed out words before they went to the list.
Program 1.6 - At this point the 20 line limit was lifted and a stopping point needed to be found. Gustafson was a strong bet to not appear in the book test, so it
				the winner. Again for another book an exit point would need to be fed. The code is getting unweildy at this point and needed cleaning. I made the
				hyphen check a function.
Program 1.7 - Here the odd punctuation of Melville's day continues to byte me (pun intended) I found there were words with 3 hyphens. The hyphen catchwas expanded to 
				recursively check, and it worked like a dream. It require a lot more checks for empty strings - a function for that was added for readability. Also 
				added in this version was a line to correct 'Whale' and 'whale' from being separate words. All words were sent to lower case.
Program 1.8 - A final cleaned up version before the released version.
Program		- Everything was stripped out from version 1.8 and made functions in the Form class. All the file does is to activate the newly created user interface
UI 1.0		- The form is created and the code from Program 1.8 is ported over. In addition some objects are added to the form to verify the code still works as expected.
UI 1.1		- The user interface was prettied up and put in place the required elements. 
